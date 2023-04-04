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
    public partial class Form1 : Form
    {
        OracleConnection con;
        public Form1()
        {
            InitializeComponent();
        }

        //Hiển thị danh sách user/role
        private void button1_Click(object sender, EventArgs e)
        {
            
            DanhSachUser();

        }

        private void DanhSachUser()
        {
            OracleConnection con_ds = new OracleConnection();
            con_ds.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID = SYSTEM;PASSWORD=Man2082002@ ;Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ds;
            if (timkiemuserroletb.Text == "")
                cmd_ds = new OracleCommand("Select * from all_users", con_ds);
            else
                cmd_ds = new OracleCommand("Select * from all_users where username = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);
            cmd_ds.CommandType = CommandType.Text;
            con_ds.Open();
            using (OracleDataReader reader = cmd_ds.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                danhsachuserdg.DataSource = dataTable;
            }

            //danh sách các role 
            OracleCommand cmd_role;
            if (timkiemuserroletb.Text == "")
                cmd_role = new OracleCommand("Select * from dba_roles", con_ds);
            else
                cmd_role = new OracleCommand("Select * from dba_roles where role = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);

            using (OracleDataReader reader = cmd_role.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                danhsachroledg.DataSource = dataTable;
            }

            //danh sách role đã cấp cho user
            OracleCommand cmd_userrole;
            if (timkiemuserroletb.Text == "")
                cmd_userrole = new OracleCommand("Select * from dba_ROLE_PRIVS", con_ds);
            else
            {
                if (CheckUser(timkiemuserroletb.Text.ToUpper()) != 0) //có tồn tại user
                    cmd_userrole = new OracleCommand("Select * from dba_ROLE_PRIVS where granted_role<>'CONNECT' and grantee = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);
                else //có tồn tại role, nếu không thì không có giá trị trả về
                    cmd_userrole = new OracleCommand("Select * from dba_ROLE_PRIVS where granted_role<>'CONNECT' and granted_role = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);

            }


            using (OracleDataReader reader = cmd_userrole.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                danhsachuserroledg.DataSource = dataTable;
            }
            con_ds.Close();
        }

        //kiểm tra user có tồn tại
        private int CheckUser(string username)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=SYSTEM;PASSWORD=Man2082002@;Connection Timeout=10;";
            con.Open();
            OracleCommand cmd_checkuser = new OracleCommand();
            cmd_checkuser.Connection = con;
            cmd_checkuser.CommandText = "select count(*) from all_users where username = '" + username + "'";
            cmd_checkuser.CommandType = CommandType.Text;
            object count = cmd_checkuser.ExecuteScalar();
            if (count.ToString() == "0")
                return 0; //chưa tồn tại
            return 1; //đã tồn tại 
        }



        //Thông tin quyền của user
        private void ThongTinQuyen()
        {
            OracleConnection con_ttq = new OracleConnection();
            con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=SYSTEM;PASSWORD=Man2082002@;Connection Timeout=120;";

            DataSet dataSet_ttq = new DataSet();
            OracleCommand cmd_ttq;
            if (timkiemusertb.Text == "")
                cmd_ttq = new OracleCommand("Select owner, grantee, privilege, table_name, grantable from user_tab_privs ", con_ttq);
            else
                cmd_ttq = new OracleCommand("Select  owner, grantee, privilege, table_name, grantable from user_tab_privs  WHERE owner = '" + timkiemusertb.Text.ToUpper() + "'", con_ttq);
            //dba_sys_privs 
            //user_tab_privs
            cmd_ttq.CommandType = CommandType.Text;
            con_ttq.Open();
            using (OracleDataReader reader = cmd_ttq.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                thongtinquyendg.DataSource = dataTable;
            }

            con_ttq.Close();
        }
        //Thông tin quyền của roles
        private void ThongTinQuyenRoles()
        {
            OracleConnection con_ttq = new OracleConnection();
            con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=SYSTEM;PASSWORD=Man2082002@;Connection Timeout=120;";

            DataSet dataSet_ttq = new DataSet();
            OracleCommand cmd_ttq;
            if (timkiemusertb.Text == "")
                cmd_ttq = new OracleCommand("SELECT grantee, privilege, owner, table_name, grantable FROM dba_tab_privs WHERE grantee = '" + timkiemroles.Text.ToUpper() + "' AND grantee IN(SELECT role FROM dba_roles)", con_ttq);
            else
                cmd_ttq = new OracleCommand("Select  owner, grantee, privilege, table_name, grantable from user_tab_privs  WHERE grantee = '" + timkiemroles.Text.ToUpper() + "'", con_ttq);
            //dba_sys_privs 
            //user_tab_privs
            cmd_ttq.CommandType = CommandType.Text;
            con_ttq.Open();
            using (OracleDataReader reader = cmd_ttq.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                thongtinquyendg.DataSource = dataTable;
            }

            con_ttq.Close();
        }
        private void timkiemrole_Click(object sender, EventArgs e)
        {
            ThongTinQuyenRoles();
        }

        private void timkiemuserbtn_Click(object sender, EventArgs e)
        {
            ThongTinQuyen();
        }


        private void XemBangCua1TaiKhoan()
        {
            //Xem bảng 

            ////DBA PRIVILEGE = SYSDBA; TNS_ADMIN = C:\Users\ACER\Oracle\network\admin; USER ID = SYS; DATA SOURCE = localhost:1521 / XE
            ////string conStr = "DATA SOURCE = localhost:1521 / XE; USER ID = SYSDBA;PASSWORD=Man2082002@";
            //string conStr = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID = GIAMDOC;PASSWORD=123 ;Connection Timeout=120;";

            //con = new OracleConnection(conStr);
            ////MessageBox.Show("888jhhh8559");
            //con.Open();
            //OracleCommand getEmps = con.CreateCommand();
            //getEmps.CommandText = "Select * FROM SYS.NHANVIEN";
            //getEmps.CommandType = CommandType.Text;
            //OracleDataReader empDR = getEmps.ExecuteReader();
            //DataTable empDT = new DataTable();
            //empDT.Load(empDR);
            //danhsachuserroledg.DataSource = empDT;
            //con.Close();

            //OracleConnection con_ds = new OracleConnection();
            //con_ds.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID = SYSTEM;PASSWORD=Man2082002@ ;Connection Timeout=120;";
            //DataSet dataSet_ds = new DataSet();
            //OracleCommand cmd_ds;
            //if (timkiemuserroletb.Text == "")
            //    cmd_ds = new OracleCommand("Select * from all_users", con_ds);
            //else
            //    cmd_ds = new OracleCommand("Select * from all_users where username = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);
            //cmd_ds.CommandType = CommandType.Text;
            //con_ds.Open();
            //using (OracleDataReader reader = cmd_ds.ExecuteReader())
            //{
            //    DataTable dataTable = new DataTable();
            //    dataTable.Load(reader);
            //    //danhsachuserdg = null;
            //    danhsachuserdg.DataSource = dataTable;
            //}

            ////danh sách các role 
            //OracleCommand cmd_role;
            //if (timkiemuserroletb.Text == "")
            //    cmd_role = new OracleCommand("SELECT * FROM dba_roles", con_ds);
            //else
            //    cmd_role = new OracleCommand("SELECT * FROM dba_roles where role = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);

            //using (OracleDataReader reader = cmd_role.ExecuteReader())
            //{
            //    DataTable dataTable = new DataTable();
            //    dataTable.Load(reader);
            //    danhsachroledg.DataSource = dataTable;
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
