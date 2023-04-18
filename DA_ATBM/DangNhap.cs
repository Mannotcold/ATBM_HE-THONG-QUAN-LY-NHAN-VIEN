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

                string role = "";
                string strSQL = "select GRANTED_ROLE from user_role_privs";
                OracleCommand oCmd = new OracleCommand(strSQL, con);
                OracleDataReader dr;
                dr = oCmd.ExecuteReader();
                dr.Read();
                role = dr["GRANTED_ROLE"].ToString();
                MessageBox.Show(role);
                //create role Ban_Giam_Doc;
                //create role Truong_Phong;
                //create role Tai_Chinh;
                //create role Nhan_Su;
                //create role Truong_De_An;
                //create role QL_Truc_Tiep;
                //create role Nhan_Vien;
                
                switch (role)
                {
                    case "BAN_GIAM_DOC":
                        Form quanLy = new QuanLy();
                        this.Hide();
                        quanLy.ShowDialog();
                        this.Close();
                        break;
                    case "TRUONG_PHONG":
                        break;
                    case "TAI_CHINH":
                        break;
                    case "NHAN_SU":
                        break;
                    case "TRUONG_DE_AN":
                        break;
                    case "QL_TRUC_TIEP":
                        break;
                    case "NHAN_VIEN":
                        break;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("Connection error please check your id or password");
            }
        }




        private void DangNhap_Load(object sender, EventArgs e)
        {
            
        }

        private void checkBoxShowPass_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBoxShowPass.Checked)
            {
                textBoxMK.UseSystemPasswordChar = false;
            }
            else textBoxMK.UseSystemPasswordChar = true;
        }
    }
}
