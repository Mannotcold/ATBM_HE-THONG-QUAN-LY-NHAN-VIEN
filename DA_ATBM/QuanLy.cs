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
    public partial class QuanLy : Form
    {
        OracleConnection con;
        public QuanLy()
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
            con_ds.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID = QUANLY;PASSWORD=12345 ;Connection Timeout=120;";
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

       



//Thông tin quyền của user
        private void ThongTinQuyen()
        {
            OracleConnection con_ttq = new OracleConnection();
            con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=QUANLY;PASSWORD=12345;Connection Timeout=120;";

            DataSet dataSet_ttq = new DataSet();
            OracleCommand cmd_ttq;
            if (timkiemusertb.Text == "")
                cmd_ttq = new OracleCommand("Select owner, grantee, privilege, table_name, grantable from user_tab_privs ", con_ttq);
            else
                cmd_ttq = new OracleCommand("Select  owner, grantee, privilege, table_name, grantable from user_tab_privs  WHERE grantee = '" + timkiemusertb.Text.ToUpper() + "'", con_ttq);
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
            con_ttq.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=QUANLY;PASSWORD=12345;Connection Timeout=120;";

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



//Thuc hiện cấp quyền trên toàn bảng

        //Kiemr tra view có tồn tại hay không
        private int CheckView(string viewname)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID=QuanLy;Password=12345;Connection Timeout=120;";
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


        //kiểm tra user có tồn tại
        private int CheckUser(string username)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QuanLy;Password=12345;Connection Timeout=10;";
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


        //kiểm tra roles có tồn tại
        private int CheckRole(string rolename)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QuanLy;Password=12345;Connection Timeout=120;";
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
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cấp quyền
            NHANVIEN.Enabled = true;
            PHONGBAN.Enabled = false;
            DEAN.Enabled = false;
            PHANCONG.Enabled = false;

            //Thu quyền
            NHANVIEN_TQ.Enabled = true;
            PHONGBAN_TQ.Enabled = false;
            DEAN_TQ.Enabled = false;
            PHANCONG_TQ.Enabled = false;

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
                cmd_insert.CommandText = "grant insert on NHANVIEN to " + tenuserroletb.Text.ToUpper();
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
            if (phongbanrb.Checked) //insert trên bảng phongban
            {

                OracleCommand cmd_insert = new OracleCommand();
                cmd_insert.Connection = con;
                cmd_insert.CommandText = "grant insert on PHONGBAN to " + tenuserroletb.Text.ToUpper();
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
                cmd_insert.CommandText = "grant insert on DEAN to " + tenuserroletb.Text.ToUpper();
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
                cmd_insert.CommandText = "grant insert on PHANCONG to " + tenuserroletb.Text.ToUpper();
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
                cmd_insert.CommandText = "grant delete on NHANVIEN to " + tenuserroletb.Text.ToUpper();
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
                cmd_insert.CommandText = "grant delete on PHONGBAN to " + tenuserroletb.Text.ToUpper();
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
                cmd_insert.CommandText = "grant delete on DEAN to " + tenuserroletb.Text.ToUpper();
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
                cmd_insert.CommandText = "grant delete on PHANCONG to " + tenuserroletb.Text.ToUpper();
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
                    cmd_select.CommandText = "grant select on NHANVIEN to " + tenuserroletb.Text.ToUpper();
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
                    cmd_select.CommandText = "grant select on PHONGBAN to " + tenuserroletb.Text.ToUpper();
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
                        cmd_taoview.CommandText = "create view " + column + " as select " + select_column + " from PHONGBAN";
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
                    cmd_select.CommandText = "grant select on DEAN to " + tenuserroletb.Text.ToUpper();
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

                    //them  vào cuối để biết là view từ bàng DEAN
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

                if (PHONGBAN.CheckedItems.Count == 0 || PHONGBAN.CheckedItems.Count == 3) //cấp quyền select trên cả bảng
                {
                    cmd_select.CommandText = "grant select on PHANCONG to " + tenuserroletb.Text.ToUpper();
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

                    //them  vào cuối để biết là view từ bàng PHANCONG
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
                    cmd_update.CommandText = "grant update on NHANVIEN to " + tenuserroletb.Text.ToUpper();
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
                    cmd_update.CommandText = "grant update(" + column + ") on NHANVIEN to " + tenuserroletb.Text.ToUpper();
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
                    cmd_update.CommandText = "grant update on PHONGBAN to " + tenuserroletb.Text.ToUpper();
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
                 
                    cmd_update.CommandText = "grant update(" +column+ ") on PHONGBAN to " + tenuserroletb.Text.ToUpper();
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
                    cmd_update.CommandText = "grant update on DEAN to " + tenuserroletb.Text.ToUpper();
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
                    cmd_update.CommandText = "grant update(" + column + ") on DEAN to " + tenuserroletb.Text.ToUpper();
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
                    cmd_update.CommandText = "grant update on PHANCONG to " + tenuserroletb.Text.ToUpper();
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
                    cmd_update.CommandText = "grant update(" + column + ") on PHANCONG to " + tenuserroletb.Text.ToUpper();
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





// Thuc hien thu quyền trên các bảng
        


        private void Revoke(string tablename, string privilege, string grantee)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            con.Open();
            OracleCommand cmd_revoke = new OracleCommand();
            cmd_revoke.Connection = con;
            cmd_revoke.CommandText = "revoke " + privilege + " on " + tablename + " from " + grantee;
            cmd_revoke.CommandType = CommandType.Text;
            cmd_revoke.ExecuteNonQuery();
            con.Close();
        }

        private int CheckPrivilege(string tablename, string privilege, string grantee)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            con.Open();

            if (privilege != "UPDATE")
            {
                OracleCommand cmd_checkprivilege = new OracleCommand();
                cmd_checkprivilege.Connection = con;
                cmd_checkprivilege.CommandText = "select count(*) from user_tab_privs where table_name = '" + tablename + "' and privilege = '" + privilege + "' and grantee='" + grantee + "'";
                cmd_checkprivilege.CommandType = CommandType.Text;
                object count = cmd_checkprivilege.ExecuteScalar();
                if (count.ToString() == "0")
                    return 0; //chưa tồn tại
                return 1; //đã tồn tại 
            }
            else
            {
                //kiểm tra có cấp quyền UPDATE trên toàn bảng
                OracleCommand cmd_checkprivilege1 = new OracleCommand();
                cmd_checkprivilege1.Connection = con;
                cmd_checkprivilege1.CommandText = "select count(*) from user_tab_privs where table_name = '" + tablename + "' and privilege = '" + privilege + "' and grantee='" + grantee + "'";
                cmd_checkprivilege1.CommandType = CommandType.Text;
                object count1 = cmd_checkprivilege1.ExecuteScalar();

                //kiểm tra có cấp quyền UPDATE trên mức cột
                OracleCommand cmd_checkprivilege2 = new OracleCommand();
                cmd_checkprivilege2.Connection = con;
                cmd_checkprivilege2.CommandText = "select count(*) FROM USER_COL_PRIVS_MADE where table_name = '" + tablename + "' and privilege = '" + privilege + "' and grantee='" + grantee + "'";
                cmd_checkprivilege2.CommandType = CommandType.Text;
                object count2 = cmd_checkprivilege2.ExecuteScalar();

                if (count1.ToString() == "0" && count2.ToString() == "0") //hoàn toàn chưa được cấp quyền UPDATE
                    return 0; //chưa tồn tại
                return 1; //đã tồn tại 
            }
        }


        private void tabPage3_Click(object sender, EventArgs e)
        {
            
        }


        private void NV1_CheckedChanged(object sender, EventArgs e)
        {
            NHANVIEN_TQ.Enabled = true;
            PHONGBAN_TQ.Enabled = false;
            DEAN_TQ.Enabled = false;
            PHANCONG_TQ.Enabled = false;
        }

        private void PB1_CheckedChanged(object sender, EventArgs e)
        {
            NHANVIEN_TQ.Enabled = false;
            PHONGBAN_TQ.Enabled = true;
            DEAN_TQ.Enabled = false;
            PHANCONG.Enabled = false;
        }

        private void DA1_CheckedChanged(object sender, EventArgs e)
        {
            NHANVIEN_TQ.Enabled = false;
            PHONGBAN_TQ.Enabled = false;
            DEAN_TQ.Enabled = true;
            PHANCONG_TQ.Enabled = false;
        }

        private void PC1_CheckedChanged(object sender, EventArgs e)
        {
            NHANVIEN_TQ.Enabled = false;
            PHONGBAN_TQ.Enabled = false;
            DEAN_TQ.Enabled = false;
            PHANCONG_TQ.Enabled = true;
        }


        private void selecttq_Click(object sender, EventArgs e)
        {
            if (thuquyenrole.Checked)
            {
                if (CheckRole(tenuserroletq.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("Role không tồn tại.");
                    //con.Close();
                    return;
                }
            }
            else
            {
                if (CheckUser(tenuserroletq.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("User không tồn tại.");
                    //con.Close();
                    return;
                }
            }

            if (NV1.Checked)//thu quyền SELECT trên bảng NHANVIEN
            {
                //những cột viết tắt để tạo view không bị lỗi view quá dài
                string[] short_name = { "MNV", "TNV", "GT", "NGS", "DC", "SDT", "LG", "PC", "VT", "MQL", "PHG" };
                //MVN: MANV, TNV: TENNV, GT: PHAI, NGS:NGAYSINH, DC: DIACHI, STD: SODT, PC: PHUCAP, VT: VAITRO, MQL: MANQL
                //kiểm tra cấp quyền select trên những cột nào
                string column = "";
                //chọn ra những cột để select
                string select_column = "";

                //nếu cấp quyền đọc trên toàn bảng
                if (NHANVIEN_TQ.CheckedItems.Count != 0 && NHANVIEN_TQ.CheckedItems.Count != 11)
                {
                    for (int i = 0; i < NHANVIEN_TQ.Items.Count; i++)
                    {
                        if (NHANVIEN_TQ.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                        }
                    }
                    //them HS vào cuối để biết là view từ bàng HS
                    column += "NV";
                }
                else
                    column = "NHANVIEN";
                //kiểm tra có cấp quyền select trên bảng column??
                if (CheckPrivilege(column, "SELECT", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke(column, "SELECT", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }

                ThongTinQuyen();
            }
            else if (PB1.Checked) //thu quyền SELECT trên bảng PHONGBAN
            {

                //kiểm tra cấp quyền select trên những cột nào
                //những cột viết tắt để tạo view không bị lỗi view quá dài
                string[] short_name = { "MPB", "TPB", "TRPHG" };
                //kiểm tra cấp quyền select trên những cột nào
                string column = "";
                if (PHONGBAN_TQ.CheckedItems.Count != 0 && PHONGBAN_TQ.CheckedItems.Count != 3)
                {
                    for (int i = 0; i < PHONGBAN_TQ.Items.Count; i++)
                    {
                        if (PHONGBAN_TQ.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                        }
                    }
                    //them  vào cuối để biết là view từ bàng Lop
                    column += "PB";
                }
                else
                    column = "PHONGBAN";

                //kiểm tra có cấp quyền select trên view column?
                if (CheckPrivilege(column, "SELECT", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke(column, "SELECT", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }
            else if (DA1.Checked) //thu quyền SELECT trên bảng PHONGBAN
            {

                //kiểm tra cấp quyền select trên những cột nào
                //những cột viết tắt để tạo view không bị lỗi view quá dài
                string[] short_name = { "MDA", "TDA", "NGBD", "PG" };
                //kiểm tra cấp quyền select trên những cột nào
                string column = "";
                if (DEAN_TQ.CheckedItems.Count != 0 && DEAN_TQ.CheckedItems.Count != 3)
                {
                    for (int i = 0; i < DEAN_TQ.Items.Count; i++)
                    {
                        if (DEAN_TQ.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                        }
                    }
                    //them  vào cuối để biết là view từ bàng Lop
                    column += "DA";
                }
                else
                    column = "DEAN";

                //kiểm tra có cấp quyền select trên view column?
                if (CheckPrivilege(column, "SELECT", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke(column, "SELECT", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }
            else if (PC1.Checked) //thu quyền SELECT trên bảng PHONGBAN
            {

                //kiểm tra cấp quyền select trên những cột nào
                //những cột viết tắt để tạo view không bị lỗi view quá dài
                string[] short_name = { "MNV", "MDA", "TG" };
                //kiểm tra cấp quyền select trên những cột nào
                string column = "";
                if (PHANCONG_TQ.CheckedItems.Count != 0 && PHANCONG_TQ.CheckedItems.Count != 3)
                {
                    for (int i = 0; i < PHANCONG_TQ.Items.Count; i++)
                    {
                        if (PHANCONG_TQ.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                        }
                    }
                    //them  vào cuối để biết là view từ bàng Lop
                    column += "PC";
                }
                else
                    column = "PHANCONG";

                //kiểm tra có cấp quyền select trên view column?
                if (CheckPrivilege(column, "SELECT", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke(column, "SELECT", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }

            //load lại thong tin quyền
            ThongTinQuyen();
        }

        private void updatetq_Click(object sender, EventArgs e)
        {
            if (thuquyenrole.Checked)
            {
                if (CheckRole(tenuserroletq.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("Role không tồn tại.");
                    //con.Close();
                    return;
                }
            }
            else
            {
                if (CheckUser(tenuserroletq.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("User không tồn tại.");
                    //con.Close();
                    return;
                }
            }

            if (NV1.Checked)//thu quyền SELECT trên bảng NHANVIEN
            {
                //những cột viết tắt để tạo view không bị lỗi view quá dài
                string[] short_name = { "MNV", "TNV", "GT", "NGS", "DC", "SDT", "LG", "PC", "VT", "MQL", "PHG" };
                //MVN: MANV, TNV: TENNV, GT: PHAI, NGS:NGAYSINH, DC: DIACHI, STD: SODT, PC: PHUCAP, VT: VAITRO, MQL: MANQL
                //kiểm tra cấp quyền select trên những cột nào
                string column = "";
                //chọn ra những cột để select
                string select_column = "";

                //nếu cấp quyền đọc trên toàn bảng
                if (NHANVIEN_TQ.CheckedItems.Count != 0 && NHANVIEN_TQ.CheckedItems.Count != 11)
                {
                    for (int i = 0; i < NHANVIEN_TQ.Items.Count; i++)
                    {
                        if (NHANVIEN_TQ.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                        }
                    }
                    //them HS vào cuối để biết là view từ bàng HS
                    column += "NV";
                }
                else
                    column = "NHANVIEN";
                //kiểm tra có cấp quyền select trên bảng column??
                if (CheckPrivilege(column, "UPDATE", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke(column, "UPDATE", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }

                ThongTinQuyen();
            }
            else if (PB1.Checked) //thu quyền SELECT trên bảng PHONGBAN
            {

                //kiểm tra cấp quyền select trên những cột nào
                //những cột viết tắt để tạo view không bị lỗi view quá dài
                string[] short_name = { "MPB", "TPB", "TRPHG" };
                //kiểm tra cấp quyền select trên những cột nào
                string column = "";
                if (PHONGBAN_TQ.CheckedItems.Count != 0 && PHONGBAN_TQ.CheckedItems.Count != 3)
                {
                    for (int i = 0; i < PHONGBAN_TQ.Items.Count; i++)
                    {
                        if (PHONGBAN_TQ.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                        }
                    }
                    //them  vào cuối để biết là view từ bàng Lop
                    column += "PB";
                }
                else
                    column = "PHONGBAN";

                //kiểm tra có cấp quyền select trên view column?
                if (CheckPrivilege(column, "UPDATE", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke(column, "UPDATE", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }
            else if (DA1.Checked) //thu quyền SELECT trên bảng PHONGBAN
            {

                //kiểm tra cấp quyền select trên những cột nào
                //những cột viết tắt để tạo view không bị lỗi view quá dài
                string[] short_name = { "MDA", "TDA", "NGBD", "PG" };
                //kiểm tra cấp quyền select trên những cột nào
                string column = "";
                if (DEAN_TQ.CheckedItems.Count != 0 && DEAN_TQ.CheckedItems.Count != 3)
                {
                    for (int i = 0; i < DEAN_TQ.Items.Count; i++)
                    {
                        if (DEAN_TQ.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                        }
                    }
                    //them  vào cuối để biết là view từ bàng Lop
                    column += "DA";
                }
                else
                    column = "DEAN";

                //kiểm tra có cấp quyền select trên view column?
                if (CheckPrivilege(column, "UPDATE", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke(column, "UPDATE", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }
            else if (PC1.Checked) //thu quyền SELECT trên bảng PHONGBAN
            {

                //kiểm tra cấp quyền select trên những cột nào
                //những cột viết tắt để tạo view không bị lỗi view quá dài
                string[] short_name = { "MNV", "MDA", "TG" };
                //kiểm tra cấp quyền select trên những cột nào
                string column = "";
                if (PHANCONG_TQ.CheckedItems.Count != 0 && PHANCONG_TQ.CheckedItems.Count != 3)
                {
                    for (int i = 0; i < PHANCONG_TQ.Items.Count; i++)
                    {
                        if (PHANCONG_TQ.GetItemCheckState(i) == CheckState.Checked)
                        {
                            column += short_name[i] + "_";
                        }
                    }
                    //them  vào cuối để biết là view từ bàng Lop
                    column += "PC";
                }
                else
                    column = "PHANCONG";

                //kiểm tra có cấp quyền select trên view column?
                if (CheckPrivilege(column, "UPDATE", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke(column, "UPDATE", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }

            //load lại thong tin quyền
            ThongTinQuyen();
        }

        private void inserttq_Click(object sender, EventArgs e)
        {
            if (thuquyenrole.Checked)
            {
                if (CheckRole(tenuserroletq.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("Role không tồn tại.");
                    //con.Close();
                    return;
                }
            }
            else
            {
                if (CheckUser(tenuserroletq.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("User không tồn tại.");
                    //con.Close();
                    return;
                }
            }

            if (NV1.Checked)//thu quyền INSERT trên bảng NHANVIEN
            {
                
                if (CheckPrivilege("NHANVIEN", "INSERT", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke("NHANVIEN", "INSERT", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }

                ThongTinQuyen();
            }
            else if (PB1.Checked) //thu quyền INSERT trên bảng PHONGBAN
            {

                //kiểm tra có cấp quyền INSERT trên view column?
                if (CheckPrivilege("PHONGBAN", "INSERT", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke("PHONGBAN", "INSERT", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }
            else if (DA1.Checked) //thu quyền INSERT trên bảng DOAN
            {

                
                //kiểm tra có cấp quyền INSERT trên view column?
                if (CheckPrivilege("DEAN", "INSERT", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke("DEAN", "INSERT", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }
            else if (PC1.Checked) //thu quyền INSERT trên bảng PHANCONG
            {

                
                if (CheckPrivilege("PHANCONG", "INSERT", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke("PHANCONG", "INSERT", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }

            //load lại thong tin quyền
            ThongTinQuyen();
        }

        private void deletetq_Click(object sender, EventArgs e)
        {
            if (thuquyenrole.Checked)
{
    if (CheckRole(tenuserroletq.Text.ToUpper()) == 0)
    {
        MessageBox.Show("Role không tồn tại.");
        //con.Close();
        return;
    }
}
            else
            {
                if (CheckUser(tenuserroletq.Text.ToUpper()) == 0)
                {
                    MessageBox.Show("User không tồn tại.");
                    //con.Close();
                    return;
                }
            }

            if (NV1.Checked)//thu quyền DELETE trên bảng NHANVIEN
            {
                
                if (CheckPrivilege("NHANVIEN", "DELETE", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke("NHANVIEN", "DELETE", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }

                ThongTinQuyen();
            }
                 else if (PB1.Checked) //thu quyền DELETE trên bảng PHONGBAN
                 {

                if (CheckPrivilege("PHONGBAN", "DELETE", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke("PHONGBAN", "DELETE", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }
            else if (DA1.Checked) //thu quyền DELETE trên bảng DEAN
            {
                if (CheckPrivilege("DEAN", "DELETE", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke("DEAN", "DELETE", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }
            else if (PC1.Checked) //thu quyền DELETE trên bảng PHANCONG
            {

      
                if (CheckPrivilege("PHANCONG", "DELETE", tenuserroletq.Text.ToUpper()) == 0)//chưa được cấp quyền
                {
                    MessageBox.Show("Chưa được cấp quyền nên không thể thu quyền.");
                    //con.Close();
                    return;
                }
                else
                {
                    //thực hiện thu quyền
                    Revoke("PHANCONG", "DELETE", tenuserroletq.Text.ToUpper());
                    MessageBox.Show("Thu quyền thành công.");
                }
            }

            //load lại thong tin quyền
            ThongTinQuyen();
        }


        private void taorolebtn_Click(object sender, EventArgs e)
        {
            if (tenroletb.Text == "")
            {
                MessageBox.Show("Nhập tên role muốn tạo : ");
                return;
            }
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            con.Open();
            if (CheckRole(tenroletb.Text.ToUpper()) == 0 && CheckUser(tenroletb.Text.ToUpper()) == 0)
            {
                OracleCommand cmd_themrole = new OracleCommand();
                cmd_themrole.Connection = con;
                cmd_themrole.CommandText = "alter session set \"_ORACLE_SCRIPT\"=true"; 
                cmd_themrole.CommandType = CommandType.Text;
                cmd_themrole.ExecuteNonQuery();

                OracleCommand cmd_themrole1 = new OracleCommand();
                cmd_themrole1.Connection = con;
                cmd_themrole1.CommandText = "create role " + tenroletb.Text.ToUpper();
                cmd_themrole1.CommandType = CommandType.Text;
                cmd_themrole1.ExecuteNonQuery();
                MessageBox.Show("Thêm role mới thành công.");
                //load lại danh sách role
                DanhSachUser();
            }
            else
            {
                MessageBox.Show("Role hoặc User đã tồn tại trong hệ thống.");
            }
            con.Close();
        }

        private void xoarolebtn_Click(object sender, EventArgs e)
        {
            if (tenroletb.Text == "")
            {
                MessageBox.Show("Lỗi, Chưa nhập tên role");
                return;
            }
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            con.Open();
            if (CheckRole(tenroletb.Text.ToUpper()) == 0)
            {
                MessageBox.Show("Role không tồn tại.");
            }
            else
            {
                OracleCommand cmd_themrole = new OracleCommand();
                cmd_themrole.Connection = con;
                cmd_themrole.CommandText = "alter session set \"_ORACLE_SCRIPT\"=true";
                cmd_themrole.CommandType = CommandType.Text;
                cmd_themrole.ExecuteNonQuery();

                OracleCommand cmd_xoarole = new OracleCommand();
                cmd_xoarole.Connection = con;
                cmd_xoarole.CommandText = "drop role " + tenroletb.Text.ToUpper();
                cmd_xoarole.CommandType = CommandType.Text;
                cmd_xoarole.ExecuteNonQuery();
                MessageBox.Show("Xóa Role thành công.");
                //load lai Thong thin quye62n
                ThongTinQuyen();
                //load lại danh sách role
                DanhSachUser();
            }
            con.Close();
        }

        private void taouserbtn_Click(object sender, EventArgs e)
        {
            if (tendangnhaptb.Text == "" || matkhautb.Text == "")
            {
                MessageBox.Show("Lỗi! Chưa nhập đủ thông tin cần tạo của User.");
                return;
            }
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            con.Open();
            if (CheckRole(tendangnhaptb.Text.ToUpper()) == 0 && CheckUser(tendangnhaptb.Text.ToUpper()) == 0)
            {
                OracleCommand cmd_themrole1 = new OracleCommand();
                cmd_themrole1.Connection = con;
                cmd_themrole1.CommandText = "alter session set \"_ORACLE_SCRIPT\"=true";
                cmd_themrole1.CommandType = CommandType.Text;
                cmd_themrole1.ExecuteNonQuery();
                //tạo user
                OracleCommand cmd_themrole = new OracleCommand();
                cmd_themrole.Connection = con;
                cmd_themrole.CommandText = "create user " + tendangnhaptb.Text.ToUpper() + " identified by " + matkhautb.Text;
                cmd_themrole.CommandType = CommandType.Text;
                cmd_themrole.ExecuteNonQuery();

                //cấp quyền connect cho user 
                OracleCommand cmd_connect = new OracleCommand();
                cmd_connect.Connection = con;
                cmd_connect.CommandText = "grant connect to " + tendangnhaptb.Text.ToUpper();
                cmd_connect.CommandType = CommandType.Text;
                cmd_connect.ExecuteNonQuery();
                //load lại danh sách user
                MessageBox.Show("Tạo User thành công.");
                DanhSachUser();

            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc role đã tồn tại trong hệ thống.");
            }
            con.Close();
        }

        private void doimatkhauntn_Click(object sender, EventArgs e)
        {
            if (tendangnhaptb.Text == "" || matkhautb.Text == "")
            {
                MessageBox.Show("Lỗi! Chưa nhập đủ thông tin");
                return;
            }
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            con.Open();
            if (CheckUser(tendangnhaptb.Text.ToUpper()) == 0)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại trong hệ thống.");
            }
            else
            {
                OracleCommand cmd_themrole1 = new OracleCommand();
                cmd_themrole1.Connection = con;
                cmd_themrole1.CommandText = "alter session set \"_ORACLE_SCRIPT\"=true";
                cmd_themrole1.CommandType = CommandType.Text;
                cmd_themrole1.ExecuteNonQuery();

                OracleCommand cmd_chinhuser = new OracleCommand();
                cmd_chinhuser.Connection = con;
                cmd_chinhuser.CommandText = "alter user " + tendangnhaptb.Text.ToUpper() + " identified by " + matkhautb.Text;
                cmd_chinhuser.CommandType = CommandType.Text;
                cmd_chinhuser.ExecuteNonQuery();
                MessageBox.Show("Đổi mật khẩu User thành công.");
            }
            con.Close();
        }

        private void xoauserbtn_Click(object sender, EventArgs e)
        {
            if (tendangnhaptb.Text == "")
            {
                MessageBox.Show("Lỗi! Chưa nhập đủ thông tin, xin kiểm tra lại");
                return;
            }
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            con.Open();
            if (CheckUser(tendangnhaptb.Text.ToUpper()) == 0)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại trong hệ thống, vui lòng kiểm tra lại.");
            }
            else
            {
                OracleCommand cmd_themrole1 = new OracleCommand();
                cmd_themrole1.Connection = con;
                cmd_themrole1.CommandText = "alter session set \"_ORACLE_SCRIPT\"=true";
                cmd_themrole1.CommandType = CommandType.Text;
                cmd_themrole1.ExecuteNonQuery();

                OracleCommand cmd_xoauser = new OracleCommand();
                cmd_xoauser.Connection = con;
                cmd_xoauser.CommandText = "drop user " + tendangnhaptb.Text.ToUpper();
                cmd_xoauser.CommandType = CommandType.Text;
                cmd_xoauser.ExecuteNonQuery();
                MessageBox.Show("Xóa User thành công.");
                //load lại danh sách user
                DanhSachUser();
                //load lai Thong thin quye62n
                ThongTinQuyen();
            }
            con.Close();
        }

        private void capbtb_Click(object sender, EventArgs e)
        {
            if (caproletb.Text == "" || chousertb.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ thông tin!");
                return;
            }
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = doanatbmhttt)(SERVICE_NAME = XE)));;User ID=QUANLY;Password=12345;Connection Timeout=120;";
            con.Open();
            if (CheckRole(caproletb.Text.ToUpper()) == 0 && CheckUser(chousertb.Text.ToUpper()) == 0)
            {
                MessageBox.Show("Tên role hoặc tên user không tồn tại trong hệ thống.");
            }
            else
            {
                OracleCommand cmd_themrole1 = new OracleCommand();
                cmd_themrole1.Connection = con;
                cmd_themrole1.CommandText = "alter session set \"_ORACLE_SCRIPT\"=true";
                cmd_themrole1.CommandType = CommandType.Text;
                cmd_themrole1.ExecuteNonQuery();

                OracleCommand cmd_caprolechouser = new OracleCommand();
                cmd_caprolechouser.Connection = con;
                cmd_caprolechouser.CommandText = "grant " + caproletb.Text.ToUpper() + " to " + chousertb.Text;
                cmd_caprolechouser.CommandType = CommandType.Text;
                cmd_caprolechouser.ExecuteNonQuery();
                MessageBox.Show(String.Format("Cấp role: {0} cho user:{1} thành công", caproletb.Text.ToUpper(), chousertb.Text.ToUpper()));
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleConnection con_ttaudit = new OracleConnection();
            con_ttaudit.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =quanly;Password=12345;Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ttaudit;
            cmd_ttaudit = new OracleCommand("SELECT DBUID, LSQLTEXT, NTIMESTAMP# FROM SYS.FGA_LOG$", con_ttaudit);
            cmd_ttaudit.CommandType = CommandType.Text;
            con_ttaudit.Open();
            using (OracleDataReader reader = cmd_ttaudit.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                ThongtinAudit.DataSource = dataTable;
            }
            con_ttaudit.Close();
        }
    }

}
