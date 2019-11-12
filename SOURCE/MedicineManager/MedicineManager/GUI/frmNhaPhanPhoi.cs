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
        }
    }
}
