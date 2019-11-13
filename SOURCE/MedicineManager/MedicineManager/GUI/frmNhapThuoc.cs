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
        SqlDataAdapter da_NPP_NT;
        SqlDataAdapter da_DVT_NT;
        SqlDataAdapter da_MaHD_NT;
        SqlDataAdapter da_TT_NT;
        SqlDataAdapter da_TenNV_NT;

        public frmNhapThuoc()
        {
            InitializeComponent();
        }

        public void Load_cbo_TenNPP()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from NhaPhanPhoi";
            da_NPP_NT = new SqlDataAdapter(strsel, conn.Str);
            da_NPP_NT.Fill(ds, "NhaPhanPhoi");
            cbo_TenNPP.DataSource = ds.Tables[0];
            cbo_TenNPP.DisplayMember = "TenNPP";
            cbo_TenNPP.ValueMember = "MaNPP";
        }

        public void Load_cbo_DVT()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from DonViTinh";
            da_DVT_NT = new SqlDataAdapter(strsel, conn.Str);
            da_DVT_NT.Fill(ds, "DonViTinh");
            cbo_DVT.DataSource = ds.Tables[0];
            cbo_DVT.DisplayMember = "TenDVT";
            cbo_DVT.ValueMember = "MaDVT";
        }

        public void Load_cbo_MaHD()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from HoaDonNhap";
            da_MaHD_NT = new SqlDataAdapter(strsel, conn.Str);
            da_MaHD_NT.Fill(ds, "HoaDonNhap");
            cbo_Ma_HDN.DataSource = ds.Tables[0];
            cbo_Ma_HDN.DisplayMember = "MaHDN";
            cbo_Ma_HDN.ValueMember = "MaNPP";
        }

        public void Load_cbo_TenThuoc()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from Thuoc";
            da_TT_NT = new SqlDataAdapter(strsel, conn.Str);
            da_TT_NT.Fill(ds, "Thuoc");
            cbo_MaThuoc.DataSource = ds.Tables[0];
            cbo_MaThuoc.DisplayMember = "TenThuoc";
            cbo_MaThuoc.ValueMember = "MaThuoc";
        }

        public void Load_cbo_TenNV()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from NhanVien";
            da_TenNV_NT = new SqlDataAdapter(strsel, conn.Str);
            da_TenNV_NT.Fill(ds, "NhanVien");
            cbo_TenNV.DataSource = ds.Tables[0];
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
            Load_cbo_TenNPP();
            Load_cbo_TenThuoc();
            Load_cbo_MaHD();
            txt_DonGia.Enabled = false;
            txt_ThanhTien.Text = "0";
            txt_ThanhTien.Enabled = false;
            Load_cbo_DVT();
            Load_cbo_TenNV();

            cbo_TenNPP.SelectedIndex = -1;
            cbo_TenNV.SelectedIndex = -1;
            cbo_Ma_HDN.SelectedIndex = -1;
            cbo_MaThuoc.SelectedIndex = -1;
            cbo_DVT.SelectedIndex = -1;
            txt_DonGia.Text = "0";
            txt_MaHDN.Enabled = false;
            txt_MaHDN.Focus();
            btn_Luu_PN.Enabled = false;
            DateTime now = DateTime.Now;
            txt_Ngay.Text = now.ToString("dd/MM/yyyy");
            txt_Ngay.Enabled = false;
            dgv_ds_HDN.AllowUserToAddRows = false;
            dgv_ds_HDN.ReadOnly = true;

            gb_CTPN.Enabled = false;
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

        }

        private void btn_TaoPN_Click(object sender, EventArgs e)
        {
            txt_MaHDN.Clear();

            cbo_TenNPP.SelectedIndex = -1;
            cbo_TenNV.SelectedIndex = -1;

            txt_ThanhTien.Text = "0";
            txt_MaHDN.Enabled = true;

            btn_Luu_PN.Enabled = true;

        }

        private void btn_LPN_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaHDN.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập mã hóa đơn mới nè");
                    txt_MaHDN.Focus();
                    return;
                }
                if (cbo_TenNPP.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn nhà phân phối nè!");
                    cbo_TenNPP.SelectedIndex = 1;
                    return;
                }
                if (cbo_TenNV.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn tên nhân viên nè!");
                    cbo_TenNV.SelectedIndex = 1;
                    return;
                }
                if (txt_MaHDN.Enabled == true)
                {
                    string strSearch = "select COUNT(*) from HoaDonNhap where MaHDN = '"+ txt_MaHDN.Text +"'";
                    int checkMHD = conn.getCount(strSearch);
                    if (checkMHD > 0)
                    {
                        MessageBox.Show("Mã "+ txt_MaHDN.Text +" đã được sử dụng");
                        return;
                    }
                    else
                    {
                        DataRow inNew = ds_HDN.Tables["HoaDonNhap"].NewRow();

                        inNew["MaHDN"] = txt_MaHDN.Text;
                        try
                        {
                            //DataSet dsTenNPP = new DataSet();
                            //string strTenNPP = "select MaNPP from NhaPhanPhoi where TenNPP LIKE N'" + cbo_TenNPP.Text + "'";
                            //SqlDataAdapter daTenNPP = new SqlDataAdapter(strTenNPP, conn.Str);
                            //inNew["MaNPP"] = daTenNPP;
                            //string haha = cbo_TenNPP.ValueMember.ToString();
                            //MessageBox.Show("aaaaa" + haha);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Lỗi khi lưu nhà phân phối");
                        }
                        try
                        {
                            inNew["MaNV"] = cbo_TenNV.Text;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Lỗi khi lưu tên nhân viên");
                        }
                        inNew["NgayLap"] = txt_Ngay.Text;

                        ds_HDN.Tables["HoaDonNhap"].Rows.Add(inNew);
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Lưu lỗi rồi!");
            }
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {

        }

        private void cbo_TenNPP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (cbo_Ma_HDN.Text == "" || txt_DonGia.Text == "" || cbo_DVT.Text == "" || cbo_MaThuoc.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ thông tin");
                cbo_Ma_HDN.Text = txt_MaHDN.Text;
                return;
            }
            try
            {
                //mã hóa đơn nhập
                string maHDN = cbo_Ma_HDN.Text;

                //lấy mã thuốc
                string tenThuoc = "select MaThuoc from Thuoc where TenThuoc LIKE N'%" + cbo_MaThuoc.Text.Trim() + "%'";
                SqlDataReader drTenThuoc = conn.excuteReader(tenThuoc);
                drTenThuoc.Read();
                string maThuoc = drTenThuoc["MaThuoc"].ToString(); ;

                drTenThuoc.Close();
                conn.ClosedConnection();

                //lấy mã đơn vị tính
                string tenDVT = "select MaDVT from DonViTinh where TenDVT LIKE N'%" + cbo_DVT.Text.Trim() + "%'";
                SqlDataReader drTenDVT = conn.excuteReader(tenDVT);
                drTenDVT.Read();
                string maDVT = drTenDVT["MaDVT"].ToString(); ;

                drTenDVT.Close();
                conn.ClosedConnection();

                //lấy đơn giá
                string dongia = txt_DonGia.Text;

                //lấy số lượng
                string soluong = nUD_SL.Value.ToString();

                string strSQL = "select COUNT(*) from ChiTietHoaDonNhap ct where ct.MaHDN = '"+ cbo_Ma_HDN.Text.Trim() +"' And ct.MaThuoc = '"+ cbo_MaThuoc.Text.Trim() +"'";
                int Check_MHD = conn.getCount(strSQL);
                if (Check_MHD > 0)
                {
                    MessageBox.Show("Đơn hàng "+ maHDN +" đã tồn tại thuốc "+ maThuoc +" vui lòng chọn thuốc khác hoặc chỉnh sửa.");
                    return;
                }
                //thêm dữ liệu mới vào bảng
                string strIns = "insert into ChiTietHoaDonNhap (MaHDN,MaThuoc,MaDVT,SoLuong,Gia) values ('" + maHDN + "','" + maThuoc + "','" + maDVT + "','" + soluong + "','" + dongia + "');";
                conn.updateToDB(strIns);
                MessageBox.Show("Mã "+ maHDN +"và thuốc "+ maThuoc +"được tạo thành công!");
                load_HDN();

                //thêm dữ liệu thành tiền
                int sl = int.Parse(nUD_SL.Value.ToString());
                int gia = int.Parse(txt_DonGia.Text.Trim());
                txt_MaHDN.Text = cbo_Ma_HDN.Text;
                int thanhTien = sl * gia;

                string strUpTT = "update HoaDonNhap set TienNhan = TienNhan + " + thanhTien + " where MaHDN = '" + maHDN + "'";
                conn.updateToDB(strUpTT);

                string strUp = "select TienNhan from HoaDonNhap where MaHDN = '"+ txt_MaHDN.Text.Trim() +"'";
                SqlDataReader drTT = conn.excuteReader(strUp);
                while (drTT.Read())
                {
                    txt_ThanhTien.Text = drTT["TienNhan"].ToString();
                }
                drTT.Close();
                conn.ClosedConnection();

                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi ở phần thêm thông tin chi tiết");
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {

        }
    }
}
