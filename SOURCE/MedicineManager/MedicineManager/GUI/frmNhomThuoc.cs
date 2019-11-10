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
        //DataColumn[] primaryKey = new DataColumn[1];
        DataSet ds_NT = new DataSet();
        public frmNhomThuoc()
        {
            InitializeComponent();
        }

        void load_NT()
        {
            string strsel = "select * from NhomThuoc";
            SqlDataAdapter da_NT = new SqlDataAdapter(strsel, conn.Str);
            da_NT.Fill(ds_NT, "NhomThuoc");
            dgv_ds_NT.DataSource = ds_NT.Tables["NhomThuoc"];
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_NT.Tables["NhomThuoc"].Columns[0];
            ds_NT.Tables["NhomThuoc"].PrimaryKey = key;
            //da_NT = conn.getDataAdapter(strsel, "NhomThuoc");
            //primaryKey[0] = conn.Ds.Tables["NhomThuoc"].Columns["MaNhom"];
            //conn.Ds.Tables["NhomThuoc"].PrimaryKey = primaryKey;
        }

        private void frmNhomThuoc_Load(object sender, EventArgs e)
        {
            load_NT();
            //dgv_ds_NT.DataSource = conn.Ds.Tables["NhomThuoc"];
        }
    }
}
