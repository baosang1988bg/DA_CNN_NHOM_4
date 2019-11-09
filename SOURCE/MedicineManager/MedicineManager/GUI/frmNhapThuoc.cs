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

        public void Load_cbo_DVT()
        {
            string strsel = "select * from DonViTinh";
            DataTable dt = conn.getDataTable(strsel, "DonViTinh");
            cbo_DVT.DataSource = dt;
            cbo_DVT.DisplayMember = "TenDVT";
            cbo_DVT.ValueMember = "MaDVT";
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

        public void Load_cbo_TenNV()
        {
            string strsel = "select * from NhanVien";
            DataTable dt = conn.getDataTable(strsel, "NhanVien");
            cbo_TenNV.DataSource = dt;
            cbo_TenNV.DisplayMember = "TenNV";
            cbo_TenNV.ValueMember = "MaNV";
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
            // TODO: This line of code loads data into the 'qL_ThuocDataSet.ChiTietHoaDonXuat' table. You can move, or remove it, as needed.
            this.chiTietHoaDonXuatTableAdapter.Fill(this.qL_ThuocDataSet.ChiTietHoaDonXuat);
            Load_cbo_TenNPP();
            Load_cbo_TenThuoc();
            Load_cbo_MaHD();
            txt_DonGia.Text = "0";
            txt_DonGia.Enabled = false;
            load_HDN();
            txt_ThanhTien.Text = "0";
            txt_ThanhTien.Enabled = false;
            Load_cbo_DVT();
            Load_cbo_TenNV();
            cbo_DVT.Text = "";
            cbo_Ma_HDN.Text = "";
            cbo_MaThuoc.Text = "";
            cbo_TenNPP.Text = "";
            cbo_TenNV.Text = "";
            DateTime now = DateTime.Now;
            txt_Ngay.Text = now.ToString("dd/MM/yyyy");
            txt_Ngay.Enabled = false;
        }



        private void cbo_MaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strsel = "select GiaBan from Thuoc where TenThuoc LIKE N'%" + cbo_MaThuoc.Text.Trim() + "%'";
            SqlDataReader dr = conn.excuteReader(strsel);
            while (dr.Read())
            {
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

        private void btn_TaoPN_Click(object sender, EventArgs e)
        {
            txt_MaHDN.Clear();
            cbo_TenNPP.Text = "";
            cbo_TenNV.Text = "";
            txt_ThanhTien.Text = "0";
        }

        private void btn_LPN_Click(object sender, EventArgs e)
        {
            if (txt_MaHDN.Text == "" || cbo_TenNPP.Text == "" || cbo_TenNV.Text == "" || txt_Ngay.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ thông tin");
                txt_MaHDN.Focus();
                return;
            }
            try
            {
                string maHDN = txt_MaHDN.Text.Trim();

                string tenNV = "select MaNV from NhanVien where TenNV LIKE N'%" + cbo_TenNV.Text.Trim() + "%'";
                SqlDataReader drTenNV = conn.excuteReader(tenNV);
                string maNV = drTenNV["MaNV"].ToString();;
                drTenNV.Read();
                maNV = drTenNV["MaNV"].ToString();
                drTenNV.Close();
                conn.ClosedConnection();

                string ngaypp = txt_Ngay.Text;

                string tenNPP = "select MaNPP from NhaPhanPhoi where TenNPP LIKE N'%" + cbo_TenNPP.Text.Trim() + "%'";
                SqlDataReader drTenNPP = conn.excuteReader(tenNPP);
                string maNPP = drTenNPP["MaNPP"].ToString(); ;
                drTenNPP.Read();
                maNPP = drTenNPP["MaNPP"].ToString();
                drTenNPP.Close();
                conn.ClosedConnection();

                string strSQL = "select COUNT(*) from HoaDonNhap where MaHDN = '"+ maHDN +"'";
                int Check_MHD = conn.getCount(strSQL);
                if (Check_MHD > 0)
                {
                    MessageBox.Show("Mã "+ maHDN +" đã tồn tại");
                    return;
                }
                string strIns = "insert into HoaDonNhap (MaHDN,MaNPP,MaNV,NgayLap) values ('"+ maHDN +"','"+ maNPP +"','"+ maNV +"','"+ ngaypp +"');";
                conn.updateToDB(strIns);
                MessageBox.Show("Mã "+ maHDN +" được tạo thành công!");
                load_HDN();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi roi ne" + ex);
                return;
            }
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            string tenNV = "select MaNV from NhanVien where TenNV LIKE N'%" + cbo_TenNV.Text.Trim() + "%'";
            SqlDataReader drTenNV = conn.excuteReader(tenNV);
            drTenNV.Read();
            string maNV = drTenNV["MaNV"].ToString();
            //while (drTenNV.Read())
            //{
            //    maNV = drTenNV["MaNV"].ToString();

            //}
            drTenNV.Close();
            conn.ClosedConnection();
            MessageBox.Show(maNV);
        }

        private void cbo_TenNPP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
