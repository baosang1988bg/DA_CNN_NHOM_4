using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MedicineManager.GUI;

namespace MedicineManager
{
    class ketnoi
    {
        private string str;

        public string Str
        {
            get { return str; }
            set { str = value; }
        }
        private DataSet ds;

        public DataSet Ds
        {
            get { return ds; }
            set { ds = value; }
        }

        private SqlConnection con;

        public SqlConnection Con
        {
            get { return con; }
            set { con = value; }
        }

        public ketnoi(string DataSetName)
        {
            string server = frmLogin.luuThongTin.server;
            str = DataSetName;
            Con = new SqlConnection(str);
            Ds = new DataSet(DataSetName);
        }

        public ketnoi()
        {
            string server = frmLogin.luuThongTin.server;
            str = @"Data Source=" + server + ";Initial Catalog=QL_Thuoc;User ID=sa;Password=sa2012";
            Con = new SqlConnection(str);
            ds = new DataSet();
        }

        public void OpenConnection()
        {
            if (Con.State == ConnectionState.Closed)
                Con.Open();

        }
        public void ClosedConnection()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
        }
        public void disposeConnection()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Dispose();
        }
        public void updateToDB(string strSQL)
        {
            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Connection = Con;
            cmd.ExecuteNonQuery();

            ClosedConnection();
        }
        public SqlDataReader getReader(string strSQL)
        {
            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Connection = Con;
            return cmd.ExecuteReader();

        }

        public SqlDataReader excuteReader(string strSQL)
        {
            OpenConnection();

            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader rd = cmd.ExecuteReader();

            return rd;
        }

        public int getCount(string strSQL)
        {
            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Connection = Con;
            int count = (int)cmd.ExecuteScalar();

            ClosedConnection();
            return count;
        }

        public bool checkForExistence(string strSQL)
        {
            int count = getCount(strSQL);
            if (count > 0)
                return true;
            return false;
        }

        public DataTable getDataTable(string strSQL, string tableName)
        {
            OpenConnection();

            SqlDataAdapter ada = new SqlDataAdapter(strSQL, Con);
            ada.Fill(Ds, tableName);

            ClosedConnection();
            return Ds.Tables[tableName];

        }
        public SqlDataAdapter getDataAdapter(string strSQL, string tableName)
        {
            OpenConnection();

            SqlDataAdapter ada = new SqlDataAdapter(strSQL, con);
            ada.Fill(Ds, tableName);

            ClosedConnection();
            return ada;

        }
        public bool checkKey(string sql)
        {
            DataTable table = new DataTable();
            SqlDataAdapter MyData = new SqlDataAdapter(sql, Con);            
            MyData.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }

    }
}
