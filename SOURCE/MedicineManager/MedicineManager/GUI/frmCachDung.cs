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
    public partial class frmCachDung : Form
    {
        ketnoi conn = new ketnoi();
        SqlDataAdapter da_NSX = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmCachDung()
        {
            InitializeComponent();
        }

        public void load_CD()
        {
            string strsel = "select * from CachDung";
            da_NSX = conn.getDataAdapter(strsel, "CachDung");
            primaryKey[0] = conn.Ds.Tables["CachDung"].Columns["MaCD"];
            conn.Ds.Tables["CachDung"].PrimaryKey = primaryKey;
        }

        private void frmCachDung_Load(object sender, EventArgs e)
        {
            load_CD();
            dgv_ds_CD.DataSource = conn.Ds.Tables["CachDung"];
            btn_Luu.Enabled = false;
            btn_Sua.Enabled = false;
            btn_Xoa.Enabled = false;
            txt_MaCD.Enabled = false;
            txt_TenCD.Enabled = false;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_MaCD.Text = "";
            txt_TenCD.Text = "";
            txt_MaCD.Focus();
            btn_Luu.Enabled = true;
            txt_MaCD.Enabled = true;
            txt_TenCD.Enabled = true;
        }

        private void dgv_ds_CD_SelectionChanged(object sender, EventArgs e)
        {
            btn_Xoa.Enabled = btn_Sua.Enabled = true;
        }

        private void dgv_ds_CD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txt_MaCD.Text = dgv_ds_CD.Rows[index].Cells[0].Value.ToString();
                txt_TenCD.Text = dgv_ds_CD.Rows[index].Cells[1].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Sắp xếp");
            }
            
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {

        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            btn_Luu.Enabled = true;
            txt_TenCD.Enabled = true;


        }



    }
}
