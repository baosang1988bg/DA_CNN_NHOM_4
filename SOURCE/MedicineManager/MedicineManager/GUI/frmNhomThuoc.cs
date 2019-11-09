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
        SqlDataAdapter da_NSX = new SqlDataAdapter();
        DataColumn[] primaryKey = new DataColumn[1];
        public frmNhomThuoc()
        {
            InitializeComponent();
        }

        public void load_NT()
        {
            string strsel = "select * from NhomThuoc";
            da_NSX = conn.getDataAdapter(strsel, "NhomThuoc");
            primaryKey[0] = conn.Ds.Tables["NhomThuoc"].Columns["MaNhom"];
            conn.Ds.Tables["NhomThuoc"].PrimaryKey = primaryKey;
        }

        private void frmNhomThuoc_Load(object sender, EventArgs e)
        {
            load_NT();
            dgv_ds_NT.DataSource = conn.Ds.Tables["NhomThuoc"];
        }
    }
}
