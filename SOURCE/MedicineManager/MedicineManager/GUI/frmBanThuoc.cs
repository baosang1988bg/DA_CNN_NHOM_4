using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

using System.Data.SqlClient;
using System.Data;

namespace MedicineManager.GUI
{
    public partial class frmBanThuoc : Form
    {
        ketnoi conn = new ketnoi();
        SqlDataAdapter da_HD;
        DataSet ds_HD = new DataSet();
        DataColumn[] primarykey = new DataColumn[1];
        SqlDataAdapter da_TenNV_HD;
        SqlDataAdapter da_DVT_HD;
        SqlDataAdapter da_TenThuoc_HD;


        public frmBanThuoc()
        {
            InitializeComponent();
        }

        public void Load_cbo_TenNV()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from NhanVien";
            da_TenNV_HD = new SqlDataAdapter(strsel, conn.Str);
            da_TenNV_HD.Fill(ds, "NhanVien");
            cbo_TenNV_HD.DataSource = ds.Tables[0];
            cbo_TenNV_HD.DisplayMember = "TenNV";
            cbo_TenNV_HD.ValueMember = "MaNV";
        }

        public void Load_cbo_DVT()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from DonViTinh";
            da_DVT_HD = new SqlDataAdapter(strsel, conn.Str);
            da_DVT_HD.Fill(ds, "DonViTinh");
            cbo_DVT_HD.DataSource = ds.Tables[0];
            cbo_DVT_HD.DisplayMember = "TenDVT";
            cbo_DVT_HD.ValueMember = "MaDVT";
        }

        public void Load_cbo_TenThuoc()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from Thuoc";
            da_TenThuoc_HD = new SqlDataAdapter(strsel, conn.Str);
            da_TenThuoc_HD.Fill(ds, "Thuoc");
            cbo_MaThuoc_HD.DataSource = ds.Tables[0];
            cbo_MaThuoc_HD.DisplayMember = "TenThuoc";
            cbo_MaThuoc_HD.ValueMember = "MaThuoc";
        }

        public void Load_CTHD()
        {
            string strsel = "select * from ChiTietHoaDonXuat where = '"+ txt_HD.Text.Trim() +"'";
            da_HD = new SqlDataAdapter(strsel, conn.Str);
            da_HD.Fill(ds_HD, "ChiTietHoaDonXuat");
            dgv_BanThuoc.DataSource = ds_HD.Tables["ChiTietHoaDonXuat"];
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_HD.Tables["ChiTietHoaDonXuat"].Columns[0];

            ds_HD.Tables["ChiTietHoaDonXuat"].PrimaryKey = key;
        }

        private void frmBanThuoc_Load(object sender, EventArgs e)
        {
            Load_cbo_DVT();
            Load_cbo_TenNV();
            Load_cbo_TenThuoc();

            txt_HD.ReadOnly = true;
            txt_DonGia_HD.ReadOnly = true;
            txt_NgayHD.ReadOnly = true;
            txt_ThanhTien_HD.ReadOnly = true;

            DateTime now = DateTime.Now;
            txt_NgayHD.Text = now.ToString("dd/MM/yyyy");
        }

       
    }
}
