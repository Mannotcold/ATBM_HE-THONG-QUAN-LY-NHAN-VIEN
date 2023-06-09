﻿using System;
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
        private string MK, TK;
        public TaiChinh()
        {
            InitializeComponent();
        }

        public TaiChinh(string tk, string mk)
        {
            InitializeComponent();
            this.TK = tk;
            this.MK = mk;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DanhSachNhanVien();
        }

        private void DanhSachNhanVien()
        {
            OracleConnection con_ds = new OracleConnection();
            con_ds.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ds;
            if (timkiemuserroletb.Text == "")
                cmd_ds = new OracleCommand("Select * from quanly.TC_NHANVIEN", con_ds);
            else
                cmd_ds = new OracleCommand("Select * from quanly.TC_NHANVIEN where MaNV = '" + timkiemuserroletb.Text.ToUpper() + "'", con_ds);
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

        private void danhsachuserdg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = danhsachuserdg.CurrentRow.Index;
            textBox1.Text = danhsachuserdg.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = danhsachuserdg.Rows[i].Cells[6].Value.ToString();
            textBox3.Text = danhsachuserdg.Rows[i].Cells[7].Value.ToString();
        }

        //Cap nhat luong hoac phu cap
        private void button2_Click(object sender, EventArgs e)
        {
            CapNhatLuong_PhuCap();
        }

        private void CapNhatLuong_PhuCap()
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
                    cmd_ttq = new OracleCommand("update quanly.NHANVIEN set LUONG = '" + textBox2.Text + "', PHUCAP = '" + textBox3.Text + "' where MaNV = '" + textBox1.Text + "'", con_ttq);

                    cmd_ttq.CommandType = CommandType.Text;
                    con_ttq.Open();
                    using (OracleDataReader reader = cmd_ttq.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        BangPhanCong.DataSource = dataTable;
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    DanhSachNhanVien();
                    con_ttq.Close();

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Cập nhật không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    
                }

            }
            else
            {

            }
        }

        //Tim kiem phan cong
        private void timkiemuserbtn_Click(object sender, EventArgs e)
        {
            DanhSachPhanCong();
        }

        private void DanhSachPhanCong()
        {
            OracleConnection con_ds = new OracleConnection();
            con_ds.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = Oracle Man)(SERVICE_NAME = XE)));;User ID =" + TK + ";PASSWORD=" + MK + ";Connection Timeout=120;";
            DataSet dataSet_ds = new DataSet();
            OracleCommand cmd_ds;
            if (timkiemusertb.Text == "")
                cmd_ds = new OracleCommand("Select * from quanly.TC_PHANCONG", con_ds);
            else
                cmd_ds = new OracleCommand("Select * from quanly.TC_PHANCONG where MaNV = '" + timkiemusertb.Text.ToUpper() + "'", con_ds);
            cmd_ds.CommandType = CommandType.Text;
            con_ds.Open();
            using (OracleDataReader reader = cmd_ds.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                BangPhanCong.DataSource = dataTable;
            }
            con_ds.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form nhanvien = new NhanVien(TK, MK);
            nhanvien.Show();
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
    }
}
