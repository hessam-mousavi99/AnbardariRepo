using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Anbardari
{
    public class DataBase
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt;
        public void DoCommand(string sql)
        {
            con = new SqlConnection();
            con.ConnectionString = "Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True";
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataTable MySelect(string sql)
        {
            con = new SqlConnection();
            con.ConnectionString = "Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True";
            cmd = new SqlCommand();
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            cmd.CommandText = sql;
            da.Fill(dt);
            con.Close();
            return dt;
        }
    }
}
