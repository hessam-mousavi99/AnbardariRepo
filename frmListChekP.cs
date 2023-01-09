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
    public partial class frmListChekP : Form
    {
        public frmListChekP()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from ChekPardakhti where SarResid between '" + txtAzTarikh.Text + "' And '" + txtTaTarikh.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ChekPardakhti");
            dgvListAsnad.DataSource = ds.Tables["ChekPardakhti"].DefaultView;
            con.Close();
        }
        private void frmListChekP_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            txtAzTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            txtTaTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            display();
            dgvListAsnad.Columns[0].HeaderText = "کد";
            dgvListAsnad.Columns[1].HeaderText = "شماره حساب";
            dgvListAsnad.Columns[2].HeaderText = "نام حساب";
            dgvListAsnad.Columns[3].HeaderText = "شماره سند";
            dgvListAsnad.Columns[4].HeaderText = "مبلغ";
            dgvListAsnad.Columns[5].HeaderText = "تاریخ ثبت";
            dgvListAsnad.Columns[6].HeaderText = "تاریخ سررسید";
            dgvListAsnad.Columns[7].HeaderText = "پرداخت به";
            dgvListAsnad.Columns[8].HeaderText = "وضعیت";
            dgvListAsnad.Columns[9].HeaderText = "توضیح";
        }

        private void btnVosol_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                long s;
                long sum = 0;
                con.Open();
                SqlCommand sc = new SqlCommand("select Mojodi from Hesabha where ShomareHesab='" + (dgvListAsnad.SelectedCells[1].Value) + "'", con);
                str = Convert.ToString(sc.ExecuteScalar());
                s = Convert.ToInt64(dgvListAsnad.SelectedCells[4].Value);
                if (s > Convert.ToInt64(str))
                {
                    MessageBox.Show("موجودی حساب برای وصول این سند کافی نمیباشد");
                }
                else
                {
                    sum += Convert.ToInt64(str) - s;
                    string UpdateQuery = "Update Hesabha set Mojodi='" + sum + "' where  ShomareHesab='" + dgvListAsnad.SelectedCells[1].Value + "'";
                    SqlCommand c = new SqlCommand(UpdateQuery, con);
                    c.ExecuteNonQuery();
                    string UpdateVaziat = "update ChekPardakhti set Vaziat=N'وصول شده' where  ShomareSanad='" +Convert.ToInt32( dgvListAsnad.SelectedCells[3].Value) + "'";
                    SqlCommand c1 = new SqlCommand(UpdateVaziat, con);
                    c1.ExecuteNonQuery();
                    MessageBoxFarsi.Show("وصول با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
                    display();
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvListAsnad.SelectedCells[0].Value);
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "delete from ChekPardakhti where IdChek=@I";
                cmd.Parameters.AddWithValue("@I", x);
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

        private void btnEdite_Click(object sender, EventArgs e)
        {
            ChekPardakhti frm = new ChekPardakhti();
            frm.txtId.Text = dgvListAsnad[0, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.txtShomareHesab.Text = dgvListAsnad[1, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.txtNameHesab.Text = dgvListAsnad[2, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.txtShomareSanad.Text = dgvListAsnad[3, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvListAsnad[4, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.txtTarikhSabt.Text = dgvListAsnad[5, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.txtTarikhsarresidChek.Text = dgvListAsnad[6, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.txtPardakhtBe.Text = dgvListAsnad[7, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.cmbVaziat.Text = dgvListAsnad[8, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvListAsnad[9, dgvListAsnad.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
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
            Report1.Load("Report/ReportListAsnadPardakhti.mrt");
            Report1.Compile();
            Report1["Tarikh1"] = txtAzTarikh.Text;
            Report1["Tarikh2"] = txtTaTarikh.Text;
            Report1.ShowWithRibbonGUI();
        }
    }
}
