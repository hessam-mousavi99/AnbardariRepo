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
using BehComponents;
namespace Anbardari
{
    public partial class frmControlKala : Form
    {
        public frmControlKala()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display1()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Kala Where Tedad between '"+txtKalaAz.Text+"' and '"+txtKalaTa.Text+"'";
            da.Fill(ds, "Kala");
            dgvControlKala.DataSource = ds.Tables["Kala"].DefaultView;
            con.Close();
        }
        void display2()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Kala Where Tedad='"+0+"'";
            da.Fill(ds, "Kala");
            dgvControlKala.DataSource = ds.Tables["Kala"].DefaultView;
            con.Close();
        }
        private void frmControlKala_Load(object sender, EventArgs e)
        {
            display2();
            dgvControlKala.Columns[0].HeaderText = "کد";
            dgvControlKala.Columns[1].HeaderText = "گروه کالا";
            dgvControlKala.Columns[2].HeaderText = "نام کالا";
            dgvControlKala.Columns[3].HeaderText = "قیمت خرید";
            dgvControlKala.Columns[4].HeaderText = "قیمت فروش";
            dgvControlKala.Columns[5].HeaderText = "تعداد";
            dgvControlKala.Columns[6].HeaderText = "واحد";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display1();
        }
    }
}
