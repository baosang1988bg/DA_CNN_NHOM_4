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
    public partial class frmDonViTinh : Form
    {
        ketnoi conn = new ketnoi();
        SqlDataAdapter da_DVT;
        DataSet ds_DVT = new DataSet();

        public frmDonViTinh()
        {
            InitializeComponent();
        }

        public void load_DVT()
        {
            string strsel = "select * from DonViTinh";
            da_DVT = conn.getDataAdapter(strsel, "DonViTinh");
            da_DVT.Fill(ds_DVT, "DonViTinh");
            dgv_ds_DVT.DataSource = ds_DVT.Tables["DonViTinh"];
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_DVT.Tables["DonViTinh"].Columns[0];
            ds_DVT.Tables["DonViTinh"].PrimaryKey = key;
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
        {
            load_DVT();
            txt_MaDVT.Enabled = txt_TenDVT.Enabled = false;

            dgv_ds_DVT.AllowUserToAddRows = false;
            dgv_ds_DVT.ReadOnly = true;
            dgv_ds_DVT.MultiSelect = false;

            btn_Luu_DVT.Enabled = btn_Sua_DVT.Enabled = btn_Xoa_DVT.Enabled = false;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_MaDVT.Clear();
            txt_TenDVT.Clear();
            txt_MaDVT.Enabled = txt_TenDVT.Enabled = true;
            btn_Luu_DVT.Enabled = true;
            txt_MaDVT.Focus();
            btn_Sua_DVT.Enabled = btn_Xoa_DVT.Enabled = false;
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaDVT.Text == string.Empty)
                {
                    MessageBox.Show("Bạn chưa nhập mã nhóm thuốc");
                    txt_MaDVT.Focus();
                    return;
                }
                if (txt_TenDVT.Text == string.Empty)
                {
                    MessageBox.Show("Bạn chưa nhập tên nhóm thuốc");
                    txt_TenDVT.Focus();
                    return;
                }
                if (txt_MaDVT.Enabled == true)
                {
                    string strSearch = "select COUNT(*) from Thuoc where MaDVT = '" + txt_MaDVT.Text + "'";
                    int checkDVT = conn.getCount(strSearch);
                    if (checkDVT > 0)
                    {
                        MessageBox.Show("Mã " + txt_MaDVT.Text + " đã tồn tại");
                        return;
                    }
                    else
                    {
                        DataRow insNew = ds_DVT.Tables["DonViTinh"].NewRow();
                        insNew["MaDVT"] = txt_MaDVT.Text;
                        insNew["TenDVT"] = txt_TenDVT.Text;
                        ds_DVT.Tables["DonViTinh"].Rows.Add(insNew);
                    }
                }
                else
                {
                    DataRow fixNew = ds_DVT.Tables["DonViTinh"].Rows.Find(txt_MaDVT.Text);
                    if (fixNew != null)
                    {
                        fixNew["TenDVT"] = txt_TenDVT.Text;
                    }
                    else
                    {
                        MessageBox.Show("Đã tồn tại mã " + txt_MaDVT.Text);
                    }
                }
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_DVT);
                da_DVT.Update(ds_DVT, "DonViTinh");
                txt_MaDVT.Clear();
                txt_TenDVT.Clear();
                MessageBox.Show("Lưu thành công");
                btn_Luu_DVT.Enabled = false;
                txt_MaDVT.Enabled = txt_TenDVT.Enabled = false;
                btn_Xoa_DVT.Enabled = btn_Sua_DVT.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn xóa mã " + txt_MaDVT.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dt_DVT = new DataTable();

                    string strsel = "select * from DonViTinh where MaDVT = '" + txt_MaDVT.Text + "'";
                    SqlDataAdapter da_Thuoc = new SqlDataAdapter(strsel, conn.Str);
                    da_Thuoc.Fill(dt_DVT);
                    if (dt_DVT.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã nhóm " + txt_MaDVT.Text + "đang được sử dụng");
                        return;
                    }
                    DataRow upNew = ds_DVT.Tables["DonViTinh"].Rows.Find(txt_MaDVT.Text);
                    if (upNew != null)
                    {
                        upNew.Delete();
                    }
                    SqlCommandBuilder cmb = new SqlCommandBuilder(da_DVT);
                    da_DVT.Update(ds_DVT, "NhomThuoc");
                    btn_Xoa_DVT.Enabled = btn_Sua_DVT.Enabled = false;
                    txt_MaDVT.Clear();
                    txt_TenDVT.Clear();
                    MessageBox.Show("Xóa mã " + txt_MaDVT.Text + "thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            txt_TenDVT.Enabled = true;
            txt_MaDVT.Enabled = false;

            btn_Luu_DVT.Enabled = true;
        }

        private void dgv_ds_DVT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txt_MaDVT.Text = dgv_ds_DVT.Rows[index].Cells[0].Value.ToString();
                txt_TenDVT.Text = dgv_ds_DVT.Rows[index].Cells[1].Value.ToString();
                btn_Sua_DVT.Enabled = btn_Xoa_DVT.Enabled = true;
                btn_Luu_DVT.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Sắp xếp");
            }
        }

        private void dgv_ds_DVT_SelectionChanged(object sender, EventArgs e)
        {
            btn_Sua_DVT.Enabled = btn_Xoa_DVT.Enabled = true;
        }

    }
}
