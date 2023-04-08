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
        private int CheckView(string viewname)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-E896G02)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl)));;User ID=QuanLyLopHoc;Password=123;Connection Timeout=120;";
            con.Open();
            OracleCommand cmd_checkview = new OracleCommand();
            cmd_checkview.Connection = con;
            cmd_checkview.CommandText = "SELECT COUNT(*) FROM dba_views WHERE view_name = '" + viewname + "'";
            cmd_checkview.CommandType = CommandType.Text;
            object count = cmd_checkview.ExecuteScalar();
            if (count.ToString() == "0")
                return 0; //chưa tồn tại
            return 1; //đã tồn tại 
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

        private int CheckUser(string username)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=SYSTEM;PASSWORD=abcd123;Connection Timeout=10;";
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

        private int CheckRole(string rolename)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=SYSTEM;Password=abcd123;Connection Timeout=120;";
            con.Open();
            OracleCommand cmd_checkrole = new OracleCommand();
            cmd_checkrole.Connection = con;
            cmd_checkrole.CommandText = "select count(*) from DBA_ROLES where role = '" + rolename + "'";
            cmd_checkrole.CommandType = CommandType.Text;
            object count = cmd_checkrole.ExecuteScalar();
            if (count.ToString() == "0")
                return 0; //chưa tồn tại
            return 1; //đã tồn tại 
        }
        private void nhavienrb_CheckedChanged(object sender, EventArgs e)
        {
            NHANVIEN.Enabled = true;
            PHONGBAN.Enabled = false;
            DEAN.Enabled = false;
            PHANCONG.Enabled = false;
        }

        private void phongban_CheckedChanged(object sender, EventArgs e)
        {
            NHANVIEN.Enabled = false;
            PHONGBAN.Enabled = true;
            DEAN.Enabled = false;
            PHANCONG.Enabled = false;
        }

        private void dean_CheckedChanged(object sender, EventArgs e)
        {
            NHANVIEN.Enabled = false;
            PHONGBAN.Enabled = false;
            DEAN.Enabled = true;
            PHANCONG.Enabled = false;
        }

        private void phancong_CheckedChanged(object sender, EventArgs e)
        {
            NHANVIEN.Enabled = false;
            PHONGBAN.Enabled = false;
            DEAN.Enabled = false;
            PHANCONG.Enabled = true;
        }
        private void insertbtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            if (capquyenrolerb.Checked)
            {
                if (CheckRole(tenuserroletb.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("Role không tồn tại.");
                    con.Close();
                    return;

                }
            }
            else
            {
                if (CheckUser(tenuserroletb.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("User không tồn tại.");
                    con.Close();
                    return;
                }
            }
            con.Open();
            if (nhanvienrb.Checked) //insert trên bảng nhanvien
            {
                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant insert on sys.NHANVIEN to " + tenuserroletb.Text.ToUpper();
                /*if (wgo.Checked)//có with grant option
                {
                    cmd_insert.CommandText += " WITH GRANT OPTION";
                }*/
                cmd_insert.CommandType = CommandType.Text;
                cmd_insert.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền INSERT thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (phongbanrb.Checked) //insert trên bảng phongban
            {

                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant insert on sys.PHONGBAN to " + tenuserroletb.Text.ToUpper();
                MessageBox.Show(tenuserroletb.Text.ToUpper());
                if (wgo.Checked)//có with grant option
                {
                    cmd_insert.CommandText += " WITH GRANT OPTION";
                }
                cmd_insert.CommandType = CommandType.Text;
                cmd_insert.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền INSERT thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (deanrb.Checked) //insert trên bảng dean
            {
                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant insert on sys.DEAN to " + tenuserroletb.Text.ToUpper();
                if (wgo.Checked)//có with grant option
                {
                    cmd_insert.CommandText += " WITH GRANT OPTION";
                }
                cmd_insert.CommandType = CommandType.Text;
                cmd_insert.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền INSERT thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (phancongrb.Checked) //insert trên bảng phancong
            {
                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant insert on sys.PHANCONG to " + tenuserroletb.Text.ToUpper();
                if (wgo.Checked)//có with grant option
                {
                    cmd_insert.CommandText += " WITH GRANT OPTION";
                }
                cmd_insert.CommandType = CommandType.Text;
                cmd_insert.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền INSERT thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }

            con.Close();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            if (capquyenrolerb.Checked)
            {
                if (CheckRole(tenuserroletb.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("Role không tồn tại.");
                    con.Close();
                    return;

                }
            }
            else
            {
                if (CheckUser(tenuserroletb.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("User không tồn tại.");
                    con.Close();
                    return;
                }
            }
            con.Open();
            if (nhanvienrb.Checked) //delete trên bảng nhanvien
            {
                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant delete on sys.NHANVIEN to " + tenuserroletb.Text.ToUpper();
                if (wgo.Checked)//có with grant option
                {
                    cmd_insert.CommandText += " WITH GRANT OPTION";
                }
                cmd_insert.CommandType = CommandType.Text;
                cmd_insert.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền delete thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (phongbanrb.Checked) //delete trên bảng phongban
            {

                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant delete on sys.PHONGBAN to " + tenuserroletb.Text.ToUpper();
                MessageBox.Show(tenuserroletb.Text.ToUpper());
                if (wgo.Checked)//có with grant option
                {
                    cmd_insert.CommandText += " WITH GRANT OPTION";
                }
                cmd_insert.CommandType = CommandType.Text;
                cmd_insert.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền delete thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (deanrb.Checked) //delete trên bảng dean
            {
                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant delete on sys.DEAN to " + tenuserroletb.Text.ToUpper();
                if (wgo.Checked)//có with grant option
                {
                    cmd_insert.CommandText += " WITH GRANT OPTION";
                }
                cmd_insert.CommandType = CommandType.Text;
                cmd_insert.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền delete thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (phancongrb.Checked) //delete trên bảng phancong
            {
                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant delete on sys.PHANCONG to " + tenuserroletb.Text.ToUpper();
                if (wgo.Checked)//có with grant option
                {
                    cmd_insert.CommandText += " WITH GRANT OPTION";
                }
                cmd_insert.CommandType = CommandType.Text;
                cmd_insert.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền delete thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }

            con.Close();
        }


        private void selectbtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";

            if (capquyenrolerb.Checked)
            {
                if (CheckRole(tenuserroletb.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("Role không tồn tại.");
                    con.Close();
                    return;
                }
            }
            else
            {
                if (CheckUser(tenuserroletb.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("User không tồn tại.");
                    con.Close();
                    return;
                }
            }
            con.Open();
            if (nhanvienrb.Checked) //select trên bảng nhanvien
            {
                OracleCommand cmd_select = new OracleCommand();
                cmd_select.Connection = con;
                if (NHANVIEN.CheckedItems.Count == 0 || NHANVIEN.CheckedItems.Count == 11) //cấp quyền select trên cả bảng
                {
                    cmd_select.CommandText = "grant select on sys.NHANVIEN to " + tenuserroletb.Text.ToUpper();
                }
                else
                {
                    //những cột viết tắt để tạo view không bị lỗi view quá dài
                    string[] short_name = { "MNV", "TNV", "GT", "NGS", "DC", "SDT", "LG", "PC", "VT", "MQL", "PHG"};
                    //MVN: MANV, TNV: TENNV, GT: PHAI, NGS:NGAYSINH, DC: DIACHI, STD: SODT, PC: PHUCAP, VT: VAITRO, MQL: MANQL
                    //kiểm tra cấp quyền select trên những cột nào
                    string column = "";
                    //chọn ra những cột để select
                    string select_column = "";
                    for (int i = 0; i < NHANVIEN.Items.Count; i++)
                    {
                        if (NHANVIEN.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                            select_column += NHANVIEN.Items[i].ToString() + ",";
                        }
                    }
                    //xóa dấu , ở cuối (để bỏ váo câu lệnh select)
                    select_column = select_column.TrimEnd(',');

                    //them NV vào cuối để biết là view từ bàng NHANVIEN
                    column += "NV";

                    //kiểm tra view có tồn tại hay chưa
                    if (CheckView(column) == 0)//chưa tồn tại
                    {
                        //tạo view mới
                        OracleCommand cmd_taoview = new OracleCommand();
                        cmd_taoview.Connection = con;
                        cmd_taoview.CommandText = "create view " + column + " as select " + select_column + " from NHANVIEN";
                        cmd_taoview.CommandType = CommandType.Text;
                        cmd_taoview.ExecuteNonQuery();
                    }
                    //cấp quyền đọc trên view này cho user
                    cmd_select.CommandText = "grant select on " + column + " to " + tenuserroletb.Text.ToUpper();
                }

                if (wgo.Checked)//có with grant option
                {
                    cmd_select.CommandText += " WITH GRANT OPTION";
                }
                cmd_select.CommandType = CommandType.Text;
                cmd_select.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền select thành công");

                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if(phongbanrb.Checked) //select trên bảng phong ban
            {
                OracleCommand cmd_select = new OracleCommand();
                cmd_select.Connection = con;

                if (PHONGBAN.CheckedItems.Count == 0 || PHONGBAN.CheckedItems.Count == 3) //cấp quyền select trên cả bảng
                {
                    cmd_select.CommandText = "grant select on sys.PHONGBAN to " + tenuserroletb.Text.ToUpper();
                }
                else
                {
                    //kiểm tra cấp quyền select trên những cột nào
                    //những cột viết tắt để tạo view không bị lỗi view quá dài
                    string[] short_name = { "MPB", "TPB", "TRPHG" };
                    //MPB: MAPB, TPB: TENPB
                    //kiểm tra cấp quyền select trên những cột nào
                    string column = "";
                    //chọn ra những cột để select
                    string select_column = "";
                    for (int i = 0; i < PHONGBAN.Items.Count; i++)
                    {
                        if (PHONGBAN.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                            select_column += PHONGBAN.Items[i].ToString() + ",";
                        }
                    }
                    //xóa dấu , ở cuối (để bỏ váo câu lệnh select)
                    select_column = select_column.TrimEnd(',');

                    //them  vào cuối để biết là view từ bàng PHONGBAN
                    column += "PB";

                    //kiểm tra view có tồn tại?
                    if (CheckView(column) == 0)//chưa tồn tại
                    {
                        //tạo view mới
                        OracleCommand cmd_taoview = new OracleCommand();
                        cmd_taoview.Connection = con;
                        cmd_taoview.CommandText = "create view " + column + " as select " + select_column + " from LOP";
                        cmd_taoview.CommandType = CommandType.Text;
                        cmd_taoview.ExecuteNonQuery();
                    }
                    cmd_select.CommandText = "grant select on " + column + " to " + tenuserroletb.Text.ToUpper();
                }
                if (wgo.Checked)//có with grant option
                {
                    cmd_select.CommandText += " WITH GRANT OPTION";
                }
                cmd_select.CommandType = CommandType.Text;
                cmd_select.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền select thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (deanrb.Checked) //select trên bảng de an
            {
                OracleCommand cmd_select = new OracleCommand();
                cmd_select.Connection = con;

                if (DEAN.CheckedItems.Count == 0 || DEAN.CheckedItems.Count == 4) //cấp quyền select trên cả bảng
                {
                    cmd_select.CommandText = "grant select on sys.DEAN to " + tenuserroletb.Text.ToUpper();
                }
                else
                {
                    //kiểm tra cấp quyền select trên những cột nào
                    //những cột viết tắt để tạo view không bị lỗi view quá dài
                    string[] short_name = { "MDA", "TDA", "NGBD", "PG" };
                    //MDA: MADA, TDA:TENDA, NGBD: NGAYBD, PG: PHONG
                    //kiểm tra cấp quyền select trên những cột nào
                    string column = "";
                    //chọn ra những cột để select
                    string select_column = "";
                    for (int i = 0; i < DEAN.Items.Count; i++)
                    {
                        if (DEAN.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                            select_column += DEAN.Items[i].ToString() + ",";
                        }
                    }
                    //xóa dấu , ở cuối (để bỏ váo câu lệnh select)
                    select_column = select_column.TrimEnd(',');

                    //them  vào cuối để biết là view từ bàng PHONGBAN
                    column += "DA";

                    //kiểm tra view có tồn tại?
                    if (CheckView(column) == 0)//chưa tồn tại
                    {
                        //tạo view mới
                        OracleCommand cmd_taoview = new OracleCommand();
                        cmd_taoview.Connection = con;
                        cmd_taoview.CommandText = "create view " + column + " as select " + select_column + " from DEAN";
                        cmd_taoview.CommandType = CommandType.Text;
                        cmd_taoview.ExecuteNonQuery();
                    }
                    cmd_select.CommandText = "grant select on " + column + " to " + tenuserroletb.Text.ToUpper();
                }
                if (wgo.Checked)//có with grant option
                {
                    cmd_select.CommandText += " WITH GRANT OPTION";
                }
                cmd_select.CommandType = CommandType.Text;
                cmd_select.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền select thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (phancongrb.Checked) //select trên bảng phan cong
            {
                OracleCommand cmd_select = new OracleCommand();
                cmd_select.Connection = con;

                if (PHONGBAN.CheckedItems.Count == 0 || PHONGBAN.CheckedItems.Count == 5) //cấp quyền select trên cả bảng
                {
                    cmd_select.CommandText = "grant select on sys.PHANCONG to " + tenuserroletb.Text.ToUpper();
                }
                else
                {
                    //kiểm tra cấp quyền select trên những cột nào
                    //những cột viết tắt để tạo view không bị lỗi view quá dài
                    string[] short_name = { "MNV", "MDA", "TG" };
                    //MPB: MNV: MANV, MDA: MADA, TG: THOIGIAN
                    //kiểm tra cấp quyền select trên những cột nào
                    string column = "";
                    //chọn ra những cột để select
                    string select_column = "";
                    for (int i = 0; i < PHANCONG.Items.Count; i++)
                    {
                        if (PHANCONG.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                            select_column += PHANCONG.Items[i].ToString() + ",";
                        }
                    }
                    //xóa dấu , ở cuối (để bỏ váo câu lệnh select)
                    select_column = select_column.TrimEnd(',');

                    //them  vào cuối để biết là view từ bàng PHONGBAN
                    column += "PC";

                    //kiểm tra view có tồn tại?
                    if (CheckView(column) == 0)//chưa tồn tại
                    {
                        //tạo view mới
                        OracleCommand cmd_taoview = new OracleCommand();
                        cmd_taoview.Connection = con;
                        cmd_taoview.CommandText = "create view " + column + " as select " + select_column + " from PHANCONG";
                        cmd_taoview.CommandType = CommandType.Text;
                        cmd_taoview.ExecuteNonQuery();
                    }
                    cmd_select.CommandText = "grant select on " + column + " to " + tenuserroletb.Text.ToUpper();
                }
                if (wgo.Checked)//có with grant option
                {
                    cmd_select.CommandText += " WITH GRANT OPTION";
                }
                cmd_select.CommandType = CommandType.Text;
                cmd_select.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền select thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            con.Close();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";

            if (capquyenrolerb.Checked)
            {
                if (CheckRole(tenuserroletb.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("Role không tồn tại.");
                    con.Close();
                    return;
                }
            }
            else
            {
                if (CheckUser(tenuserroletb.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("User không tồn tại.");
                    con.Close();
                    return;
                }
            }
            con.Open();
            if (nhanvienrb.Checked) //update trên bảng nhan vien
            {
                OracleCommand cmd_update = new OracleCommand();
                cmd_update.Connection = con;
                if (NHANVIEN.CheckedItems.Count == 0 || NHANVIEN.CheckedItems.Count == 11) //cấp quyền upate trên cả bảng
                {
                    cmd_update.CommandText = "grant update on sys.NHANVIEN to " + tenuserroletb.Text.ToUpper();
                }
                else
                {
                    //kiểm tra cấp quyền update trên những cột nào
                    string column = "";
                    for (int i = 0; i < NHANVIEN.Items.Count; i++)
                    {
                        if (NHANVIEN.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += NHANVIEN.Items[i].ToString() + ",";
                        }
                    }
                    //xóa dấu , ở cuối
                    column = column.TrimEnd(',');
                    cmd_update.CommandText = "grant update(" + column + ") on sys.NHANVIEN to " + tenuserroletb.Text.ToUpper();
                }

                if (wgo.Checked)//có with grant option
                {
                    cmd_update.CommandText += " WITH GRANT OPTION";
                }
                cmd_update.CommandType = CommandType.Text;
                cmd_update.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền update thành công");

                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if(phongbanrb.Checked) //update trên bảng phongban
            {
                OracleCommand cmd_update = new OracleCommand();
                cmd_update.Connection = con;

                if (PHONGBAN.CheckedItems.Count == 0 || PHONGBAN.CheckedItems.Count == 3) //cấp quyền upate trên cả bảng
                {
                    cmd_update.CommandText = "grant update on sys.PHONGBAN to " + tenuserroletb.Text.ToUpper();
                }
                else
                {
                    //kiểm tra cấp quyền update trên những cột nào
                    string column = "";
                    for (int i = 0; i < PHONGBAN.Items.Count; i++)
                    {
                        if (PHONGBAN.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += PHONGBAN.Items[i].ToString() + ",";
                        }
                    }
                    //xóa dấu , ở cuối
                  
                    
                    column = column.TrimEnd(',');
                 
                    cmd_update.CommandText = "grant update(" +column+ ") on sys.PHONGBAN to " + tenuserroletb.Text.ToUpper();
                }
                if (wgo.Checked)//có with grant option
                {
                    cmd_update.CommandText += " WITH GRANT OPTION";
                }
                cmd_update.CommandType = CommandType.Text;
                cmd_update.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền update thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (deanrb.Checked) //update trên bảng dean
            {
                OracleCommand cmd_update = new OracleCommand();
                cmd_update.Connection = con;

                if (DEAN.CheckedItems.Count == 0 || DEAN.CheckedItems.Count == 4) //cấp quyền upate trên cả bảng
                {
                    cmd_update.CommandText = "grant update on sys.DEAN to " + tenuserroletb.Text.ToUpper();
                }
                else
                {
                    //kiểm tra cấp quyền update trên những cột nào
                    string column = "";
                    for (int i = 0; i < DEAN.Items.Count; i++)
                    {
                        if (DEAN.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += DEAN.Items[i].ToString() + ",";
                        }
                    }
                    //xóa dấu , ở cuối
                    column = column.TrimEnd(',');
                    cmd_update.CommandText = "grant update(" + column + ") on sys.DEAN to " + tenuserroletb.Text.ToUpper();
                }
                if (wgo.Checked)//có with grant option
                {
                    cmd_update.CommandText += " WITH GRANT OPTION";
                }
                cmd_update.CommandType = CommandType.Text;
                cmd_update.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền update thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            if (phancongrb.Checked) //update trên bảng phancong
            {
                OracleCommand cmd_update = new OracleCommand();
                cmd_update.Connection = con;

                if (PHANCONG.CheckedItems.Count == 0 || PHANCONG.CheckedItems.Count == 3) //cấp quyền upate trên cả bảng
                {
                    cmd_update.CommandText = "grant update on sys.PHANCONG to " + tenuserroletb.Text.ToUpper();
                }
                else
                {
                    //kiểm tra cấp quyền update trên những cột nào
                    string column = "";
                    for (int i = 0; i < PHANCONG.Items.Count; i++)
                    {
                        if (PHANCONG.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += PHANCONG.Items[i].ToString() + ",";
                        }
                    }
                    //xóa dấu , ở cuối
                    column = column.TrimEnd(',');
                    cmd_update.CommandText = "grant update(" + column + ") on sys.PHANCONG to " + tenuserroletb.Text.ToUpper();
                }
                if (wgo.Checked)//có with grant option
                {
                    cmd_update.CommandText += " WITH GRANT OPTION";
                }
                cmd_update.CommandType = CommandType.Text;
                cmd_update.ExecuteNonQuery();
                MessageBox.Show("Cấp quyền update thành công");
                //load lại thong tin quyền
                ThongTinQuyen();
            }
            con.Close();
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tenuserrolelbl_Click(object sender, EventArgs e)
        {

        }
    }
}
