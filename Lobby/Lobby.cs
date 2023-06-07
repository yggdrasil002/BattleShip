using BattleshipsLibrary;
using NetworkCommsDotNet;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lobby
{
    public partial class Lobby : Form
    {
        public static Room.Room Room;
        public Lobby()
        {
            InitializeComponent();
        }

        private void btnPhong1_Click(object sender, EventArgs e)
        {
            //Số thứ tự phòng
            int num = 1;

            //Vào Room 1
            ConnectRoom(ConnectDB(num - 1));
        }

        private void btnPhong2_Click(object sender, EventArgs e)
        {
            //Số thứ tự phòng
            int num = 2;

            //Vào Room 2
            ConnectRoom(ConnectDB(num - 1));
        }

        private void btnPhong3_Click(object sender, EventArgs e)
        {
            //Số thứ tự phòng
            int num = 3;

            //Vào Room 3
            ConnectRoom(ConnectDB(num - 1));
        }

        private void btnPhong4_Click(object sender, EventArgs e)
        {
            //Số thứ tự phòng
            int num = 4;

            //Vào Room 4
            ConnectRoom(ConnectDB(num - 1));
        }

        private void btnPhong5_Click(object sender, EventArgs e)
        {
            //Số thứ tự phòng
            int num = 5;

            //Vào Room 5
            ConnectRoom(ConnectDB(num - 1));
        }
        string ip;
        private string ConnectDB(int i)
        {
            //Kết nối tới cơ sở dữ liệu chứ IP server
            string constr = @"Data Source=LAPTOP-R19BP168\SQLEXPRESS;Initial Catalog=IP_Server_Provided;Integrated Security=True";
            SqlConnection connection = new SqlConnection(constr);
            connection.Open();

            //Chọn IP theo phòng
            string query = "SELECT * FROM IP\nWHERE Room = " + i;
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // Đọc giá trị từng cột trong bản ghi
                string columnValue = reader.GetString(1); // Đọc giá trị từ cột IP (index 1)

                // Xử lý dữ liệu

                ip = columnValue;
            }
            connection.Close();
            return ip;
        }

        private void ConnectRoom(string ipaddress)
        {
            try
            {
                // Gửi yêu cầu kết nối và đợi 10 giây để máy chủ phản hồi
                var response = NetworkComms.SendReceiveObject<string, ConnectResponse>("ConnectUser", ipaddress, 20123, "ConnectInfo", 10000, "Huy");

                //Yêu cầu được chấp nhận
                if (response.ResponseType == ResponseType.Accepted)
                {
                    //Ẩn hình thức đăng nhập
                    Hide();

                    //Mở Lobby form
                    Room = new Room.Room(response)
                    {
                        ServerIp = ipaddress,
                        ServerPort = 20123,
                        PlayerName = "Huy"
                    };
                    Room.Show();
                }
                //Yêu cầu bị từ chối
                else
                {
                    //Hiển thị lý do từ chối
                    MessageBox.Show(string.Format($"Server {response.ServerName} từ chối kết nối với tin nhắn: {response.Response}"));
                }
            }
            catch
            {
                //Kết nối thất bại
                MessageBox.Show("Không thể kết nối với máy chủ, kiểm tra kết nối và tính chính xác của dữ liệu đã nhập");
            }
        }
    }
}
