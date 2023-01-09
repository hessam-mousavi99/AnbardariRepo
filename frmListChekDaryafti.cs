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
    public partial class frmListChekDaryafti : Form
    {
        public frmListChekDaryafti()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from ChekDaryafti where SarResid between '" + txtAzTarikh.Text + "' And '" + txtTaTarikh.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ChekDaryafti");
            dgvListAsnadd.DataSource = ds.Tables["ChekDaryafti"].DefaultView;
            con.Close();
        }
        private void frmListChekDaryafti_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            txtAzTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            txtTaTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            display();
            dgvListAsnadd.Columns[0].HeaderText = "کد";
            dgvListAsnadd.Columns[1].HeaderText = "شماره حساب";
            dgvListAsnadd.Columns[2].HeaderText = "نام حساب";
            dgvListAsnadd.Columns[3].HeaderText = "شماره سند";
            dgvListAsnadd.Columns[4].HeaderText = "مبلغ";
            dgvListAsnadd.Columns[5].HeaderText = "تاریخ ثبت";
            dgvListAsnadd.Columns[6].HeaderText = "تاریخ سررسید";
            dgvListAsnadd.Columns[7].HeaderText = "دریافت از";
            dgvListAsnadd.Columns[8].HeaderText = "وضعیت";
            dgvListAsnadd.Columns[9].HeaderText = "توضیح";
        }

        private void txtTaTarikh_TextChanged(object sender, EventArgs e)
        {
            display();
        }

        private void txtAzTarikh_TextChanged(object sender, EventArgs e)
        {
            display();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvListAsnadd.SelectedCells[0].Value);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from ChekDaryafti where IdChek=@i";
                cmd.Parameters.AddWithValue("@i", x);
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

        private void btnVosol_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                long s;
                long sum = 0;
                con.Open();
                SqlCommand sc = new SqlCommand("select Mojodi from Hesabha where ShomareHesab='" + (dgvListAsnadd.SelectedCells[1].Value) + "'", con);
                str = Convert.ToString(sc.ExecuteScalar());
                s = Convert.ToInt64(dgvListAsnadd.SelectedCells[4].Value);
                //if (s > Convert.ToInt64(str))
                //{
                //    MessageBox.Show("موجودی حساب برای وصول این سند کافی نمیباشد");
                //}
                //else
                //{
                    sum += Convert.ToInt64(str) + s;
                    string UpdateQuery = "Update Hesabha set Mojodi='" + sum + "' where  ShomareHesab='" + dgvListAsnadd.SelectedCells[1].Value + "'";
                    SqlCommand c = new SqlCommand(UpdateQuery, con);
                    c.ExecuteNonQuery();
                    string UpdateVaziat = "update ChekDaryafti set Vaziat=N'وصول شده' where  ShomareSanad='" + Convert.ToInt32(dgvListAsnadd.SelectedCells[3].Value) + "'";
                    SqlCommand c1 = new SqlCommand(UpdateVaziat, con);
                    c1.ExecuteNonQuery();
                    MessageBoxFarsi.Show("وصول با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
                    display();
                //}
                con.Close();
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmChekDaryafti frm = new frmChekDaryafti();
            frm.txtId.Text = dgvListAsnadd[0, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.txtShomareHesab.Text = dgvListAsnadd[1, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.txtNameHesab.Text = dgvListAsnadd[2, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.txtShomareSanad.Text = dgvListAsnadd[3, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvListAsnadd[4, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.txtTarikhSabt.Text = dgvListAsnadd[5, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.txtTarikhsarresidChek.Text = dgvListAsnadd[6, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.txtDaryaftAz.Text = dgvListAsnadd[7, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.cmbVaziat.Text = dgvListAsnadd[8, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvListAsnadd[9, dgvListAsnadd.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Stimulsoft.Report.StiReport Report1 = new Stimulsoft.Report.StiReport();
            Report1.Load("Report/ReportListAsnadDaryafti.mrt");
            Report1.Compile();
            Report1["Tarikh1"] = txtAzTarikh.Text;
            Report1["Tarikh2"] = txtTaTarikh.Text;
            Report1.ShowWithRibbonGUI();
        }
    }
}
