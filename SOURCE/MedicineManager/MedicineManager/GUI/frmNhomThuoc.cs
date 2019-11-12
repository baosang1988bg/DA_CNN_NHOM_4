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
    public partial class frmNhomThuoc : Form
    {
        ketnoi conn = new ketnoi(); 
        DataSet ds_NT = new DataSet();
        SqlDataAdapter da_NT;
        public frmNhomThuoc()
        {
            InitializeComponent();
        }

        void load_NT()
        {
            string strsel = "select * from NhomThuoc";
            da_NT = new SqlDataAdapter(strsel, conn.Str);
            da_NT.Fill(ds_NT, "NhomThuoc");
            dgv_ds_NT.DataSource = ds_NT.Tables["NhomThuoc"];
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_NT.Tables["NhomThuoc"].Columns[0];
            ds_NT.Tables["NhomThuoc"].PrimaryKey = key;

        }

        private void frmNhomThuoc_Load(object sender, EventArgs e)
        {
            load_NT();
            txt_MaNT.Enabled = txt_TenNT.Enabled = false;

            btn_Xoa_NT.Enabled = btn_Luu_NT.Enabled = btn_Sua_NT.Enabled = false;

            dgv_ds_NT.AllowUserToAddRows = false;
            dgv_ds_NT.ReadOnly = true;
            dgv_ds_NT.MultiSelect = false;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_MaNT.Clear();
            txt_TenNT.Clear();
            txt_MaNT.Enabled = txt_TenNT.Enabled = true;
            btn_Luu_NT.Enabled = true;
            txt_MaNT.Focus();
        }

        private void dgv_ds_NT_SelectionChanged(object sender, EventArgs e)
        {
            btn_Sua_NT.Enabled = btn_Xoa_NT.Enabled = true;
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaNT.Text == string.Empty)
                {
                    MessageBox.Show("Bạn chưa nhập mã nhóm thuốc");
                    txt_MaNT.Focus();
                    return;
                }
                if (txt_TenNT.Text == string.Empty)
                {
                    MessageBox.Show("Bạn chưa nhập tên nhóm thuốc");
                    txt_TenNT.Focus();
                    return;
                }
                if (txt_MaNT.Enabled == true)
                {
                    string strSearch = "select COUNT(*) from Thuoc where MaNhom = '"+ txt_MaNT.Text +"'";
                    int checkNT = conn.getCount(strSearch);
                    if (checkNT > 0)
                    {
                        MessageBox.Show("Mã "+ txt_MaNT.Text +" đã tồn tại");
                        return;
                    }
                    else
                    {
                        DataRow insNew = ds_NT.Tables["NhomThuoc"].NewRow();
                        insNew["MaNhom"] = txt_MaNT.Text;
                        insNew["TenNhom"] = txt_TenNT.Text;
                        ds_NT.Tables["NhomThuoc"].Rows.Add(insNew);
                    }
                }
                else
                {
                    DataRow fixNew = ds_NT.Tables["NhomThuoc"].Rows.Find(txt_MaNT.Text);
                    if (fixNew != null)
                    {
                        fixNew["TenNhom"] = txt_TenNT.Text;
                    }
                    else
                    {
                        MessageBox.Show("Đã tồn tại mã "+ txt_MaNT.Text);
                    }
                }
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_NT);
                da_NT.Update(ds_NT, "NhomThuoc");
                txt_MaNT.Clear();
                txt_TenNT.Clear();
                MessageBox.Show("Lưu thành công");
                btn_Luu_NT.Enabled = false;
                txt_MaNT.Enabled = txt_TenNT.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            txt_TenNT.Enabled = true;
            txt_MaNT.Enabled = false;

            btn_Luu_NT.Enabled = true;
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn xóa mã " + txt_MaNT.Text , "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dt_Thuoc = new DataTable();

                    string strsel = "select * from Thuoc where MaNhom = '"+ txt_MaNT.Text +"'";
                    SqlDataAdapter da_Thuoc = new SqlDataAdapter(strsel, conn.Str);
                    da_Thuoc.Fill(dt_Thuoc);
                    if (dt_Thuoc.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã nhóm " + txt_MaNT.Text + "đang được sử dụng");
                        return;
                    }
                    DataRow upNew = ds_NT.Tables["NhomThuoc"].Rows.Find(txt_MaNT.Text);
                    if (upNew != null)
                    {
                        upNew.Delete();
                    }
                    SqlCommandBuilder cmb = new SqlCommandBuilder(da_NT);
                    da_NT.Update(ds_NT, "NhomThuoc");
                    btn_Xoa_NT.Enabled = btn_Sua_NT.Enabled = false;
                    txt_MaNT.Clear();
                    txt_TenNT.Clear();
                    MessageBox.Show("Xóa thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
        }

        private void dgv_ds_NT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txt_MaNT.Text = dgv_ds_NT.Rows[index].Cells[0].Value.ToString();
                txt_TenNT.Text = dgv_ds_NT.Rows[index].Cells[1].Value.ToString();
                btn_Sua_NT.Enabled = btn_Xoa_NT.Enabled = true;
                btn_Luu_NT.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Sắp xếp");
            }
        }
    }
}
