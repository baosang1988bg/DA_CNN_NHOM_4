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
        }


    }
}
