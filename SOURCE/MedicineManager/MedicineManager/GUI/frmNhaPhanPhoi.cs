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
        }

        private void txt_SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
