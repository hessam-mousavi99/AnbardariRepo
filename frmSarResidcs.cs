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
    public partial class frmSarResidcs : Form
    {
        public frmSarResidcs()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display1()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from ChekDaryafti where SarResid between '" + txtAzTarikh1.Text + "' And '" + txtTaTarikh1.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ChekDaryafti");
            dgvListAsnadDaryafti.DataSource = ds.Tables["ChekDaryafti"].DefaultView;
            con.Close();
        }
        void display2()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from ChekPardakhti where SarResid between '" + txtAzTarikh.Text + "' And '" + txtTaTarikh.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ChekPardakhti");
            dgvListAsnadPardakhti.DataSource = ds.Tables["ChekPardakhti"].DefaultView;
            con.Close();
        }
        private void frmSarResidcs_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            txtAzTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            txtTaTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            txtAzTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            txtTaTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            display1();
            display2();
            dgvListAsnadDaryafti.Columns[0].HeaderText = "کد";
            dgvListAsnadDaryafti.Columns[1].HeaderText = "شماره حساب";
            dgvListAsnadDaryafti.Columns[2].HeaderText = "نام حساب";
            dgvListAsnadDaryafti.Columns[3].HeaderText = "شماره سند";
            dgvListAsnadDaryafti.Columns[4].HeaderText = "مبلغ";
            dgvListAsnadDaryafti.Columns[5].HeaderText = "تاریخ ثبت";
            dgvListAsnadDaryafti.Columns[6].HeaderText = "تاریخ سررسید";
            dgvListAsnadDaryafti.Columns[7].HeaderText = "دریافت از";
            dgvListAsnadDaryafti.Columns[8].HeaderText = "وضعیت";
            dgvListAsnadDaryafti.Columns[9].HeaderText = "توضیح";

            dgvListAsnadPardakhti.Columns[0].HeaderText = "کد";
            dgvListAsnadPardakhti.Columns[1].HeaderText = "شماره حساب";
            dgvListAsnadPardakhti.Columns[2].HeaderText = "نام حساب";
            dgvListAsnadPardakhti.Columns[3].HeaderText = "شماره سند";
            dgvListAsnadPardakhti.Columns[4].HeaderText = "مبلغ";
            dgvListAsnadPardakhti.Columns[5].HeaderText = "تاریخ ثبت";
            dgvListAsnadPardakhti.Columns[6].HeaderText = "تاریخ سررسید";
            dgvListAsnadPardakhti.Columns[7].HeaderText = "پرداخت به";
            dgvListAsnadPardakhti.Columns[8].HeaderText = "وضعیت";
            dgvListAsnadPardakhti.Columns[9].HeaderText = "توضیح";
        }

        private void txtAzTarikh1_TextChanged(object sender, EventArgs e)
        {
            display1();
        }

        private void txtTaTarikh1_TextChanged(object sender, EventArgs e)
        {
            display1();
        }

        private void txtAzTarikh_TextChanged(object sender, EventArgs e)
        {
            display2();
        }

        private void txtTaTarikh_TextChanged(object sender, EventArgs e)
        {
            display2();
        }
    }
}
