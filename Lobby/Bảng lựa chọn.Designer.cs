namespace Lobby
{
    partial class Bảng_lựa_chọn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bảng_lựa_chọn));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lsbTGMoiLuot = new System.Windows.Forms.ListBox();
            this.lsbTongTG = new System.Windows.Forms.ListBox();
            this.lsbNguoiChoiTruoc = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(239, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(381, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thời gian mỗi lượt chơi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(153, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(571, 39);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tổng thời gian của từng người chơi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(284, 286);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 39);
            this.label3.TabIndex = 5;
            this.label3.Text = "Người chơi trước";
            // 
            // lsbTGMoiLuot
            // 
            this.lsbTGMoiLuot.FormattingEnabled = true;
            this.lsbTGMoiLuot.ItemHeight = 38;
            this.lsbTGMoiLuot.Items.AddRange(new object[] {
            "10s",
            "15s",
            "30s",
            "40s",
            "Không giới hạn"});
            this.lsbTGMoiLuot.Location = new System.Drawing.Point(246, 79);
            this.lsbTGMoiLuot.Name = "lsbTGMoiLuot";
            this.lsbTGMoiLuot.Size = new System.Drawing.Size(351, 42);
            this.lsbTGMoiLuot.TabIndex = 9;
            // 
            // lsbTongTG
            // 
            this.lsbTongTG.FormattingEnabled = true;
            this.lsbTongTG.ItemHeight = 38;
            this.lsbTongTG.Items.AddRange(new object[] {
            "3 phút",
            "4 phút",
            "5 phút",
            "Không giới hạn"});
            this.lsbTongTG.Location = new System.Drawing.Point(246, 218);
            this.lsbTongTG.Name = "lsbTongTG";
            this.lsbTongTG.Size = new System.Drawing.Size(351, 42);
            this.lsbTongTG.TabIndex = 10;
            // 
            // lsbNguoiChoiTruoc
            // 
            this.lsbNguoiChoiTruoc.FormattingEnabled = true;
            this.lsbNguoiChoiTruoc.ItemHeight = 38;
            this.lsbNguoiChoiTruoc.Items.AddRange(new object[] {
            "Ngẫu nhiên",
            "Đối thủ",
            "Bản thân"});
            this.lsbNguoiChoiTruoc.Location = new System.Drawing.Point(246, 344);
            this.lsbNguoiChoiTruoc.Name = "lsbNguoiChoiTruoc";
            this.lsbNguoiChoiTruoc.Size = new System.Drawing.Size(351, 42);
            this.lsbNguoiChoiTruoc.TabIndex = 11;
            // 
            // Bảng_lựa_chọn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 38F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(896, 501);
            this.Controls.Add(this.lsbNguoiChoiTruoc);
            this.Controls.Add(this.lsbTongTG);
            this.Controls.Add(this.lsbTGMoiLuot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "Bảng_lựa_chọn";
            this.Text = "Bảng_lựa_chọn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lsbTGMoiLuot;
        private System.Windows.Forms.ListBox lsbTongTG;
        private System.Windows.Forms.ListBox lsbNguoiChoiTruoc;
    }
}