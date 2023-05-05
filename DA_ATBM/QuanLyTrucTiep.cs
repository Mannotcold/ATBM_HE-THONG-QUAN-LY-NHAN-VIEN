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

namespace DA_ATBM
{
    public partial class QuanLyTrucTiep : Form
    {
        private string MK, TK;

        public QuanLyTrucTiep()
        {
            InitializeComponent();
        }

        public QuanLyTrucTiep(string tk, string mk)
        {
            InitializeComponent();
            this.TK = tk;
            this.MK = mk;
        }
        private void button1_qltt_Click(object sender, EventArgs e)
        {
            Xemthongtincanhan();
        }
        private void Xemthongtincanhan()
        {
            OracleConnection con_ttcn = new OracleConnection();
            con_ttcn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ttcn;
            cmd_ttcn = new OracleCommand("select * from quanly.nhanvien_v", con_ttcn);
            cmd_ttcn.CommandType = CommandType.Text;
            con_ttcn.Open();
            using (OracleDataReader reader = cmd_ttcn.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                thongtincanhan.DataSource = dataTable;
            }
            con_ttcn.Close();

        }
        

        private void button2_qltt_Click(object sender, EventArgs e)
        {
            Xemthongtinphancongcanhan();
        }

        private void Xemthongtinphancongcanhan()
        {
            OracleConnection con_ttpccn = new OracleConnection();
            con_ttpccn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ttpccn;
            cmd_ttpccn = new OracleCommand("select * from quanly.phancong_v", con_ttpccn);
            cmd_ttpccn.CommandType = CommandType.Text;
            con_ttpccn.Open();
            using (OracleDataReader reader = cmd_ttpccn.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                thongtinpccanhan.DataSource = dataTable;
            }
            con_ttpccn.Close();
        }

        private void button3_qltt_Click(object sender, EventArgs e)
        {
            Xemthongtinphongban();
        }
        private void Xemthongtinphongban()
        {
            OracleConnection con_ttpb = new OracleConnection();
            con_ttpb.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ttpb;
            cmd_ttpb = new OracleCommand("select * from quanly.phongban", con_ttpb);
            cmd_ttpb.CommandType = CommandType.Text;
            con_ttpb.Open();
            using (OracleDataReader reader = cmd_ttpb.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                thongtinphongban.DataSource = dataTable;
            }
            con_ttpb.Close();
        }

        private void button4_qltt_Click(object sender, EventArgs e)
        {
            Xemthongtindean();
        }

        private void Xemthongtindean()
        {
            OracleConnection con_ttdean = new OracleConnection();
            con_ttdean.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ttdean;
            cmd_ttdean = new OracleCommand("select * from quanly.dean", con_ttdean);
            cmd_ttdean.CommandType = CommandType.Text;
            con_ttdean.Open();
            using (OracleDataReader reader = cmd_ttdean.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                thongtindean.DataSource = dataTable;
            }
            con_ttdean.Close();
        }

        private void button5_qlttiep_Click(object sender, EventArgs e)
        {
            Chinhsuangaysinhnv();
        }

        private void Chinhsuangaysinhnv()
        {
            OracleConnection con_csngs = new OracleConnection();
            con_csngs.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csngs.Open();
            OracleCommand cmd_csngs;
            OracleCommand cmd_commit;
            cmd_csngs = new OracleCommand("update quanly.nhanvienupdate_v set ngaysinh = date'" + ngaysinh_nv.Text.ToUpper() + "'", con_csngs);
            cmd_csngs.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csngs);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csngs.ExecuteNonQuery();
            MessageBox.Show("UPDATE ngày sinh thành công");
            con_csngs.Close();
        }

        private void button6_qlttiep_Click(object sender, EventArgs e)
        {
            Chinhsuadiachinv();
        }

        private void Chinhsuadiachinv()
        {
            OracleConnection con_csdc = new OracleConnection();
            con_csdc.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_csdc.Open();
            OracleCommand cmd_csdc;
            OracleCommand cmd_commit;
            cmd_csdc = new OracleCommand("update quanly.nhanvienupdate_v set diachi = '" + diachi_nv.Text.ToUpper() + "'", con_csdc);
            cmd_csdc.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_csdc);
            cmd_commit.CommandType = CommandType.Text;
            cmd_csdc.ExecuteNonQuery();
            MessageBox.Show("UPDATE địa chỉ thành công");
            con_csdc.Close();
        }
        private void button7_nv_Click(object sender, EventArgs e)
        {
            Chinhsuasodtnv();
        }



        private void Chinhsuasodtnv()
        {
            OracleConnection con_cssdt = new OracleConnection();
            con_cssdt.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            con_cssdt.Open();
            OracleCommand cmd_cssdt;
            OracleCommand cmd_commit;
            cmd_cssdt = new OracleCommand("update quanly.nhanvienupdate_v set sodt = '" + sodienthoai_nv.Text.ToUpper() + "'", con_cssdt);
            cmd_cssdt.CommandType = CommandType.Text;
            cmd_commit = new OracleCommand("commit", con_cssdt);
            cmd_commit.CommandType = CommandType.Text;
            cmd_cssdt.ExecuteNonQuery();
            MessageBox.Show("UPDATE số điện thoại thành công");
            con_cssdt.Close();
        }

        private void button8_qltt_Click(object sender, EventArgs e)
        {
            Xemdanhsachcacnhanvien();
        }

       

        private void Xemdanhsachcacnhanvien()
        {
            OracleConnection con_ttcnv = new OracleConnection();
            con_ttcnv.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_cnv;
            cmd_cnv = new OracleCommand("SELECT * FROM QUANLY.nhanvienquanly_v", con_ttcnv);
            cmd_cnv.CommandType = CommandType.Text;
            con_ttcnv.Open();
            using (OracleDataReader reader = cmd_cnv.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                thongtincacnhanvien.DataSource = dataTable;
            }
            con_ttcnv.Close();
        }

        private void button9_qltt_Click(object sender, EventArgs e)
        {
            Xemdanhsachcacnhanvienpc();
        }

        private void Xemdanhsachcacnhanvienpc()
        {
            OracleConnection con_ttcnvpc = new OracleConnection();
            con_ttcnvpc.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_cnvpc;
            cmd_cnvpc = new OracleCommand("SELECT * FROM QUANLY.NHANVIENQLPHANCONG_V", con_ttcnvpc);
            cmd_cnvpc.CommandType = CommandType.Text;
            con_ttcnvpc.Open();
            using (OracleDataReader reader = cmd_cnvpc.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                thongtincacnhanvienpc.DataSource = dataTable;
            }
            con_ttcnvpc.Close();
        }
    }
}
