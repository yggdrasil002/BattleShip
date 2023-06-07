using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lobby
{
    public partial class GameForm : Form
    {
        //Info o serveru
        public string ServerIp;
        public int ServerPort;

        //Info o sobě
        public string PlayerName;

        //Info o protivníkovi
        public string EnemyIp;
        public int EnemyPort;
        public string EnemyName;
        public GameForm()
        {
            InitializeComponent();
        }
    }
}
