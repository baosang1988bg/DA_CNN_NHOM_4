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
    public partial class frmNhaPhanPhoi : Form
    {
        ketnoi conn = new ketnoi();
        SqlDataAdapter da_NPP;
        DataSet ds_NPP = new DataSet();
        public frmNhaPhanPhoi()
        {
            InitializeComponent();
        }

        public void load_NPP()
        {
            string strsel = "select * from NhaPhanPhoi";
            da_NPP = new SqlDataAdapter(strsel, conn.Str);
            da_NPP.Fill(ds_NPP, "NhaPhanPhoi");
            dgv_ds_npp.DataSource = ds_NPP.Tables["NhaPhanPhoi"];
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_NPP.Tables["NhaPhanPhoi"].Columns[0];
            ds_NPP.Tables["NhaPhanPhoi"].PrimaryKey = key;

        }

        private void frmNhaPhanPhoi_Load(object sender, EventArgs e)
        {
            load_NPP();
            dgv_ds_npp.AllowUserToAddRows = false;
            dgv_ds_npp.ReadOnly = true;

            btn_Luu_NPP.Enabled = btn_Sua_NPP.Enabled = btn_Xoa_NPP.Enabled = false;

            txt_MaNPP.Enabled = txt_TenNPP.Enabled = txt_DiaChi_NPP.Enabled = txt_Email_NPP.Enabled = txt_SDT_NPP.Enabled = false;
        }

        private void txt_SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("" + txt_SDT_NPP.TextLength);
                e.Handled = true;
            }
            if (txt_SDT_NPP.TextLength >= 10)
            {
                e.Handled = true;
                MessageBox.Show("Vui lòng kiểm tra lại số điện thoại");
            }
        }

        private void btn_Them_NPP_Click(object sender, EventArgs e)
        {
            txt_MaNPP.Enabled = txt_TenNPP.Enabled = txt_DiaChi_NPP.Enabled = txt_Email_NPP.Enabled = txt_SDT_NPP.Enabled = btn_Luu_NPP.Enabled = true;
            txt_MaNPP.Focus();
            txt_MaNPP.Clear();
            txt_TenNPP.Clear();
            txt_DiaChi_NPP.Clear();
            txt_Email_NPP.Clear();
            txt_SDT_NPP.Clear();
            btn_Sua_NPP.Enabled = btn_Xoa_NPP.Enabled = false;
        }

        private void btn_Luu_NPP_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaNPP.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập mã nhà phân phối");
                    txt_MaNPP.Focus();
                    return;
                }
                if (txt_TenNPP.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập tên nhà phân phối");
                    txt_TenNPP.Focus();
                    return;
                }
                if (txt_DiaChi_NPP.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập địa chỉ của nhà phân phối");
                    txt_DiaChi_NPP.Focus();
                    return;
                }
                if (txt_SDT_NPP.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập số điện thoại của nhà phân phối");
                    txt_SDT_NPP.Focus();
                    return;
                }
                if (txt_Email_NPP.Text == string.Empty)
                {
                    MessageBox.Show("Chưa nhập email của nhà phân phối");
                    txt_Email_NPP.Focus();
                    return;
                }
                if (txt_MaNPP.Enabled == true)
                {
                    string strSearch = "select COUNT(*) from NhaPhanPhoi where MaNPP = '"+txt_MaNPP.Text+"'";
                    int checkNPP = conn.getCount(strSearch);
                    if (checkNPP > 0)
                    {
                        MessageBox.Show("Mã "+txt_MaNPP.Text+" đã tồn tại");
                        return;
                    }
                    else
                    {
                        DataRow inNew = ds_NPP.Tables["NhaPhanPhoi"].NewRow();

                        inNew["MaNPP"] = txt_MaNPP.Text;
                        inNew["TenNPP"] = txt_TenNPP.Text;
                        inNew["DiaChi"] = txt_DiaChi_NPP.Text;
                        inNew["DienThoai"] = txt_SDT_NPP.Text;
                        inNew["Email"] = txt_Email_NPP.Text;

                        ds_NPP.Tables["NhaPhanPhoi"].Rows.Add(inNew);
                    }
                }
                else
                {
                    DataRow fixNew = ds_NPP.Tables["NhaPhanPhoi"].Rows.Find(txt_MaNPP.Text);
                    if (fixNew != null)
                    {
                        fixNew["TenNPP"] = txt_TenNPP.Text;
                        fixNew["DiaChi"] = txt_DiaChi_NPP.Text;
                        fixNew["Email"] = txt_Email_NPP.Text;
                        fixNew["DienThoai"] = txt_SDT_NPP.Text;
                    }
                    else
                    {
                        MessageBox.Show("Đã tồn tại mã " + txt_MaNPP.Text);
                    }
                }

                SqlCommandBuilder cmb = new SqlCommandBuilder(da_NPP);
                da_NPP.Update(ds_NPP, "NhaPhanPhoi");
                txt_MaNPP.Clear();
                txt_TenNPP.Clear();
                txt_DiaChi_NPP.Clear();
                txt_Email_NPP.Clear();
                txt_SDT_NPP.Clear();
                btn_Sua_NPP.Enabled = btn_Xoa_NPP.Enabled = btn_Luu_NPP.Enabled = false;
                txt_MaNPP.Enabled = txt_TenNPP.Enabled = txt_DiaChi_NPP.Enabled = txt_Email_NPP.Enabled = txt_SDT_NPP.Enabled = false;
                MessageBox.Show("Lưu thành công");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
        }

        private void dgv_ds_npp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txt_MaNPP.Text = dgv_ds_npp.Rows[index].Cells[0].Value.ToString();
                txt_TenNPP.Text = dgv_ds_npp.Rows[index].Cells[1].Value.ToString();
                txt_DiaChi_NPP.Text = dgv_ds_npp.Rows[index].Cells[2].Value.ToString();
                txt_SDT_NPP.Text = dgv_ds_npp.Rows[index].Cells[3].Value.ToString();
                txt_Email_NPP.Text = dgv_ds_npp.Rows[index].Cells[4].Value.ToString();
                btn_Xoa_NPP.Enabled = btn_Sua_NPP.Enabled = true;
                txt_MaNPP.Enabled = txt_TenNPP.Enabled = txt_DiaChi_NPP.Enabled = txt_Email_NPP.Enabled = txt_SDT_NPP.Enabled = btn_Luu_NPP.Enabled = false ;
            }
            catch (Exception)
            {
                MessageBox.Show("Sắp xếp");
            }
            
        }

        private void btn_Sua_NPP_Click(object sender, EventArgs e)
        {
            txt_MaNPP.Enabled = false;
            txt_TenNPP.Enabled = txt_DiaChi_NPP.Enabled = txt_Email_NPP.Enabled = txt_SDT_NPP.Enabled = true;

            btn_Luu_NPP.Enabled = true;
        }

        private void btn_Xoa_NPP_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn xóa "+ txt_MaNPP.Text,"Xóa",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dt_HDN = new DataTable();

                    string strsel = "select * from HoaDonNhap where MaNPP = '"+ txt_MaNPP.Text +"'";
                    SqlDataAdapter da_HDN = new SqlDataAdapter(strsel, conn.Str);
                    da_HDN.Fill(dt_HDN);
                    if (dt_HDN.Rows.Count > 0)
                    {
                        MessageBox.Show("");
                        return;
                    }
                    DataRow delNew = ds_NPP.Tables["NhaPhanPhoi"].Rows.Find(txt_MaNPP.Text);
                    if (delNew != null)
                    {
                        delNew.Delete();
                    }
                    SqlCommandBuilder cmb = new SqlCommandBuilder(da_NPP);
                    da_NPP.Update(ds_NPP, "NhaPhanPhoi");
                    txt_MaNPP.Clear();
                    txt_TenNPP.Clear();
                    txt_DiaChi_NPP.Clear();
                    txt_Email_NPP.Clear();
                    txt_SDT_NPP.Clear();
                    btn_Sua_NPP.Enabled = btn_Xoa_NPP.Enabled = btn_Luu_NPP.Enabled = false;
                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex);
            }
        }
    }
}
