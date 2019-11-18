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
        SqlDataAdapter da_HDX;
        DataSet ds_HDX = new DataSet();


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
            string strsel = "select * from ChiTietHoaDonXuat where = '"+ txt_MaHD.Text.Trim() +"'";
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
            txt_MaHD.Enabled = txt_HD.Enabled = false;
            txt_DonGia_HD.Enabled = false;
            txt_NgayHD.Enabled = false;
            txt_ThanhTien_HD.Enabled = false;
            txt_ThanhTien_HD.Text = "0";

            cbo_TenNV_HD.SelectedIndex = -1;

            DateTime now = DateTime.Now;
            txt_NgayHD.Text = now.ToString("dd/MM/yyyy");
        }

        private void btn_Tao_HD_Click(object sender, EventArgs e)
        {
            txt_MaHD.Enabled = cbo_TenNV_HD.Enabled = true;
        }

        private void btn_Luu_HD_Click(object sender, EventArgs e)
        {
            try
            {
                string strsel = "select * from HoaDonXuat";
                da_HDX = new SqlDataAdapter(strsel, conn.Str);
                da_HDX.Fill(ds_HDX, "HoaDonXuat");
                if (txt_MaHD.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập mã hóa đơn mới nè");
                    txt_MaHD.Focus();
                    return;
                }
                else
                {
                    DataRow dr = ds_HDX.Tables["HoaDonXuat"].NewRow();

                    dr["MaHDX"] = txt_MaHD.Text;
                    dr["MaNV"] = cbo_TenNV_HD.SelectedValue;
                    dr["NgayLap"] = txt_NgayHD.Text;
                    dr["TienThuoc"] = txt_ThanhTien_HD.Text;

                    ds_HDX.Tables["HoaDonXuat"].Rows.Add(dr);
                }
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_HDX);
                da_HDX.Update(ds_HDX, "HoaDonXuat");
                MessageBox.Show("Thành công");
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi Thêm");
            }
        }

       
    }
}
