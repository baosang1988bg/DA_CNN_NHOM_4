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

namespace MedicineManager.GUI
{
    public partial class frmNhaSanXuat : Form
    {
        ketnoi conn = new ketnoi();
        DataSet ds_NSX = new DataSet();
        SqlDataAdapter da_NSX;
        public frmNhaSanXuat()
        {
            InitializeComponent();
        }

        public void load_NSX()
        {
            string strsel = "select * from NhaSanXuat";
            da_NSX = new SqlDataAdapter(strsel, conn.Str);
            da_NSX.Fill(ds_NSX, "NhaSanXuat");
            dgv_ds_nsx.DataSource = ds_NSX.Tables["NhaSanXuat"];
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_NSX.Tables["NhaSanXuat"].Columns[0];
            ds_NSX.Tables["NhaSanXuat"].PrimaryKey = key;
        }

        private void frmNhaSanXuat_Load(object sender, EventArgs e)
        {
            load_NSX();

            btn_Luu_NSX.Enabled = btn_Sua_NSX.Enabled = btn_Xoa_NSX.Enabled = false;
            txt_DiaChi_NSX.Enabled = txt_MaNSX.Enabled = txt_TenNSX.Enabled = txt_SDT_NSX.Enabled = false;

            dgv_ds_nsx.AllowUserToAddRows = false;
            dgv_ds_nsx.ReadOnly = true;
            dgv_ds_nsx.MultiSelect = false;
        }

        private void btn_Them_NSX_Click(object sender, EventArgs e)
        {
            txt_DiaChi_NSX.Enabled = txt_MaNSX.Enabled = txt_TenNSX.Enabled = txt_SDT_NSX.Enabled = btn_Luu_NSX.Enabled = true;
            txt_MaNSX.Focus();
            txt_MaNSX.Clear();
            txt_TenNSX.Clear();
            txt_DiaChi_NSX.Clear();
            txt_SDT_NSX.Clear();
            btn_Sua_NSX.Enabled = btn_Xoa_NSX.Enabled = false;
        }

        private void btn_Xoa_NSX_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn xóa " + txt_MaNSX.Text, "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dt_Thuoc = new DataTable();

                    string strsel = "select * from Thuoc where MaNSX = '" + txt_MaNSX.Text + "'";
                    SqlDataAdapter da_Thuoc = new SqlDataAdapter(strsel, conn.Str);
                    da_Thuoc.Fill(dt_Thuoc);
                    if (dt_Thuoc.Rows.Count > 0)
                    {
                        MessageBox.Show("Dữ liệu đang được sử dụng");
                        return;
                    }
                    DataRow delNew = ds_NSX.Tables["NhaSanXuat"].Rows.Find(txt_MaNSX.Text);
                    if (delNew != null)
                    {
                        delNew.Delete();
                    }
                    SqlCommandBuilder cmb = new SqlCommandBuilder(da_NSX);
                    da_NSX.Update(ds_NSX, "NhaSanXuat");
                    txt_MaNSX.Focus();
                    txt_MaNSX.Clear();
                    txt_TenNSX.Clear();
                    txt_DiaChi_NSX.Clear();
                    txt_SDT_NSX.Clear(); 
                    btn_Luu_NSX.Enabled = btn_Sua_NSX.Enabled = btn_Xoa_NSX.Enabled = false;
                    txt_DiaChi_NSX.Enabled = txt_MaNSX.Enabled = txt_TenNSX.Enabled = txt_SDT_NSX.Enabled = false;
                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
        }

        private void btn_Sua_NSX_Click(object sender, EventArgs e)
        {
            txt_MaNSX.Enabled = false;
            txt_TenNSX.Enabled = txt_DiaChi_NSX.Enabled = txt_SDT_NSX.Enabled = true;

            btn_Luu_NSX.Enabled = true;
        }

        private void btn_Luu_NSX_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaNSX.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập mã nhà sản xuất");
                    txt_MaNSX.Focus();
                    return;
                }
                if (txt_TenNSX.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập tên nhà sản xuất");
                    txt_TenNSX.Focus();
                    return;
                }
                if (txt_DiaChi_NSX.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập địa chỉ của nhà sản xuất");
                    txt_DiaChi_NSX.Focus();
                    return;
                }
                if (txt_TenNSX.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập số điện thoại của nhà sản xuất");
                    txt_TenNSX.Focus();
                    return;
                }
                if (txt_MaNSX.Enabled == true)
                {
                    string strSearchNSX = "select COUNT(*) from Thuoc where MaNSX = '" + txt_MaNSX.Text + "'";
                    int checkNSX = conn.getCount(strSearchNSX);
                    if (checkNSX > 0)
                    {
                        MessageBox.Show("Mã " + txt_MaNSX.Text + " đã tồn tại");
                        return;
                    }
                    else
                    {
                        DataRow inNew = ds_NSX.Tables["NhaSanXuat"].NewRow();

                        inNew["MaNSX"] = txt_MaNSX.Text;
                        inNew["TenNSX"] = txt_TenNSX.Text;
                        inNew["DiaChi"] = txt_DiaChi_NSX.Text;
                        inNew["SDT"] = txt_SDT_NSX.Text;

                        ds_NSX.Tables["NhaSanXuat"].Rows.Add(inNew);
                    }
                }
                else
                {
                    DataRow fixNewNSX = ds_NSX.Tables["NhaSanXuat"].Rows.Find(txt_MaNSX.Text);
                    if (fixNewNSX != null)
                    {
                        fixNewNSX["TenNSX"] = txt_TenNSX.Text;
                        fixNewNSX["DiaChi"] = txt_DiaChi_NSX.Text;
                        fixNewNSX["SDT"] = txt_SDT_NSX.Text;
                    }
                    else
                    {
                        MessageBox.Show("Đã tồn tại mã " + txt_MaNSX.Text);
                    }
                }

                SqlCommandBuilder cmb = new SqlCommandBuilder(da_NSX);
                da_NSX.Update(ds_NSX, "NhaSanXuat");
                txt_MaNSX.Clear();
                txt_TenNSX.Clear();
                txt_DiaChi_NSX.Clear();
                txt_SDT_NSX.Clear();
                btn_Luu_NSX.Enabled = btn_Sua_NSX.Enabled = btn_Xoa_NSX.Enabled = false;
                txt_DiaChi_NSX.Enabled = txt_MaNSX.Enabled = txt_TenNSX.Enabled = txt_SDT_NSX.Enabled = false;
                MessageBox.Show("Lưu thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
        }

        private void dgv_ds_nsx_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txt_MaNSX.Text = dgv_ds_nsx.Rows[index].Cells[0].Value.ToString();
                txt_TenNSX.Text = dgv_ds_nsx.Rows[index].Cells[1].Value.ToString();
                txt_DiaChi_NSX.Text = dgv_ds_nsx.Rows[index].Cells[2].Value.ToString();
                txt_SDT_NSX.Text = dgv_ds_nsx.Rows[index].Cells[3].Value.ToString();
                btn_Sua_NSX.Enabled = btn_Xoa_NSX.Enabled = true;
                txt_DiaChi_NSX.Enabled = txt_MaNSX.Enabled = txt_TenNSX.Enabled = txt_SDT_NSX.Enabled = btn_Luu_NSX.Enabled = false;

            }
            catch (Exception)
            {
                MessageBox.Show("Sắp xếp");
            }
        }

        private void txt_SDT_NSX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("" + txt_SDT_NSX.TextLength);
                e.Handled = true;
            }
            if (txt_SDT_NSX.TextLength >= 10)
            {
                e.Handled = true;
                MessageBox.Show("Vui lòng kiểm tra lại số điện thoại");
            }
        }
    }
}
