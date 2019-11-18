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
    public partial class frmThuoc : Form
    {
        ketnoi conn = new ketnoi();
        SqlDataAdapter da_Thuoc;
        DataSet ds_Thuoc = new DataSet();
        SqlDataAdapter da_DVT_T;
        SqlDataAdapter da_TenCD_T;
        SqlDataAdapter da_TNhom_T;
        SqlDataAdapter da_TenNSX_T;

        public frmThuoc()
        {
            InitializeComponent();
        }

        public void load_Thuoc()
        {
            string sel = "select * from Thuoc";
            da_Thuoc = new SqlDataAdapter(sel, conn.Str);
            da_Thuoc.Fill(ds_Thuoc, "Thuoc");
        }

        public void Load_cbo_TenNH()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from NhomThuoc";
            da_TNhom_T = new SqlDataAdapter(strsel, conn.Str);
            da_TNhom_T.Fill(ds, "NhomThuoc");
            cbo_TenNh.DataSource = ds.Tables[0];
            cbo_TenNh.DisplayMember = "TenNhom";
            cbo_TenNh.ValueMember = "MaNhom";
        }

        public void Load_cbo_TenNSX()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from NhaSanXuat";
            da_TenNSX_T = new SqlDataAdapter(strsel, conn.Str);
            da_TenNSX_T.Fill(ds, "NhaSanXuat");
            cbo_TenNSX.DataSource = ds.Tables[0];
            cbo_TenNSX.DisplayMember = "TenNSX";
            cbo_TenNSX.ValueMember = "MaNSX";
        }

        public void Load_cbo_TenCD()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from CachDung";
            da_TenCD_T = new SqlDataAdapter(strsel, conn.Str);
            da_TenCD_T.Fill(ds, "CachDung");
            cbo_TenCD.DataSource = ds.Tables[0];
            cbo_TenCD.DisplayMember = "TenCD";
            cbo_TenCD.ValueMember = "MaCD";
        }

        public void Load_cbo_TenDVT()
        {
            DataSet ds = new DataSet();
            string strsel = "select * from DonViTinh";
            da_DVT_T = new SqlDataAdapter(strsel, conn.Str);
            da_DVT_T.Fill(ds, "DonViTinh");
            comboBox2.DataSource = ds.Tables[0];
            comboBox2.DisplayMember = "TenDVT";
            comboBox2.ValueMember = "MaDVT";
        }

        private void frmThuoc_Load(object sender, EventArgs e)
        {
            load_Thuoc();
            Load_cbo_TenCD();
            Load_cbo_TenDVT();
            Load_cbo_TenNH();
            Load_cbo_TenNSX();

            cbo_TenCD.SelectedIndex = comboBox2.SelectedIndex = cbo_TenNh.SelectedIndex = cbo_TenNSX.SelectedIndex =  -1;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_TenThuoc.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập tên thuốc");
                    txt_TenThuoc.Focus();
                    return;
                }
                if (txt_MaThuoc.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập mã thuốc");
                    txt_MaThuoc.Focus();
                    return;
                }
                if (txt_Gia.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập giá thuốc");
                    txt_Gia.Focus();
                    return;
                }
                if (int.Parse(txt_Gia.Text) < 1000)
                {
                    MessageBox.Show("Giá thuốc chưa phù hợp");
                    txt_Gia.Focus();
                    return;
                }
                if (cbo_TenCD.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn tên cách dùng");
                    cbo_TenCD.SelectedIndex = 1;
                    return;
                }
                if (comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn tên đơn vị tính");
                    comboBox2.SelectedIndex = 1;
                    return;
                }
                if (cbo_TenNh.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn tên nhóm");
                    cbo_TenNh.SelectedIndex = 1;
                    return;
                }
                if (cbo_TenNSX.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn tên nhà sản xuất");
                    cbo_TenNSX.SelectedIndex = 1;
                    return;
                }
                else
                {
                    DataRow dr = ds_Thuoc.Tables["Thuoc"].NewRow();
                    dr["MaThuoc"] = txt_MaThuoc.Text;
                    dr["TenThuoc"] = txt_TenThuoc.Text;
                    dr["GiaBan"] = txt_Gia.Text;
                    dr["MaDVT"] = comboBox2.SelectedValue;
                    dr["MaCD"] = cbo_TenCD.SelectedValue;
                    dr["MaNhom"] = cbo_TenNh.SelectedValue;
                    dr["MaNSX"] = cbo_TenNSX.SelectedValue;
                    dr["SoLuong"] = nUD_SL.Value.ToString();

                    ds_Thuoc.Tables["Thuoc"].Rows.Add(dr);
                }

                SqlCommandBuilder cmb = new SqlCommandBuilder(da_Thuoc);
                da_Thuoc.Update(ds_Thuoc, "Thuoc");
                MessageBox.Show("Thêm thành công");
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi khi thêm thuốc");
            }
            
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }



    }
}
