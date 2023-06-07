using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BattleshipsLibrary;
using Lobby;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace Room
{
    public partial class Room : Form
    {
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }
        public string ServerName { get; set; }
        public string PlayerName { get; set; }

        public Room(ConnectResponse response)
        {
            InitializeComponent();
            MinimumSize = MaximumSize = Size;

            //Con trỏ cho các gói đến
            NetworkComms.AppendGlobalConnectionCloseHandler(ConnectionShutdownDelegate);
            NetworkComms.AppendGlobalIncomingPacketHandler<ChatMessage>("DisplayChatMessage", DisplayChatMessageDelegatePointer);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ChallengeAcceptRequest", ChallengeAcceptRequestDelegatePointer);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ChallengeFailed", ChallengeFailedDelegatePointer);
            NetworkComms.AppendGlobalIncomingPacketHandler<Client>("ChallengeAccepted", ChallengeAcceptedDelegatePointer);
            NetworkComms.AppendGlobalIncomingPacketHandler<List<Client>>("UpdateListInfo", UpdateListInfoDelegatePointer);

            //Cài đặt theo phản hồi từ máy chủ
            lblServerIp.Text = ServerName = response.ServerName;
            lblPlayers.Text = response.ConnectedClients.Count.ToString("00");

            //Điền vào trang tính theo phản hồi từ máy chủ
            foreach (var client in response.ConnectedClients)
            {
                listPlayers.Items.Add(client.Name);
            }
        }

        #region Phương pháp chính

        //Mất kết nối máy chủ
        private void ConnectionShutdownDelegate(Connection connection)
        {
            MessageBox.Show($"Connection to server {ServerName} lost!");
            NetworkComms.Shutdown();
            Application.Exit();
        }

        //Xử lý sự kiện cập nhật danh sách người chơi trực tuyến.
        private void UpdateListInfoDelegatePointer(PacketHeader packetheader, Connection connection, List<Client> clients)
        {
            Invoke(new UpdateListDelegate(UpdateList), clients);
        }

        //Update online clients - delegate
        private delegate void UpdateListDelegate(List<Client> clients);

        //Updating online clients
        private void UpdateList(List<Client> clients)
        {
            lblPlayers.Text = clients.Count.ToString("00");

            listPlayers.Items.Clear();
            foreach (var client in clients)
            {
                listPlayers.Items.Add(client.Name);
            }
        }

        //Xử lý sự kiện khi yêu cầu thách đấu được chấp nhận bởi người chơi khác.
        private void ChallengeAcceptedDelegatePointer(PacketHeader packetheader, Connection connection, Client enemy)
        {
            Invoke(new OpenGameFormDelegate(OpenGameForm), ServerIp, ServerPort, enemy);
        }

        //Mở cửa sổ trò chơi - đại biểu
        private delegate void OpenGameFormDelegate(string serverIp, int serverPort, Client enemy);

        //Mở cửa sổ trò chơi khi thách đấu được chấp nhận.
        private void OpenGameForm(string serverIp, int serverPort, Client enemy)
        {
            Hide();

            Lobby.GameForm gameForm = new GameForm
            {
                ServerIp = serverIp,
                ServerPort = serverPort,
                EnemyIp = enemy.Ip,
                EnemyPort = enemy.Port,
                EnemyName = enemy.Name,
                PlayerName = PlayerName
            };

            gameForm.Show();
        }

        //Xử lý sự kiện khi có yêu cầu thách đấu từ người chơi khác.
        private void ChallengeAcceptRequestDelegatePointer(PacketHeader packetheader, Connection connection, string name)
        {
            var response = MessageBox.Show($"User {name} has invited you to play, accept?", "Invitation to play", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (response == DialogResult.Yes)
            {
                NetworkComms.SendObject("ChallengeAcceptInfo", ServerIp, ServerPort, true);
            }
            else
            {
                NetworkComms.SendObject("ChallengeAcceptInfo", ServerIp, ServerPort, false);
            }
        }

        //Xử lý sự kiện khi không thể thách đấu với người chơi khác.
        private void ChallengeFailedDelegatePointer(PacketHeader packetheader, Connection connection, string text)
        {
            MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Xử lý sự kiện khi có tin nhắn chat mới.
        private void DisplayChatMessageDelegatePointer(PacketHeader packetheader, Connection connection, ChatMessage message)
        {
            Invoke(new DisplayToChatDelegate(DisplayToChat), message);
        }

        //Tin nhắn đến để trò chuyện - đại biểu
        private delegate void DisplayToChatDelegate(ChatMessage message);

        //Tin nhắn trò chuyện đến
        private void DisplayToChat(ChatMessage message)
        {
            rtbChat.Text += $"\n{message}";
        }

        // Gửi yêu cầu thách đấu tới người chơi được chọn.
        private void Challenge()
        {
            string name = listPlayers.GetItemText(listPlayers.SelectedItem);

            if (name == PlayerName)
            {
                MessageBox.Show("You cannot challenge yourself!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var response = MessageBox.Show($"Do you want to invite player {name} to play?", "Invite to play",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.Yes)
                {
                    NetworkComms.SendObject("ChallengeRequest", ServerIp, ServerPort, name);
                }
            }
        }

        #endregion

        #region Form Eventy

        //Cài đặt nút chấp nhận
        private void txtMessage_Click(object sender, EventArgs e)   => AcceptButton = btnSend;
        private void listPlayers_Click(object sender, EventArgs e)  => AcceptButton = btnChallenge;

        //Gửi tin nhắn từ cuộc trò chuyện đến máy chủ
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text != "")
            {
                ChatMessage message = new ChatMessage(PlayerName, txtMessage.Text);
                NetworkComms.SendObject("BroadcastChatMessage", ServerIp, ServerPort, message);
                txtMessage.Clear();
            }
        }


        //Thách thức người chơi được đánh dấu
        private void listPlayers_DoubleClick(object sender, EventArgs e)    => Challenge();
        private void btnChallenge_Click(object sender, EventArgs e)         => Challenge();

        //Đóng cửa sổ
        private void LobbyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Kết thúc quá trình kết nối
            NetworkComms.SendObject("Disconnect", ServerIp, ServerPort, "");
            NetworkComms.Shutdown();
            Application.Exit();
        }

        #endregion
    }
}
