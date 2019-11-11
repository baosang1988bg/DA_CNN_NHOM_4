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
        SqlDataAdapter da_NSX = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmNhaSanXuat()
        {
            InitializeComponent();
        }

        public void load_NSX()
        {
            string strsel = "select * from NhaSanXuat";
            da_NSX = conn.getDataAdapter(strsel,"NhaSanXuat");
            primaryKey[0] = conn.Ds.Tables["NhaSanXuat"].Columns["MaNSX"];
            conn.Ds.Tables["NhaSanXuat"].PrimaryKey = primaryKey;
        }

        private void frmNhaSanXuat_Load(object sender, EventArgs e)
        {
            load_NSX();

            dgv_ds_nsx.DataSource = conn.Ds.Tables["NhaSanXuat"];
        }
    }
}
