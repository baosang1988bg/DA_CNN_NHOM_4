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
        SqlDataAdapter da_NSX = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmDonViTinh()
        {
            InitializeComponent();
        }

        public void load_DVT()
        {
            string strsel = "select * from DonViTinh";
            da_NSX = conn.getDataAdapter(strsel, "DonViTinh");
            primaryKey[0] = conn.Ds.Tables["DonViTinh"].Columns["MaDVT"];
            conn.Ds.Tables["DonViTinh"].PrimaryKey = primaryKey;
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
        {
            load_DVT();
            dgv_ds_dvt.DataSource = conn.Ds.Tables["DonViTinh"];
        }
    }
}
