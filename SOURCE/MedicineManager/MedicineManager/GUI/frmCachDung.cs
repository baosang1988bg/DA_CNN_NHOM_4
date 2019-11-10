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
        //DataColumn[] primaryKey = new DataColumn[1];
        SqlDataAdapter da_CD;
        DataSet ds_CD = new DataSet();
        public frmCachDung()
        {
            InitializeComponent();
        }

        public void load_CD()
        {
            string strsel = "select * from CachDung";
            da_CD = new SqlDataAdapter(strsel, conn.Str);
            da_CD.Fill(ds_CD, "CachDung");
            dgv_ds_CD.DataSource = ds_CD.Tables["CachDung"];
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_CD.Tables["CachDung"].Columns[0];
            ds_CD.Tables["CachDung"].PrimaryKey = key;
            //da_CD = conn.getDataAdapter(strsel, "CachDung");
            //primaryKey[0] = conn.Ds.Tables["CachDung"].Columns["MaCD"];
            //conn.Ds.Tables["CachDung"].PrimaryKey = primaryKey;
        }

        private void frmCachDung_Load(object sender, EventArgs e)
        {
            load_CD();
            //dgv_ds_CD.DataSource = conn.Ds.Tables["CachDung"];
            btn_Luu.Enabled = false;
            btn_Sua.Enabled = false;
            btn_Xoa.Enabled = false;
            txt_MaCD.Enabled = false;
            txt_TenCD.Enabled = false;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_MaCD.Text = "";
            txt_TenCD.Text = "";
            txt_MaCD.Focus();
            btn_Luu.Enabled = true;
            txt_MaCD.Enabled = true;
            txt_TenCD.Enabled = true;
        }

        private void dgv_ds_CD_SelectionChanged(object sender, EventArgs e)
        {
            btn_Xoa.Enabled = btn_Sua.Enabled = true;
        }

        private void dgv_ds_CD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                txt_MaCD.Text = dgv_ds_CD.Rows[index].Cells[0].Value.ToString();
                txt_TenCD.Text = dgv_ds_CD.Rows[index].Cells[1].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Sắp xếp");
            }
            
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (txt_MaCD.Text == string.Empty)
            {
                MessageBox.Show("Chưa nhập mã cách dùng");
                txt_MaCD.Focus();
                return;
            }
            if (txt_TenCD.Text == string.Empty)
            {
                MessageBox.Show("Chưa nhập cách dùng");
                txt_TenCD.Focus();
                return;
            }
            if (txt_MaCD.Enabled == true)//Thêm
            {
                try
                {
                    string strC = "select COUNT(*) from CachDung where MaCD = '" + txt_MaCD.Text.Trim() + "'";
                    int checkCD = conn.getCount(strC);
                    if (checkCD > 0)
                    {
                        MessageBox.Show("Mã " + txt_MaCD.Text.Trim() + " này đã tồn tại");
                        return;
                    }
                    string strIns = "insert into CachDung (MaCD,TenCD) values ('" + txt_MaCD.Text.Trim() + "','" + txt_TenCD.Text.Trim() + "')";
                    conn.updateToDB(strIns);
                    MessageBox.Show("Lưu thành công mã: "+ txt_MaCD.Text.Trim() +" và tên: "+ txt_TenCD.Text.Trim() +"");
                    load_CD();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi trong phần tạo mới cách dùng");
                    return;
                }
            }
            else//Sửa
            {
                try
                {
                    string strUp = "update CachDung set TenCD = '"+ txt_TenCD.Text.Trim() +"'  where MaCD = '"+ txt_MaCD.Text.Trim() +"'";
                    SqlCommandBuilder builder = new SqlCommandBuilder(da_CD);
                    conn.updateToDB(strUp);
                    da_CD.Update(conn.Ds, "TenCD");
                    MessageBox.Show("Sửa mã " + txt_MaCD.Text.Trim() + " thành công!");
                    load_CD();
                }
                catch (Exception)
                {
                    MessageBox.Show("Sửa thất bại");
                }
            }
            txt_MaCD.Clear();
            txt_TenCD.Clear();
            txt_TenCD.Enabled = false;
            txt_MaCD.Enabled = false;
            btn_Luu.Enabled = false;
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            btn_Luu.Enabled = true;
            txt_TenCD.Enabled = true;
            txt_MaCD.Enabled = false;

        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //string strsel = "select * from Thuoc where MaCD = '" + txt_MaCD.Text.Trim() + "'";
                    //if (conn.checkKey(strsel) == true)
                    //{
                    //    MessageBox.Show("Dữ liệu đang được sử dụng không thể xóa");
                    //    return;
                    //}
                    //else
                    //{
                    //    string strDel = "delete from CachDung where MaCD = '" + txt_MaCD.Text.Trim() + "'";
                    //    SqlDataReader drCD = conn.excuteReader(strDel);
                    //    drCD.Read();
                    //    drCD.Close();
                    //    MessageBox.Show("Xóa thành công");
                    //    SqlCommandBuilder cmb = new SqlCommandBuilder(da_CD);
                    //    //da_CD.Update(ds_CD, "CachDung");
                    //    load_CD();
                    //}
                    DataTable dt_CD = new DataTable();
                    SqlDataAdapter da_CD1 = new SqlDataAdapter("select * from Thuoc where MaCD = '" + txt_MaCD.Text + "'", conn.Str);
                    da_CD1.Fill(dt_CD);
                    if (dt_CD.Rows.Count > 0)
                    {
                        MessageBox.Show("Dữ liệu đang được sử dụng");
                        return;
                    }

                    DataRow upd_del = ds_CD.Tables["CachDung"].Rows.Find(txt_MaCD.Text.Trim());
                    if (upd_del != null)
                    {
                        upd_del.Delete();
                    }
                    SqlCommandBuilder cmb = new SqlCommandBuilder(da_CD);
                    da_CD.Update(ds_CD, "CachDung");
                    MessageBox.Show("Xóa thành công");
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show("Xóa thất bại rồi" + ex);
                return;
            }
        }



    }
}
