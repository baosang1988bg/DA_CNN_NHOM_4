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
        }
    }
}
