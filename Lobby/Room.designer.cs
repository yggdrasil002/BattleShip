namespace Room
{
    partial class Room
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Room));
            this.label1 = new System.Windows.Forms.Label();
            this.lblServerIp = new System.Windows.Forms.Label();
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.listPlayers = new System.Windows.Forms.ListBox();
            this.btnChallenge = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // lblServerIp
            // 
            this.lblServerIp.AutoSize = true;
            this.lblServerIp.Location = new System.Drawing.Point(126, 34);
            this.lblServerIp.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblServerIp.Name = "lblServerIp";
            this.lblServerIp.Size = new System.Drawing.Size(135, 20);
            this.lblServerIp.TabIndex = 1;
            this.lblServerIp.Text = "127.0.0.1 : 20123";
            // 
            // rtbChat
            // 
            this.rtbChat.BackColor = System.Drawing.SystemColors.Window;
            this.rtbChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbChat.Location = new System.Drawing.Point(54, 80);
            this.rtbChat.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(615, 524);
            this.rtbChat.TabIndex = 2;
            this.rtbChat.Text = "Tham gia trò chuyện bằng cách gửi tin nhắn!";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(54, 620);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(483, 26);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.Click += new System.EventHandler(this.txtMessage_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(549, 618);
            this.btnSend.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(124, 35);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Gửi";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // listPlayers
            // 
            this.listPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listPlayers.FormattingEnabled = true;
            this.listPlayers.ItemHeight = 20;
            this.listPlayers.Location = new System.Drawing.Point(681, 80);
            this.listPlayers.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.listPlayers.Name = "listPlayers";
            this.listPlayers.Size = new System.Drawing.Size(282, 522);
            this.listPlayers.TabIndex = 5;
            this.listPlayers.Click += new System.EventHandler(this.listPlayers_Click);
            this.listPlayers.DoubleClick += new System.EventHandler(this.listPlayers_DoubleClick);
            // 
            // btnChallenge
            // 
            this.btnChallenge.Location = new System.Drawing.Point(681, 618);
            this.btnChallenge.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnChallenge.Name = "btnChallenge";
            this.btnChallenge.Size = new System.Drawing.Size(284, 35);
            this.btnChallenge.TabIndex = 6;
            this.btnChallenge.Text = "Chơi";
            this.btnChallenge.UseVisualStyleBackColor = true;
            this.btnChallenge.Click += new System.EventHandler(this.btnChallenge_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(681, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Số người chơi";
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.Location = new System.Drawing.Point(801, 34);
            this.lblPlayers.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(51, 20);
            this.lblPlayers.TabIndex = 8;
            this.lblPlayers.Text = "3 / 20";
            // 
            // Room
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1035, 692);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnChallenge);
            this.Controls.Add(this.listPlayers);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.rtbChat);
            this.Controls.Add(this.lblServerIp);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "Room";
            this.Text = "Battleships - Room";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LobbyForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblServerIp;
        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox listPlayers;
        private System.Windows.Forms.Button btnChallenge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPlayers;
    }
}