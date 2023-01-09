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
    public partial class frmSood : Form
    {
        public frmSood()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from FactorForosh where TarikhFactor between '" + txtAzTarikh.Text + "' And '" + txtTaTarikh.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "FactorForosh");
            dgvSoodozian.DataSource = ds.Tables["FactorForosh"].DefaultView;
            con.Close();
        }
        private void frmSood_Load(object sender, EventArgs e)
        {
            display();
            dgvSoodozian.Columns[1].HeaderText = "شماره فاکتور";
            dgvSoodozian.Columns[2].HeaderText = "تاریخ فاکتور";
            dgvSoodozian.Columns[4].HeaderText = "نام مشتری";
            dgvSoodozian.Columns[17].HeaderText = "جمع فاکتور";
            dgvSoodozian.Columns[18].HeaderText = "توضیح";
            dgvSoodozian.Columns[9].HeaderText = "قیمت خرید";
            dgvSoodozian.Columns[11].HeaderText = "قیمت فروش";
            dgvSoodozian.Columns[7].HeaderText = "نام کالا";
            dgvSoodozian.Columns[0].Visible = false;
            dgvSoodozian.Columns[3].Visible = false;
            dgvSoodozian.Columns[5].Visible = false;
            dgvSoodozian.Columns[6].Visible = false;
            dgvSoodozian.Columns[8].Visible = false;
            dgvSoodozian.Columns[10].Visible = false; 
            dgvSoodozian.Columns[12].Visible = false;
            dgvSoodozian.Columns[13].Visible = false;
            dgvSoodozian.Columns[15].Visible = false;
            dgvSoodozian.Columns[14].Visible = false;
            dgvSoodozian.Columns[16].Visible = false;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            txtAzTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            txtTaTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
        }

        private void btnSoodvZian_Click(object sender, EventArgs e)
        {
            display();
            decimal sum1 = 0, s = 0, sum2 = 0;
            for (int i = 0; i < dgvSoodozian.Rows.Count; i++)
            {
                sum1 += Convert.ToDecimal(dgvSoodozian.Rows[i].Cells[11].Value);//کل فروش
                s += Convert.ToDecimal(dgvSoodozian.Rows[i].Cells[9].Value);//کل خرید
            }
            sum2 = sum1 - s;
            if (sum2>0)
            {
                lblSood.Text = sum2.ToString("###,###,###,###");
            }
            else
            {
                lblZian.Text = sum2.ToString("###,###,###,###");
            }
        }

        private void txtAzTarikh_TextChanged(object sender, EventArgs e)
        {
            display();
        }

        private void txtTaTarikh_TextChanged(object sender, EventArgs e)
        {
            display();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Stimulsoft.Report.StiReport Report1 = new Stimulsoft.Report.StiReport();
            Report1.Load("Report/ReportSoodvZiyan.mrt");
            Report1.Compile();
            Report1["Tarikh1"]= txtAzTarikh.Text;
            Report1["Tarikh2"]= txtTaTarikh.Text;
            Report1["Sood"]= lblSood.Text;
            Report1["Ziyan"]= lblZian.Text;
            Report1.ShowWithRibbonGUI();
        }
    }
}
