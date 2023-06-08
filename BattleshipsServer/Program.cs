using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using BattleshipsLibrary;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using static NetworkCommsDotNet.Tools.HostInfo;

namespace BattleshipsServer
{
    class Program
    {
        public static List<Client> Clients = new List<Client>();
        public static List<Game> Games = new List<Game>();
        public static string ServerName = "ServerBattleShip";

        int matches;
            int win;

        private static int _gamesCountId = 0;

        static void Main(string[] args)
        {
            //Đặt tên khác với tham số bảng điều khiển
            if (args.Length == 1) ServerName = args[0];

            Console.Title = $"{ServerName} | BattleShips Server";

            //Con trỏ gói
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ConnectUser", ConnectUser);
            NetworkComms.AppendGlobalIncomingPacketHandler<GameStartRequest>("GameStartRequest", GameStartRequest);
            NetworkComms.AppendGlobalIncomingPacketHandler<GameFireInfo>("GameFireInfo", GameFireInfo);
            NetworkComms.AppendGlobalIncomingPacketHandler<ChatMessage>("BroadcastChatMessage", BroadcastChatMessageDelgatePointer);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ChallengeRequest", ChallengeRequestDelgatePointer);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Disconnect", DisconnectDelgatePointer);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("History", SendHistory);
            //NetworkComms.AppendGlobalConnectionCloseHandler(ConnectionShutdownDelegate);

            //Lắng nghe trên địa chỉ máy tính.
            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, 20123));

            //List of addresses
            IPEndPoint[] IP = new IPEndPoint[20];
            int i = 0;
            Console.WriteLine($"The server waits for TCP connections at the following addresses:");
            foreach (var endPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                var localEndPoint = (IPEndPoint)endPoint;
                //IP[i] = localEndPoint;
                Console.WriteLine(" -> {0} : {1}", localEndPoint.Address, localEndPoint.Port);
                
                ConnectDB(localEndPoint.Address, i);

                i++;
                
            }

            //Đóng sau khi nhấn phím
            Console.WriteLine();
            Console.WriteLine("Click any key to shut down the server");
            Console.WriteLine("-----------------------------------\n");
            Console.ReadKey(true);

            //Reset bảng IP
            ResetIPDB();

            //Kết thúc quá trình kết nối
            NetworkComms.Shutdown();
        }

        private static void SendHistory(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            
        }

        //Ngắt kết nối máy khách dựa trên yêu cầu
        private static void DisconnectDelgatePointer(PacketHeader packetheader, Connection connection, string enemyName)
        {
            //IP Khách
            IPEndPoint clientEndPoint = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;

            Client enemy = Clients.Find(x => x.Name == enemyName);
            Client client = Clients.Find(x => x.Ip == clientEndPoint.Address.ToString() && x.Port == clientEndPoint.Port);

            //Gửi cho đối thủ biết rằng người chơi đã ngắt kết nối
            if (enemy != null) NetworkComms.SendObject("Disconnect", enemy.Ip, enemy.Port, true);

            Console.WriteLine($"({client.Name}) to disconnect!");

            Game game = Games.Find(x => x.HasClient(enemy) || x.HasClient(client));

            //Nếu chúng tồn tại, xóa bản ghi khỏi trang tính
            if (game != null) Games.Remove(game);
            Clients.Remove(client);

            foreach (Client c in Clients)
            {
                NetworkComms.SendObject("UpdateListInfo", c.Ip, c.Port, Clients);
            }
        }

        //Yêu cầu mời người chơi
        private static void ChallengeRequestDelgatePointer(PacketHeader packetheader, Connection connection, string name)
        {
            //Người chơi bị thách đấu - đối thủ
            Client enemy = Clients.Find(x => x.Name == name);
            bool isEnemyInGame = Games.Any(x => x.HasClient(enemy));

            IPEndPoint clientEndPoint = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;

            //Nếu nó tồn tại và không có trong trò chơi
            if (enemy != null && !isEnemyInGame)
            {
                //Người thách đấu
                Client player = Clients.Find(x => x.Ip == clientEndPoint.Address.ToString() && x.Port == clientEndPoint.Port);

                Console.WriteLine($"({player.Name}) Prompt the user to play {enemy.Name}");
                try
                {
                    //Gửi yêu cầu, đợi 20 giây để phản hồi
                    bool response = NetworkComms.SendReceiveObject<string, bool>("ChallengeAcceptRequest", enemy.Ip, enemy.Port, "ChallengeAcceptInfo", 20000, player.Name);

                    //Đã được chấp nhận
                    if (response)
                    {
                        Console.WriteLine($"({enemy.Name}) accepted a challenge from {player.Name}");
                        NetworkComms.SendObject("ChallengeAccepted", clientEndPoint.Address.ToString(), clientEndPoint.Port, enemy);
                        NetworkComms.SendObject("ChallengeAccepted", enemy.Ip, enemy.Port, player);
                    }
                    //Từ chối
                    else
                    {
                        Console.WriteLine($"({enemy.Name}) rejected the challenge from {player.Name}");
                        NetworkComms.SendObject("ChallengeFailed", clientEndPoint.Address.ToString(), clientEndPoint.Port, $"The user rejected your request to play!");
                    }
                }
                //Error, no response
                catch
                {
                    Console.WriteLine($"({enemy.Name}) Does not respond to a call from {player.Name}");
                    NetworkComms.SendObject("ChallengeFailed", clientEndPoint.Address.ToString(), clientEndPoint.Port, $"The user did not have time to respond to your request in time!");
                }
            }

            //Người chơi đã ở trong trò chơi
            if (isEnemyInGame)
            {
                NetworkComms.SendObject("ChallengeFailed", clientEndPoint.Address.ToString(), clientEndPoint.Port, $"User {name} currently competing with someone else!");
            }

            //không tìm thấy người chơi
            if (enemy == null)
            {
                NetworkComms.SendObject("ChallengeFailed", clientEndPoint.Address.ToString(), clientEndPoint.Port, $"User not found!");

            }
        }

        //Gửi tin nhắn trò chuyện cho tất cả Client
        private static void BroadcastChatMessageDelgatePointer(PacketHeader packetheader, Connection connection, ChatMessage message)
        {

            foreach (Client client in Clients)
            {
                NetworkComms.SendObject("DisplayChatMessage", client.Ip, client.Port, message);
            }

            Console.WriteLine($"({message.PlayerName}) sent a message: {message.Message}");
        }

        //Thông tin về việc bắn vào người chơi (client shooting)
        private static void GameFireInfo(PacketHeader packetheader, Connection connection, GameFireInfo gfi)
        {
            Game game = Games.Find(x => x.Id == gfi.GameId);

            IPEndPoint clientEndPoint = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;

            if (game.Client1.Ip == clientEndPoint.Address.ToString() && game.Client1.Port == clientEndPoint.Port)
            {
                Console.WriteLine($"(Game #{game.Id}) User {game.Client1.Name} is shooting");
                game.FireOnClient2(gfi.Position);
            }

            if (game.Client2.Ip == clientEndPoint.Address.ToString() && game.Client2.Port == clientEndPoint.Port)
            {
                Console.WriteLine($"(Game #{game.Id}) User {game.Client2.Name} is shooting");
                game.FireOnClient1(gfi.Position);
            }

            game.ResetIfEnd();
        }

        //Bắt đầu trò chơi
        private static void GameStartRequest(PacketHeader packetheader, Connection connection, GameStartRequest gsr)
        {
            IPEndPoint clientEndPoint = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;
            Client client = Clients.Find(x => x.Ip == clientEndPoint.Address.ToString() && x.Port == clientEndPoint.Port);
            Client enemyClient = Clients.Find(x => x.Ip == gsr.EnemyIp && x.Port == gsr.EnemyPort);
            Game game = Games.Find(x => x.HasClient(enemyClient));

            //Trò chơi đã tồn tại, nó được tạo bởi người chơi khác
            if (game != null && client != game.Client1)
            {
                game.Client2 = client;
                game.Client2ShipPositions = gsr.ShipsPositions;

                game.StartGame(_gamesCountId);

                _gamesCountId++;
            }
            //Lập trình trò chơi không tồn tại, quá trình tạo mới ban đầu
            else
            {
                Game g = new Game
                {
                    Client1 = client,
                    Client1ShipPositions = gsr.ShipsPositions
                };
                Games.Add(g);
                Console.WriteLine($"(Game #{g.Id}) between {client.Name} and {enemyClient.Name} has started!");
            }

            Console.WriteLine($"({client.Name}) is ready to play with the user {enemyClient.Name}");

        }

        //Kết nối máy khách với máy chủ
        private static void ConnectUser(PacketHeader packetheader, Connection connection, string name)
        {
            IPEndPoint clientEndPoint = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;

            //Một người dùng có cùng tên đã tồn tại
            if (Clients.Any(x => x.Name == name))
            {
                string reason = string.Format($"A user with the same name already exists {name} is already taken!");

                ConnectResponse response = new ConnectResponse(ResponseType.Rejected, ServerName, reason);
                connection.SendObject("ConnectInfo", response);

                Console.WriteLine($"{name} failed to connect to the server because of:{reason}");

            }
            //Thêm người chơi
            else
            {
                Clients.Add(new Client(name, clientEndPoint.Address.ToString(), clientEndPoint.Port));

                ConnectResponse response = new ConnectResponse(ResponseType.Accepted, ServerName, Clients);
                connection.SendObject("ConnectInfo", response);

                Console.WriteLine($"({name}) joined server (total {Clients.Count} players)");
            }

            //Cập nhật khách hàng trực tuyến
            foreach (Client client in Clients)
            {
                NetworkComms.SendObject("UpdateListInfo", client.Ip, client.Port, Clients);
            }
        }
        //Thêm các IP Server lắng nghe
        private static void ConnectDB(IPAddress ip, int i)
        {
            //Tạo kết nối với cơ sở dữ liệu
            string constr = @"Data Source=LAPTOP-R19BP168\SQLEXPRESS;Initial Catalog=IP_Server_Provided;Integrated Security=True";
            SqlConnection connection = new SqlConnection(constr);

            //Bật kết nối
            connection.Open();

            //Gửi các địa chỉ lắng nghe đến cơ sở dữ liệu
            string addIP = ip.ToString();
            string number = i.ToString();
            string query = "INSERT INTO IP (Room, IP_provided)\nVALUES ('"+number+"', '"+addIP+"')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            //Đóng kết nối
            connection.Close();
        }
        private static void ResetIPDB()
        {
            //Tạo kết nối với cơ sở dữ liệu
            string constr = @"Data Source=LAPTOP-R19BP168\SQLEXPRESS;Initial Catalog=IP_Server_Provided;Integrated Security=True";
            SqlConnection connection = new SqlConnection(constr);

            //Bật kết nối
            connection.Open();

            //Reset bảng IP
            string query = "delete from IP";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            //Đóng kết nối
            connection.Close();
        }
        private static void History(string name)
        {
            //Tạo kết nối với cơ sở dữ liệu
            string constr = @"Data Source=LAPTOP-R19BP168\SQLEXPRESS;Initial Catalog=IP_Server_Provided;Integrated Security=True";
            SqlConnection connection = new SqlConnection(constr);

            //Bật kết nối
            connection.Open();

            //
            string query = "SELECT * FROM history\nWHERE Username = '"+ name +"'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            //
            int matches;
            int win;
            while (reader.Read())
            {
                // Đọc giá trị từng cột trong bản ghi
                matches = reader.GetInt32(1);   //Truy xuất số trận chơi
                win = reader.GetInt32(2);   //Truy xuất số trận thắng
                Console.WriteLine(matches);
                Console.WriteLine(win);
            }
            //Console.WriteLine(matches);
            connection.Close();
        }
    }
}