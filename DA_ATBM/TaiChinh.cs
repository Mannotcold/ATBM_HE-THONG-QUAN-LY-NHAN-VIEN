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
    public partial class TaiChinh : Form
    {
        public TaiChinh()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DanhSachNhanVien();
        }

        private void DanhSachNhanVien()
        {
            OracleConnection con_ds = new OracleConnection();
            con_ds.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID = QUANLY;PASSWORD=12345 ;Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ds;
            if (timkiemuserroletb.Text == "")
                cmd_ds = new OracleCommand("Select * from NHANVIEN", con_ds);
            else
                cmd_ds = new OracleCommand("Select * from NHANVIEN where MaNV = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);
            cmd_ds.CommandType = CommandType.Text;
            con_ds.Open();
            using (OracleDataReader reader = cmd_ds.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                danhsachuserdg.DataSource = dataTable;
            }
            con_ds.Close();
        }
    }
}
