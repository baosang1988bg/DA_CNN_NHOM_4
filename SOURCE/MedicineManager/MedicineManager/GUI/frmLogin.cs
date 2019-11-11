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
using System.Threading;

namespace MedicineManager.GUI
{
    public partial class frmLogin : Form
    {
        public static string ID_User = "";
        public static string ChucVu = "";
        ketnoi conn = new ketnoi();
        public frmLogin()
        {
            InitializeComponent();
        }

        public class luuThongTin
        {
            static public string mk;
            static public string server = SystemInformation.UserDomainName.ToString();
        }

        public void runfrmMenuMaster()
        {
            Application.Run(new MenuMaster());
        }

        public string getID(string user, string pass) 
        {
            string id = "";
            try
            {
                conn.OpenConnection();
                string strSql = "SELECT * FROM NhanVien WHERE userName ='" + user + "' and password='" + pass + "'";
                SqlDataReader dr = conn.getReader(strSql);
                while (dr.Read())
                { 
                    id = dr["TenNV"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("That bai khi ket noi");
            }
            finally
            {
                conn.ClosedConnection();
            }
            return id;
        }



        private void frmLogin_Load(object sender, EventArgs e)
        {
            txt_Pass.UseSystemPasswordChar = true;
            cbo_server.Items.Add(luuThongTin.server);
            cbo_server.SelectedIndex = 0;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            luuThongTin.mk = txt_Pass.Text;
            ID_User = getID(txt_userID.Text, txt_Pass.Text);
            if (ID_User != "")
            {
                MessageBox.Show("Xin chao " + frmLogin.ID_User);
                Thread k = new Thread(new ThreadStart(runfrmMenuMaster));
                frmLogin_Load(sender, e);
                k.Start();
                this.Close();
            }
            else
                MessageBox.Show("Tai khoan hoac mat khau khong dung");
        }


    }
}
