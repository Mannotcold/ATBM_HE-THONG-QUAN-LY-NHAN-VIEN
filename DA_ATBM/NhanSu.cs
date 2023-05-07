using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DA_ATBM
{
    public partial class NhanSu : Form
    {
        public NhanSu()
        {
            InitializeComponent();
        }

        private string MK, TK;
      
        public NhanSu(string tk, string mk)
        {
            InitializeComponent();
            this.TK = tk;
            this.MK = mk;
        }
        private void XemTTPhongBan()
        {
            OracleConnection con_ttPB = new OracleConnection();
            con_ttPB.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ttpb;
            cmd_ttpb = new OracleCommand("select * from quanly.PHONGBAN_VIEW", con_ttPB);
            cmd_ttpb.CommandType = CommandType.Text;
            con_ttPB.Open();
            using (OracleDataReader reader = cmd_ttpb.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                TtPhongBan.DataSource = dataTable;
            }
            con_ttPB.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            XemTTPhongBan();
        }

        private void ChinhsuaTENPB()
        {
            if (textTENPB.Text != "")
            {
                // Nếu có nhập mã trưởng phòng thì mới rỗng thì thực hiện các hành động ở đây
                OracleConnection con_csTENPB = new OracleConnection();
                con_csTENPB.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
                DataSet dataSet_ds = new DataSet();
                con_csTENPB.Open();
                OracleCommand cmd_csTENPB;
                OracleCommand cmd_commit;
                cmd_csTENPB = new OracleCommand("update quanly.PHONGBAN_VIEW set TENPB = '" + textTENPB.Text.ToUpper() + "' WHERE MAPB = '" + textMAPB.Text.ToUpper() + "'", con_csTENPB);
                cmd_csTENPB.CommandType = CommandType.Text;
                cmd_commit = new OracleCommand("commit", con_csTENPB);
                cmd_commit.CommandType = CommandType.Text;
                cmd_csTENPB.ExecuteNonQuery();
            
                con_csTENPB.Close();
            }
        }

        private void ChinhsuaTRPHG()
        {
            if (textTRPHG.Text != "")
            {
                // Nếu có nhập mã trưởng phòng thì mới rỗng thì thực hiện các hành động ở đây

                OracleConnection con_csTRPHG = new OracleConnection();
                con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
                DataSet dataSet_ds = new DataSet();
                con_csTRPHG.Open();
                OracleCommand cmd_csTRPHG;
                OracleCommand cmd_commit;
                cmd_csTRPHG = new OracleCommand("update quanly.PHONGBAN_VIEW set TRPHG = '" + textTRPHG.Text.ToUpper() + "' WHERE MAPB = '" + textMAPB.Text.ToUpper() + "'", con_csTRPHG);
                cmd_csTRPHG.CommandType = CommandType.Text;
                cmd_commit = new OracleCommand("commit", con_csTRPHG);
                cmd_commit.CommandType = CommandType.Text;
                cmd_csTRPHG.ExecuteNonQuery();
                
                con_csTRPHG.Close();
            }
        }

        private void ThemPB()
        {
            OracleConnection con_themPB = new OracleConnection();
            con_themPB.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";


            DataSet dataSet_ttq = new DataSet();
            con_themPB.Open();
            OracleCommand cmd_themPB;
            OracleCommand cmd_commit;
            cmd_themPB = new OracleCommand("insert into quanly.PHONGBAN_VIEW (MAPB, TENPB, TRPHG) values ('" + textMAPB + "','" + textTENPB.Text + "',date'" + textTRPHG.Text + "')", con_themPB);
            cmd_themPB.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_themPB);
            cmd_commit.CommandType = CommandType.Text;
            //con_themPB.ExecuteNonQuery();
            con_themPB.Close();
        }




        //button Cap nhat 
        private void button7_nv_Click(object sender, EventArgs e)
        {
            ChinhsuaTENPB();
            ChinhsuaTRPHG();
            MessageBox.Show("Cập nhật thành công");
        }
        //button them
        private void button6_nv_Click(object sender, EventArgs e)
        {
            ThemPB();
            MessageBox.Show("Thêm thành công");
        }


        private void XemthongtinNhanVien()
        {
            OracleConnection con_ttnv = new OracleConnection();
            con_ttnv.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ttpb;
            cmd_ttpb = new OracleCommand("select * from quanly.NHANVIENNHANSU_V", con_ttnv);
            cmd_ttpb.CommandType = CommandType.Text;
            con_ttnv.Open();
            using (OracleDataReader reader = cmd_ttpb.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                ThongtinNV.DataSource = dataTable;
            }
            con_ttnv.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XemthongtinNhanVien();
        }

        private void XemNhanVien_luong_phucap_null()
        {
            OracleConnection con_ttnv = new OracleConnection();
            con_ttnv.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ttpb;
            cmd_ttpb = new OracleCommand("select * from quanly.NHANVIEN_VIEW_luong_phucap_null", con_ttnv);
            cmd_ttpb.CommandType = CommandType.Text;
            con_ttnv.Open();
            using (OracleDataReader reader = cmd_ttpb.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                NV_Luong_pc_null.DataSource = dataTable;
            }
            con_ttnv.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            XemNhanVien_luong_phucap_null();
        }


        private void ChinhSuaNhanVien_LUONG_PC_NULL_TENNV() 
        {
            OracleConnection con_csTRPHG = new OracleConnection();
            con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csTRPHG.Open();
            OracleCommand cmd_csTRPHG;
            OracleCommand cmd_commit;
            cmd_csTRPHG = new OracleCommand("update quanly.NHANVIEN_VIEW_luong_phucap_null set TENNV = '" + TENNVNHANSU.Text.ToUpper() + "' WHERE MANV = '" + MANVNHANSU.Text.ToUpper() + "'", con_csTRPHG);
            cmd_csTRPHG.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csTRPHG);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csTRPHG.ExecuteNonQuery();

            con_csTRPHG.Close();
        }
        private void ChinhSuaNhanVien_LUONG_PC_NULL_PHAI()
        {
            OracleConnection con_csTRPHG = new OracleConnection();
            con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csTRPHG.Open();
            OracleCommand cmd_csTRPHG;
            OracleCommand cmd_commit;
            cmd_csTRPHG = new OracleCommand("update quanly.NHANVIEN_VIEW_luong_phucap_null set PHAI = '" + PHAINHANSU.Text.ToUpper() + "' WHERE MANV = '" + MANVNHANSU.Text.ToUpper() + "'", con_csTRPHG);
            cmd_csTRPHG.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csTRPHG);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csTRPHG.ExecuteNonQuery();

            con_csTRPHG.Close();
        }
        private void ChinhSuaNhanVien_LUONG_PC_NULL_NGAYSINH()
        {
            OracleConnection con_csTRPHG = new OracleConnection();
            con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csTRPHG.Open();
            OracleCommand cmd_csTRPHG;
            OracleCommand cmd_commit;
            cmd_csTRPHG = new OracleCommand("update quanly.NHANVIEN_VIEW_luong_phucap_null set NGAYSINH = date'" + NGAYSINHNHANSU.Text.ToUpper() + "' WHERE MANV = '" + MANVNHANSU.Text.ToUpper() + "'", con_csTRPHG);
            cmd_csTRPHG.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csTRPHG);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csTRPHG.ExecuteNonQuery();

            con_csTRPHG.Close();
        }
        private void ChinhSuaNhanVien_LUONG_PC_NULL_DIACHI()
        {
            OracleConnection con_csTRPHG = new OracleConnection();
            con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csTRPHG.Open();
            OracleCommand cmd_csTRPHG;
            OracleCommand cmd_commit;
            cmd_csTRPHG = new OracleCommand("update quanly.NHANVIEN_VIEW_luong_phucap_null set DIACHI = '" + DIACHINHANSU.Text.ToUpper() + "' WHERE MANV = '" + MANVNHANSU.Text.ToUpper() + "'", con_csTRPHG);
            cmd_csTRPHG.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csTRPHG);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csTRPHG.ExecuteNonQuery();

            con_csTRPHG.Close();
        }
        private void ChinhSuaNhanVien_LUONG_PC_NULL_SDT()
        {
            OracleConnection con_csTRPHG = new OracleConnection();
            con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csTRPHG.Open();
            OracleCommand cmd_csTRPHG;
            OracleCommand cmd_commit;
            cmd_csTRPHG = new OracleCommand("update quanly.NHANVIEN_VIEW_luong_phucap_null set SDT = '" + SDTNHANSU.Text.ToUpper() + "' WHERE MANV = '" + MANVNHANSU.Text.ToUpper() + "'", con_csTRPHG);
            cmd_csTRPHG.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csTRPHG);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csTRPHG.ExecuteNonQuery();

            con_csTRPHG.Close();
        }
        private void ChinhSuaNhanVien_LUONG_PC_NULL_VAITRO()
        {
            OracleConnection con_csTRPHG = new OracleConnection();
            con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csTRPHG.Open();
            OracleCommand cmd_csTRPHG;
            OracleCommand cmd_commit;
            cmd_csTRPHG = new OracleCommand("update quanly.NHANVIEN_VIEW_luong_phucap_null set VAITRO = '" + VAITRONHANSU.Text.ToUpper() + "' WHERE MANV = '" + MANVNHANSU.Text.ToUpper() + "'", con_csTRPHG);
            cmd_csTRPHG.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csTRPHG);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csTRPHG.ExecuteNonQuery();

            con_csTRPHG.Close();
        }
        private void ChinhSuaNhanVien_LUONG_PC_NULL_MANQL()
        {
            OracleConnection con_csTRPHG = new OracleConnection();
            con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csTRPHG.Open();
            OracleCommand cmd_csTRPHG;
            OracleCommand cmd_commit;
            cmd_csTRPHG = new OracleCommand("update quanly.NHANVIEN_VIEW_luong_phucap_null set MANQL = '" + MANQLNHANSU.Text.ToUpper() + "' WHERE MANV = '" + MANVNHANSU.Text.ToUpper() + "'", con_csTRPHG);
            cmd_csTRPHG.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csTRPHG);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csTRPHG.ExecuteNonQuery();

            con_csTRPHG.Close();
        }
        private void ChinhSuaNhanVien_LUONG_PC_NULL_PHG()
        {
            OracleConnection con_csTRPHG = new OracleConnection();
            con_csTRPHG.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csTRPHG.Open();
            OracleCommand cmd_csTRPHG;
            OracleCommand cmd_commit;
            cmd_csTRPHG = new OracleCommand("update quanly.NHANVIEN_VIEW_luong_phucap_null set PHG = '" + PHGNHANSU.Text.ToUpper() + "' WHERE MANV = '" + MANVNHANSU.Text.ToUpper() + "'", con_csTRPHG);
            cmd_csTRPHG.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csTRPHG);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csTRPHG.ExecuteNonQuery();

            con_csTRPHG.Close();
        }

        
        
        //-	cập nhật dữ liệu trong quan hệ NHANVIEN với giá trị các trường LUONG, PHUCAP là mang giá trị mặc định là NULL
        private void button2_Click(object sender, EventArgs e)
        {

            if (MANVNHANSU.Text != "")
            {

                if (TENNVNHANSU.Text != "")
                {
                    ChinhSuaNhanVien_LUONG_PC_NULL_TENNV();
                }
                if (PHAINHANSU.Text != "")
                {
                    ChinhSuaNhanVien_LUONG_PC_NULL_PHAI();
                }
                if (NGAYSINHNHANSU.Text != "")
                {
                    ChinhSuaNhanVien_LUONG_PC_NULL_NGAYSINH();
                }
                if (DIACHINHANSU.Text != "")
                {
                    ChinhSuaNhanVien_LUONG_PC_NULL_DIACHI();
                }
                if (SDTNHANSU.Text != "")
                {
                    ChinhSuaNhanVien_LUONG_PC_NULL_SDT();
                }
                if (VAITRONHANSU.Text != "")
                {
                    ChinhSuaNhanVien_LUONG_PC_NULL_VAITRO();
                }
                if (MANQLNHANSU.Text != "")
                {
                    ChinhSuaNhanVien_LUONG_PC_NULL_MANQL();
                }
                if (PHGNHANSU.Text != "")
                {
                    ChinhSuaNhanVien_LUONG_PC_NULL_PHG();
                }
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }
        //thực hiện quyền như là một nhân viên thông thường (vai trò “Nhân viên”).


        private void button4_Click(object sender, EventArgs e)
        {
            Form nhanvien = new NhanVien(TK, MK);
            nhanvien.ShowDialog();
            this.Close();
        }







        private void timkiemuserroletb_TextChanged(object sender, EventArgs e)
        {
        }

        private void ngaysinh_nv_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void textMAPB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
