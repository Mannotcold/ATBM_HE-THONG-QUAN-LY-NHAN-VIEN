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
            DialogResult rs = MessageBox.Show("Bạn có muốn thêm hay không?", "Thêm đề án", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {

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
                OracleConnection con_ttq = new OracleConnection();
                con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";

                DataSet dataSet_ttq = new DataSet();
                OracleCommand cmd_ttq;
                cmd_ttq = new OracleCommand("update quanly.TDA_DEAN set TENDA = '" + textBox3.Text + "', PHONG = '" + textBox2.Text + "', NGAYBD = '" + dateTimePicker1.Text + "' where MaDA = '" + textBox1.Text + "'", con_ttq);

                cmd_ttq.CommandType = CommandType.Text;
                con_ttq.Open();
                using (OracleDataReader reader = cmd_ttq.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    danhsachdeangv.DataSource = dataTable;
                    int kq = cmd_ttq.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Cập nhật thành công! ");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại! .");
                    }
                }
                DanhSachDeAN();
                con_ttq.Close();
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
