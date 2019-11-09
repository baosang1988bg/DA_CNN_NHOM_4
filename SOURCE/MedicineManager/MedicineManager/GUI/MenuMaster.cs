using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicineManager.GUI
{
    public partial class MenuMaster : Form
    {
        public MenuMaster()
        {
            InitializeComponent();
        }

        public void ActiveForm_Master()
        {
            Form frm = MenuMaster.ActiveForm;
            foreach (Form fm in frm.MdiChildren)
            {
                if (fm.Name == "MenuMaster")
                {
                    fm.Activate();
                    return;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveForm_Master();

            Test frmNH = new Test();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void ts_Thuoc_Click(object sender, EventArgs e)
        {
            //ActiveForm_Master();
            //Medicine frmNH = new Medicine();
            //frmNH.MdiParent = this;

            //frmNH.Show();
            //frmNH.Top = 0;
            //frmNH.Left = 0;
            
        }

        private void tS_nhomThuoc_Click(object sender, EventArgs e)
        {
            ActiveForm_Master();

            frmNhomThuoc frmNH = new frmNhomThuoc();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void tS_HDN_Click(object sender, EventArgs e)
        {
            ActiveForm_Master();

            frmNhapThuoc frmNH = new frmNhapThuoc();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void tS_HDX_Click(object sender, EventArgs e)
        {

        }

        private void MenuMaster_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            lbl_today.Text = now.ToString("dd/MM/yyyy");
            lbl_user.Text = frmLogin.ID_User;
        }

        private void tS_DVT_Click(object sender, EventArgs e)
        {
            ActiveForm_Master();
            frmDonViTinh frmNH = new frmDonViTinh();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void tS_CachDung_Click(object sender, EventArgs e)
        {
            ActiveForm_Master();
            frmCachDung frmNH = new frmCachDung();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void tS_NSX_Click(object sender, EventArgs e)
        {
            ActiveForm_Master();
            frmNhaSanXuat frmNH = new frmNhaSanXuat();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }

        private void tS_NPP_Click(object sender, EventArgs e)
        {
            ActiveForm_Master();
            frmNhaPhanPhoi frmNH = new frmNhaPhanPhoi();
            frmNH.MdiParent = this;

            frmNH.Show();
            frmNH.Top = 0;
            frmNH.Left = 0;
        }



    }
}
