using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MedicineManager.GUI
{
    public partial class frmNhapThuoc : Form
    {
        ketnoi conn = new ketnoi();
        SqlDataAdapter da_NT = new SqlDataAdapter();
        DataSet ds_HDN = new DataSet();
        DataColumn[] primaryKey = new DataColumn[2];

        public frmNhapThuoc()
        {
            InitializeComponent();
        }

        public void Load_cbo_TenNPP()
        {
            string strsel = "select * from NhaPhanPhoi";
            DataTable dt = conn.getDataTable(strsel, "NhaPhanPhoi");
            cbo_TenNPP.DataSource = dt;
            cbo_TenNPP.DisplayMember = "TenNPP";
            cbo_TenNPP.ValueMember = "MaNPP";
        }

        public void Load_cbo_MaHD()
        {
            string strsel = "select * from ChiTietHoaDonNhap";
            DataTable dt = conn.getDataTable(strsel, "ChiTietHoaDonNhap");
            cbo_Ma_HDN.DataSource = dt;
            cbo_Ma_HDN.DisplayMember = "MaHDN";
            cbo_Ma_HDN.ValueMember = "MaThuoc";
        }

        public void Load_cbo_TenThuoc()
        {
            string strsel = "select * from Thuoc";
            DataTable dt = conn.getDataTable(strsel, "Thuoc");
            cbo_MaThuoc.DataSource = dt;
            cbo_MaThuoc.DisplayMember = "TenThuoc";
            cbo_MaThuoc.ValueMember = "MaThuoc";
        }

        public void load_HDN()
        {
            string strsel = "select * from ChiTietHoaDonNhap";
            SqlDataAdapter da_HDN = new SqlDataAdapter(strsel, conn.Str);
            da_HDN.Fill(ds_HDN, "ChiTietHoaDonNhap");
            dgv_ds_HDN.DataSource = ds_HDN.Tables["ChiTietHoaDonNhap"];
            DataColumn[] key = new DataColumn[2];
            key[0] = ds_HDN.Tables["ChiTietHoaDonNhap"].Columns[0];
            key[1] = ds_HDN.Tables["ChiTietHoaDonNhap"].Columns[1];

            ds_HDN.Tables["ChiTietHoaDonNhap"].PrimaryKey = key;
        }

        private void frmNhapThuoc_Load(object sender, EventArgs e)
        {
            Load_cbo_TenNPP();
            Load_cbo_TenThuoc();
            Load_cbo_MaHD();
            txt_DonGia.Enabled = false;
            load_HDN();
            txt_ThanhTien.Enabled = false;
        }



        private void cbo_MaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strsel = "select GiaBan from Thuoc where TenThuoc LIKE N'%" + cbo_MaThuoc.Text.Trim() + "%'";
            SqlDataReader dr = conn.excuteReader(strsel);
            while (dr.Read())
            {
                //int sl = int.Parse(nUD_SL.Value.ToString());
                //int giaBan = int.Parse(dr["GiaBan"].ToString());
                //int tongTien = sl * giaBan;
                //txt_DonGia.Text = tongTien.ToString();
                txt_DonGia.Text = dr["GiaBan"].ToString();
            }
            dr.Close();
            conn.ClosedConnection();
        }

        private void nUD_SL_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cbo_Ma_HDN_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strsel = "select * from ChiTietHoaDonNhap where MaHDN LIKE N'%" + cbo_Ma_HDN.Text.Trim() + "%'";
            //SqlDataReader dr = conn.excuteReader(strsel);
            //while (dr.Read())
            //{
            //    txt_DonGia.Text = dr["GiaBan"].ToString();
            //}
            //dr.Close();
            //conn.ClosedConnection();
            

        }
    }
}
