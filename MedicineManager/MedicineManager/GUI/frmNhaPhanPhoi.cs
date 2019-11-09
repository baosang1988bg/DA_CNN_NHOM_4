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
        SqlDataAdapter da_NSX = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmNhaPhanPhoi()
        {
            InitializeComponent();
        }

        public void load_NPP()
        {
            string strsel = "select * from NhaPhanPhoi";
            da_NSX = conn.getDataAdapter(strsel, "NhaPhanPhoi");
            primaryKey[0] = conn.Ds.Tables["NhaPhanPhoi"].Columns["MaNPP"];
            conn.Ds.Tables["NhaPhanPhoi"].PrimaryKey = primaryKey;
        }

        private void frmNhaPhanPhoi_Load(object sender, EventArgs e)
        {
            load_NPP();
            dgv_ds_npp.DataSource = conn.Ds.Tables["NhaPhanPhoi"];
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
