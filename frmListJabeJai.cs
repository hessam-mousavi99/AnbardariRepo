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
    public partial class frmListJabeJai : Form
    {
        public frmListJabeJai()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from JabeJaiAnbar where Tarikh between '" + txtAzTarikh.Text + "' And '" + txtTaTarikh.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "JabeJaiAnbar");
            dgvAnbar.DataSource = ds.Tables["JabeJaiAnbar"].DefaultView;
            con.Close();
            dgvAnbar.Columns[0].HeaderText = "کد";
            dgvAnbar.Columns[1].HeaderText = "نام کالا";
            dgvAnbar.Columns[2].HeaderText = "از انبار";
            dgvAnbar.Columns[3].HeaderText = "تعداد";
            dgvAnbar.Columns[4].HeaderText = "به انبار";
            dgvAnbar.Columns[5].HeaderText = "تاریخ";
        }
        void display1()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from JabeJaiAnbar";
            da.Fill(ds, "JabeJaiAnbar");
            dgvAnbar.DataSource = ds;
            dgvAnbar.DataMember = "JabeJaiAnbar";
            dgvAnbar.Columns[0].HeaderText = "کد";
            dgvAnbar.Columns[1].HeaderText = "نام کالا";
            dgvAnbar.Columns[2].HeaderText = "از انبار";
            dgvAnbar.Columns[3].HeaderText = "تعداد";
            dgvAnbar.Columns[4].HeaderText = "به انبار";
            dgvAnbar.Columns[5].HeaderText = "تاریخ";
        }
        private void frmListJabeJai_Load(object sender, EventArgs e)
        {
          
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
                int x = Convert.ToInt32(dgvAnbar.SelectedCells[0].Value);
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "delete from JabeJaiAnbar Where IdAnbar=@n";
                cmd.Parameters.AddWithValue("@n",x);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                display();
                MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void txtNameKala_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from JabeJaiAnbar where NameKala Like '%' + @S +'%'";
            da.SelectCommand.Parameters.AddWithValue("@S", txtNameKala.Text + "%");
            da.Fill(ds, "JabeJaiAnbar");
            dgvAnbar.DataSource = ds;
            dgvAnbar.DataMember = "JabeJaiAnbar";
        
        }

        private void txtGroohKala_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from JabeJaiAnbar where NameAnbar Like '%' + @S +'%'";
            da.SelectCommand.Parameters.AddWithValue("@S", txtGroohKala.Text + "%");
            da.Fill(ds, "JabeJaiAnbar");
            dgvAnbar.DataSource = ds;
            dgvAnbar.DataMember = "JabeJaiAnbar";
        
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            display1();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report1 = new StiReport();
            Report1.Load("Report/ReportGozareshJabejaiAnbar.mrt");
            Report1.Compile();
            Report1["Tarikh1"] = txtAzTarikh.Text;
            Report1["Tarikh2"] = txtTaTarikh.Text;
            Report1.ShowWithRibbonGUI();
        }
    }
}
