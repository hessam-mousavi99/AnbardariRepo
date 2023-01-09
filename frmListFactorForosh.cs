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
using Stimulsoft.Report;
namespace Anbardari
{
    public partial class frmListFactorForosh : Form
    {
        public frmListFactorForosh()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Distinct CodeFactor,TarikhFactor,NameMoshtari,JameFactor,Tozih from FactorForosh where TarikhFactor between '" + txtAzTarikh.Text + "' And '" + txtTaTarikh.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "FactorForosh");
            dgvFactor.DataSource = ds.Tables["FactorForosh"].DefaultView;
            con.Close();
        }
        private void frmListFactorForosh_Load(object sender, EventArgs e)
        {
            display();
           
            dgvFactor.Columns[0].HeaderText = "شماره فاکتور";
            dgvFactor.Columns[1].HeaderText = "تاریخ فاکتور";
            dgvFactor.Columns[2].HeaderText = "نام مشتری";
            dgvFactor.Columns[3].HeaderText = "جمع فاکتور";
            dgvFactor.Columns[4].HeaderText = "توضیح";
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            txtAzTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            txtTaTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");

        }

        private void txtAzTarikh_TextChanged(object sender, EventArgs e)
        {
            display();
        }

        private void txtTaTarikh_TextChanged(object sender, EventArgs e)
        {
            display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvFactor.SelectedCells[1].Value);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from FactorForosh where Codefactor =@s";
                cmd.Parameters.AddWithValue("@s", x);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report1 = new StiReport();
            Report1.Load("Report/ReportGozareshFactorForosh.mrt");
            Report1.Compile();
            Report1["Tarikh1"] = txtAzTarikh.Text;
            Report1["Tarikh2"] = txtTaTarikh.Text;
            Report1.ShowWithRibbonGUI();
        }
    }
}
