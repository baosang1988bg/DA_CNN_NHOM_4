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
using MedicineManager.Report;

namespace MedicineManager.GUI
{
    public partial class frmBanThuoc : Form
    {
        ketnoi conn = new ketnoi();
        SqlDataAdapter da_HD;
        DataSet ds_HD = new DataSet();
        SqlDataAdapter da_dgv;
        DataSet ds_dgv = new DataSet();
        DataColumn[] primarykey = new DataColumn[1];
        SqlDataAdapter da_TenNV_HD;
        SqlDataAdapter da_DVT_HD;
        SqlDataAdapter da_TenThuoc_HD;
        SqlDataAdapter da_HDX;
        DataTable dt_dgv;
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

        public void load_hdx()
        {
            string strsel = "select * from HoaDonXuat";
            da_HDX = new SqlDataAdapter(strsel, conn.Str);
            da_HDX.Fill(ds_HDX, "HoaDonXuat");

            DataColumn[] key = new DataColumn[1];
            key[0] = ds_HDX.Tables["HoaDonXuat"].Columns[0];

            ds_HDX.Tables["HoaDonXuat"].PrimaryKey = key;
        }

        public void Load_CTHD()
        {
            string strsel = "select * from ChiTietHoaDonXuat";
            da_HD = new SqlDataAdapter(strsel, conn.Str);
            da_HD.Fill(ds_HD, "ChiTietHoaDonXuat");
            //dgv_BanThuoc.DataSource = ds_HD.Tables["ChiTietHoaDonXuat"];
            //DataColumn[] key = new DataColumn[2];
            //key[0] = ds_HD.Tables["ChiTietHoaDonXuat"].Columns[0];
            //key[1] = ds_HD.Tables["ChiTietHoaDonXuat"].Columns[1];

            //ds_HD.Tables["ChiTietHoaDonXuat"].PrimaryKey = key;
        }

        public void Load_dgv()
        {
            //where MaHDX = '"+ txt_MaHD.Text +"'
            string strsel = "select * from ChiTietHoaDonXuat where MaHDX = '"+ txt_MaHD.Text +"'";
            da_dgv = new SqlDataAdapter(strsel, conn.Str);
            da_dgv.Fill(ds_dgv, "ChiTietHoaDonXuat");

            dgv_BanThuoc.DataSource = ds_dgv.Tables["ChiTietHoaDonXuat"];
            DataColumn[] key = new DataColumn[2];
            key[0] = ds_dgv.Tables["ChiTietHoaDonXuat"].Columns[0];
            key[1] = ds_dgv.Tables["ChiTietHoaDonXuat"].Columns[1];

            ds_dgv.Tables["ChiTietHoaDonXuat"].PrimaryKey = key;
        }


        private void frmBanThuoc_Load(object sender, EventArgs e)
        {
            Load_dgv();
            Load_cbo_DVT();
            Load_cbo_TenNV();
            Load_cbo_TenThuoc();
            txt_MaHD.Enabled = txt_HD.Enabled = false;
            txt_DonGia_HD.Enabled = false;
            txt_NgayHD.Enabled = false;
            txt_ThanhTien_HD.Enabled = false;
            txt_ThanhTien_HD.Text = "0";
            gb_CTHD.Enabled = false;
            cbo_TenNV_HD.SelectedIndex = cbo_DVT_HD.SelectedIndex = cbo_MaThuoc_HD.SelectedIndex = -1;

            cbo_DVT_HD.Enabled = cbo_MaThuoc_HD.Enabled = cbo_TenNV_HD.Enabled = false;
            btn_Luu_HD.Enabled = btn_Sua_HD.Enabled = btn_View_HD.Enabled = btn_Them_HD.Enabled = btn_Xoa_HD.Enabled = false;
            DateTime now = DateTime.Now;
            txt_NgayHD.Text = now.ToString("MM/dd/yyyy");
        }

        private void btn_Tao_HD_Click(object sender, EventArgs e)
        {
            txt_MaHD.Enabled = cbo_TenNV_HD.Enabled = true;
            cbo_TenNV_HD.Enabled = true;
            btn_Luu_HD.Enabled = true;
        }

        private void btn_Luu_HD_Click(object sender, EventArgs e)
        {
            try
            {
                load_hdx();
                if (txt_MaHD.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập mã hóa đơn mới nè");
                    txt_MaHD.Focus();
                    return;
                }
                else
                {
                    DataRow search = ds_HDX.Tables["HoaDonXuat"].Rows.Find(txt_MaHD.Text);
                    if (search != null)
                    {
                        MessageBox.Show("Mã hóa đơn đã tồn tại!");
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
                }
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_HDX);
                da_HDX.Update(ds_HDX, "HoaDonXuat");
                MessageBox.Show("Thành công");
                txt_HD.Text = txt_MaHD.Text;
                groupBox1.Enabled = false;
                cbo_MaThuoc_HD.Enabled = true;
                btn_Sua_HD.Enabled = btn_View_HD.Enabled = btn_Them_HD.Enabled = btn_Xoa_HD.Enabled = true;
                gb_CTHD.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi Thêm");
            }
        }

        private void btn_Them_HD_Click(object sender, EventArgs e)
        {
            try
            {
                Load_CTHD();
                if (cbo_MaThuoc_HD.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập thuốc");
                    cbo_MaThuoc_HD.SelectedIndex = 0;
                    return;
                }
                
                else
                {
                    try 
	                {
                        DataRow dr = ds_dgv.Tables["ChiTietHoaDonXuat"].NewRow();

                        dr["MaHDX"] = txt_MaHD.Text;
                        dr["MaThuoc"] = cbo_MaThuoc_HD.SelectedValue;
                        dr["SoLuong"] = nUD_SL_HD.Value.ToString();
                        dr["MaDVT"] = cbo_DVT_HD.SelectedValue;
                        dr["Gia"] = txt_DonGia_HD.Text;

                        ds_dgv.Tables["ChiTietHoaDonXuat"].Rows.Add(dr);
	                }
	                catch (Exception)
	                {
                        MessageBox.Show("Mã thuốc đã tồn tại!");
                        return;
	                }
                }
                Load_dgv();
                SqlCommandBuilder cmb_gdv = new SqlCommandBuilder(da_dgv);
                da_dgv.Update(ds_dgv, "ChiTietHoaDonXuat");
                MessageBox.Show("Thêm hóa đơn thành công");
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi thêm");
            }
        }

        private void cbo_MaThuoc_HD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strsel = "select GiaBan, TenDVT from Thuoc t, DonViTinh cd where t.TenThuoc = N'" + cbo_MaThuoc_HD.Text.Trim() +"' and t.MaDVT = cd.MaDVT";
            SqlDataReader dr = conn.excuteReader(strsel);
            while (dr.Read())
            {
                txt_DonGia_HD.Text = dr["GiaBan"].ToString();
                cbo_DVT_HD.Text = dr["TenDVT"].ToString();
            }
            dr.Close();
            conn.ClosedConnection();
        }

        private void btn_View_HD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn in hóa đơn", "In hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
            {
                frmHoaDon frm = new frmHoaDon();
                frm.Show();
            }
        }
    }
}
