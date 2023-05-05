using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DA_ATBM
{
    public partial class TruongDeAn : Form
    {
        public TruongDeAn()
        {
            InitializeComponent();
        }
        private string MK, TK;
        public TruongDeAn(string tk, string mk)
        {
            InitializeComponent();
            this.TK = tk;
            this.MK = mk;
        }


        private void DanhSachDeAN()
        {
            OracleConnection con_ds = new OracleConnection();
            con_ds.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ds;
            if (timkiemuserroletb.Text == "")
                cmd_ds = new OracleCommand("Select * from quanly.TDA_DEAN", con_ds);
            else
                cmd_ds = new OracleCommand("Select * from quanly.TDA_DEAN where MaDA = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);
            cmd_ds.CommandType = CommandType.Text;
            con_ds.Open();
            using (OracleDataReader reader = cmd_ds.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                danhsachdeangv.DataSource = dataTable;
            }
            con_ds.Close();
        }
        private void TimKiem_Click(object sender, EventArgs e)
        {
            DanhSachDeAN();
        }

        private void Them_Click(object sender, EventArgs e)
        {
            CountMADA();
            DialogResult rs = MessageBox.Show("Bạn có muốn thêm hay không?", "Thêm đề án", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
               
                try
                {
                    OracleConnection con_ttq = new OracleConnection();
                    con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";


                    DataSet dataSet_ttq = new DataSet();
                    OracleCommand cmd_ttq;
                    cmd_ttq = new OracleCommand("insert into quanly.TDA_DEAN (MADA, TENDA, NGAYBD, PHONG) values ('" + MADA + "','" + textBox3.Text + "',date'" + dateTimePicker1.Text + "','" + textBox2.Text + "')", con_ttq);

                    cmd_ttq.CommandType = CommandType.Text;
                    con_ttq.Open();
                    OracleDataReader reader = cmd_ttq.ExecuteReader();
                    MessageBox.Show("Thêm đề án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DanhSachDeAN();
                    con_ttq.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Thêm đề án không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    DanhSachDeAN();
                }
            }
            else
            {

            }
        }

        private void Capnhat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn cập nhật hay không", "Cập nhật đề án", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                try
                {
                    OracleConnection con_ttq = new OracleConnection();
                    con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";

                    DataSet dataSet_ttq = new DataSet();
                    OracleCommand cmd_ttq;
                    cmd_ttq = new OracleCommand("update quanly.TDA_DEAN set TENDA = '" + textBox3.Text + "', PHONG = '" + textBox2.Text + "', NGAYBD = date'" + dateTimePicker1.Text + "' where MaDA = '" + textBox1.Text + "'", con_ttq);

                    cmd_ttq.CommandType = CommandType.Text;
                    con_ttq.Open();
                    using (OracleDataReader reader = cmd_ttq.ExecuteReader())
                    {
                        MessageBox.Show("Cập nhật đề án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    DanhSachDeAN();
                    con_ttq.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Cập nhật đề án không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    DanhSachDeAN();
                }
                
            }
            else
            {
                
            }
        }

        private void danhsachdeangv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = danhsachdeangv.CurrentRow.Index;
            textBox1.Text = danhsachdeangv.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = danhsachdeangv.Rows[i].Cells[3].Value.ToString();
            textBox3.Text = danhsachdeangv.Rows[i].Cells[1].Value.ToString();
            dateTimePicker1.Text = danhsachdeangv.Rows[i].Cells[2].Value.ToString();
        }
        string MADA;
        private void CountMADA()
        {
            OracleConnection con_ttq = new OracleConnection();
            con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";

            DataSet dataSet_ttq = new DataSet();
            OracleCommand cmd_ttq;
            cmd_ttq = new OracleCommand("select COUNT(*) from quanly.TDA_DEAN", con_ttq);

            cmd_ttq.CommandType = CommandType.Text;
            con_ttq.Open();
            using (OracleDataReader reader = cmd_ttq.ExecuteReader())
            {
                while (reader.Read())
                {
                    int mada = reader.GetInt32(0) + 1;
                    MADA = "DA0" + mada.ToString();
                }

            }
            con_ttq.Close();
        }
        private void TruongDeAn_Load(object sender, EventArgs e)
        {
            
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn cập nhật hay không", "Cập nhật đề án", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                try
                {
                    OracleConnection con_ttq = new OracleConnection();
                    con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";

                    DataSet dataSet_ttq = new DataSet();
                    OracleCommand cmd_ttq;
                    cmd_ttq = new OracleCommand("delete from quanly.TDA_DEAN where MaDA = '" + textBox1.Text + "'", con_ttq);

                    cmd_ttq.CommandType = CommandType.Text;
                    con_ttq.Open();
                    using (OracleDataReader reader = cmd_ttq.ExecuteReader())
                    {
                        MessageBox.Show("Xoa đề án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    DanhSachDeAN();
                    con_ttq.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Xóa đề án không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    DanhSachDeAN();
                }

            }
            else
            {

            }
        }

        private void Thoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát không", "Thoát", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {

                this.Close();

            }
            else
            {

            }
        }

        private void Lammoi_Click(object sender, EventArgs e)
        {
            timkiemuserroletb.Text = null;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            
        }

    }
}
