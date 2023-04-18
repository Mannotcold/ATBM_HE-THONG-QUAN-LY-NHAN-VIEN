using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_ATBM
{
    public partial class DangNhap : Form
    {
        OracleConnection con;
        public DangNhap()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxTK.Text == null)
            {
                MessageBox.Show("ID không được để trống");
                return;
            }
            if (textBoxMK.Text == null)
            {
                MessageBox.Show("Pass không được để trống");
                return;
            }
            Regex rgx = new Regex("[^A-Za-z0-9]");
            if (rgx.IsMatch(textBoxTK.Text))
            {
                MessageBox.Show("ID không được chứa ký tự đặc biệt");
                textBoxTK.Clear();
                return;
            }
            string conString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;" + "USER ID=" + textBoxTK.Text + ";PASSWORD=" + textBoxMK.Text + ";DATA SOURCE=localhost:1521/XE";
            try
            {
                OracleConnection con = new OracleConnection();
                con.ConnectionString = conString;
                con.Open();
                OracleCommand cmd_checkrole = new OracleCommand();
                string role = "";
                string strSQL = "select GRANTED_ROLE from user_role_privs";
              

                cmd_checkrole.CommandType = CommandType.Text;
                OracleCommand oCmd = new OracleCommand(strSQL, con);
                OracleDataReader dr;
                dr = oCmd.ExecuteReader();
                dr.Read();
                role = dr["GRANTED_ROLE"].ToString();
                MessageBox.Show(role);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Connection error please check your id or password");
            }
        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPass.Checked)
            {
                textBoxMK.UseSystemPasswordChar = false;
            }
            else textBoxMK.UseSystemPasswordChar = true;
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát khỏi chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
