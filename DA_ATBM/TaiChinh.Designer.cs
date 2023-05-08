
namespace DA_ATBM
{
    partial class TaiChinh
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
            this.tab = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timkiemuserrolelbl = new System.Windows.Forms.Label();
            this.timkiemuserroletb = new System.Windows.Forms.TextBox();
            this.danhsachuserdg = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.timkiemuserbtn = new System.Windows.Forms.Button();
            this.timkiemuserlbl = new System.Windows.Forms.Label();
            this.timkiemusertb = new System.Windows.Forms.TextBox();
            this.BangPhanCong = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.Thoat = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.tab1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.danhsachuserdg)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BangPhanCong)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tab1);
            this.tab.Controls.Add(this.tabPage2);
            this.tab.Location = new System.Drawing.Point(12, 66);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(565, 474);
            this.tab.TabIndex = 2;
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.panel1);
            this.tab1.Controls.Add(this.label1);
            this.tab1.Controls.Add(this.button1);
            this.tab1.Controls.Add(this.timkiemuserrolelbl);
            this.tab1.Controls.Add(this.timkiemuserroletb);
            this.tab1.Controls.Add(this.danhsachuserdg);
            this.tab1.Location = new System.Drawing.Point(4, 22);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(557, 448);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "Xử lý nhân viên";
            this.tab1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(111, 356);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 84);
            this.panel1.TabIndex = 8;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(46, 55);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Phu cấp";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(229, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Lương";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(46, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "MaNV";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(238, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cập nhật";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Danh sách nhân viên";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(247, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Tìm kiếm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timkiemuserrolelbl
            // 
            this.timkiemuserrolelbl.AutoSize = true;
            this.timkiemuserrolelbl.Location = new System.Drawing.Point(6, 14);
            this.timkiemuserrolelbl.Name = "timkiemuserrolelbl";
            this.timkiemuserrolelbl.Size = new System.Drawing.Size(37, 13);
            this.timkiemuserrolelbl.TabIndex = 3;
            this.timkiemuserrolelbl.Text = "MaNV";
            // 
            // timkiemuserroletb
            // 
            this.timkiemuserroletb.Location = new System.Drawing.Point(88, 11);
            this.timkiemuserroletb.Name = "timkiemuserroletb";
            this.timkiemuserroletb.Size = new System.Drawing.Size(100, 20);
            this.timkiemuserroletb.TabIndex = 2;
            // 
            // danhsachuserdg
            // 
            this.danhsachuserdg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.danhsachuserdg.Location = new System.Drawing.Point(9, 61);
            this.danhsachuserdg.Name = "danhsachuserdg";
            this.danhsachuserdg.RowHeadersWidth = 51;
            this.danhsachuserdg.Size = new System.Drawing.Size(542, 289);
            this.danhsachuserdg.TabIndex = 0;
            this.danhsachuserdg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.danhsachuserdg_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.timkiemuserbtn);
            this.tabPage2.Controls.Add(this.timkiemuserlbl);
            this.tabPage2.Controls.Add(this.timkiemusertb);
            this.tabPage2.Controls.Add(this.BangPhanCong);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(557, 448);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Xem phân công";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // timkiemuserbtn
            // 
            this.timkiemuserbtn.Location = new System.Drawing.Point(161, 14);
            this.timkiemuserbtn.Name = "timkiemuserbtn";
            this.timkiemuserbtn.Size = new System.Drawing.Size(75, 23);
            this.timkiemuserbtn.TabIndex = 4;
            this.timkiemuserbtn.Text = "Tìm kiếm";
            this.timkiemuserbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.timkiemuserbtn.UseVisualStyleBackColor = true;
            this.timkiemuserbtn.Click += new System.EventHandler(this.timkiemuserbtn_Click);
            // 
            // timkiemuserlbl
            // 
            this.timkiemuserlbl.AutoSize = true;
            this.timkiemuserlbl.Location = new System.Drawing.Point(6, 17);
            this.timkiemuserlbl.Name = "timkiemuserlbl";
            this.timkiemuserlbl.Size = new System.Drawing.Size(37, 13);
            this.timkiemuserlbl.TabIndex = 3;
            this.timkiemuserlbl.Text = "MaNV";
            // 
            // timkiemusertb
            // 
            this.timkiemusertb.Location = new System.Drawing.Point(44, 14);
            this.timkiemusertb.Name = "timkiemusertb";
            this.timkiemusertb.Size = new System.Drawing.Size(100, 20);
            this.timkiemusertb.TabIndex = 2;
            // 
            // BangPhanCong
            // 
            this.BangPhanCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BangPhanCong.Location = new System.Drawing.Point(6, 40);
            this.BangPhanCong.Name = "BangPhanCong";
            this.BangPhanCong.RowHeadersWidth = 51;
            this.BangPhanCong.Size = new System.Drawing.Size(540, 402);
            this.BangPhanCong.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(8, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(580, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "CHÀO MÙNG NHÂN VIÊN TÀI CHÍNH ĐẾN VỚI ỨNG DỤNG";
            // 
            // Thoat
            // 
            this.Thoat.Location = new System.Drawing.Point(498, 554);
            this.Thoat.Name = "Thoat";
            this.Thoat.Size = new System.Drawing.Size(75, 23);
            this.Thoat.TabIndex = 8;
            this.Thoat.Text = "Thoát";
            this.Thoat.UseVisualStyleBackColor = true;
            this.Thoat.Click += new System.EventHandler(this.Thoat_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(16, 554);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 22);
            this.button4.TabIndex = 9;
            this.button4.Text = "Thông tin cá nhân";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // TaiChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 589);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.Thoat);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tab);
            this.Name = "TaiChinh";
            this.Text = "TaiChinh";
            this.tab.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.danhsachuserdg)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BangPhanCong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label timkiemuserrolelbl;
        private System.Windows.Forms.TextBox timkiemuserroletb;
        private System.Windows.Forms.DataGridView danhsachuserdg;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button timkiemuserbtn;
        private System.Windows.Forms.Label timkiemuserlbl;
        private System.Windows.Forms.TextBox timkiemusertb;
        private System.Windows.Forms.DataGridView BangPhanCong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Thoat;
        private System.Windows.Forms.Button button4;
    }
}