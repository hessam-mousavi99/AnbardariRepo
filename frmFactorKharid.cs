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
    public partial class frmFactorKharid : Form
    {
        public frmFactorKharid()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HesabdariDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        void displayMoshtari()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "Select * from Moshtari";
            da.Fill(ds, "Moshtari");
            dgvEntekhabMoshtari.DataSource = ds;
            dgvEntekhabMoshtari.DataMember = "Moshtari";
        }
        void displayKala()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "Select * from Kala";
            da.Fill(ds, "Kala");
            dgvEntekhabKala.DataSource = ds;
            dgvEntekhabKala.DataMember = "Kala";
        }
        string Address;
        string Tel;
        private void frmFactorKharid_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(System.Globalization.CultureInfo.CreateSpecificCulture("fa-IR"));
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            txtTarikhSabt.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            displayKala();
            dgvEntekhabKala.Columns[0].HeaderText = "کد";
            dgvEntekhabKala.Columns[1].HeaderText = "گروه کالا";
            dgvEntekhabKala.Columns[2].HeaderText = "نام کالا";
            dgvEntekhabKala.Columns[3].HeaderText = "قیمت خرید";
            dgvEntekhabKala.Columns[4].HeaderText = "قیمت فروش";
            dgvEntekhabKala.Columns[5].HeaderText = "تعداد";
            dgvEntekhabKala.Columns[6].HeaderText = "واحد";
            displayMoshtari();
            dgvEntekhabMoshtari.Columns[0].HeaderText = "کد";
            dgvEntekhabMoshtari.Columns[1].HeaderText = "نام مشتری";
            dgvEntekhabMoshtari.Columns[2].HeaderText = "نوع مشتری";
            dgvEntekhabMoshtari.Columns[3].HeaderText = "تلفن";
            dgvEntekhabMoshtari.Columns[4].HeaderText = "تلفن همراه";
            dgvEntekhabMoshtari.Columns[5].HeaderText = "بدهکار";
            dgvEntekhabMoshtari.Columns[6].HeaderText = "بستانکار";
            cmd.Connection = con;
            cmd.CommandText = "select MaliatKharid from Maliat";
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtArzeshafzoode.Text = dr["MaliatKharid"].ToString();
            }
            con.Close();
            cmd.CommandText = "select * from Tanzimat";
            SqlDataReader dr1;
            con.Open();
            dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                Address = dr1["address"].ToString();
                Tel = dr1["tel"].ToString();
            }
            con.Close();
        }

        private void txtFilterKala_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Kala where NameKala Like '%' + @S +'%'";
            da.SelectCommand.Parameters.AddWithValue("@S", txtFilterKala.Text + "%");
            da.Fill(ds, "Kala");
            dgvEntekhabKala.DataSource = ds;
            dgvEntekhabKala.DataMember = "Kala";
        }

        private void txtFiltereMoshtari_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "select * from Moshtari where NameMoshtari Like '%'+ @n +'%'";
            da.SelectCommand.Parameters.AddWithValue("@n", txtFiltereMoshtari.Text + "%");
            da.Fill(ds, "Moshtari");
            dgvEntekhabMoshtari.DataSource = ds;
            dgvEntekhabMoshtari.DataMember = "Moshtari";
        }

        private void btnSabtKalaToFac_Click(object sender, EventArgs e)
        {
            dgvFactor.Rows.Add(txtIDKala.Text, txtNameKala.Text, txtMablagh.Text, txtTedad.Text);
            txtIDKala.Text = "";
            txtNameKala.Text = "";
            txtMablagh.Text = "";
            txtTedad.Text = "";
            long jameKoleFac = 0;
            int maliat = 0;
            long JameKalaHa=0;
            for (int i = 0; i < dgvFactor.Rows.Count; i++)
            {
                JameKalaHa = JameKalaHa + (Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
                long gheymatKol;
                gheymatKol = Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value);
                dgvFactor.Rows[i].Cells[4].Value = gheymatKol;
            }
            txtJameMablaghKala.Text = JameKalaHa.ToString();
            maliat = Convert.ToInt32(JameKalaHa * txtArzeshafzoode.Value);
            txtAfzoodeKol.Value = maliat;
            jameKoleFac = (JameKalaHa + maliat + txtHazineKhadamat.Value) - txtTakhfif.Value;
            txtJameMablaghFactor.Value =(int) jameKoleFac;
        }

        private void btnDelKalaAzFac_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFactor.Rows.Count==0)
                {
                    MessageBox.Show("کالایی انتخاب نشده است");
                }
                else
                {
                    dgvFactor.Rows.RemoveAt(dgvFactor.CurrentRow.Index);
                }
            }
            catch (Exception)
            {

                
            }
        }

        private void dgvEntekhabMoshtari_MouseUp(object sender, MouseEventArgs e)
        {
            txtIDMoshtari.Text = dgvEntekhabMoshtari[0, dgvEntekhabMoshtari.CurrentRow.Index].Value.ToString();
            txtNameMoshtari.Text = dgvEntekhabMoshtari[1, dgvEntekhabMoshtari.CurrentRow.Index].Value.ToString();
            txtMobile.Text = dgvEntekhabMoshtari[4, dgvEntekhabMoshtari.CurrentRow.Index].Value.ToString();
        }

        private void dgvEntekhabKala_MouseUp(object sender, MouseEventArgs e)
        {
            txtIDKala.Text = dgvEntekhabKala[0, dgvEntekhabKala.CurrentRow.Index].Value.ToString();
            txtNameKala.Text = dgvEntekhabKala[2, dgvEntekhabKala.CurrentRow.Index].Value.ToString();
            txtMablagh.Text = dgvEntekhabKala[3, dgvEntekhabKala.CurrentRow.Index].Value.ToString();
            lblTedad.Text = dgvEntekhabKala[5, dgvEntekhabKala.CurrentRow.Index].Value.ToString();
        }

        private void btnMohasebeFactor_Click(object sender, EventArgs e)
        {
            long jameKoleFac = 0;
            int maliat = 0;
            long JameKalaHa = 0;
            for (int i = 0; i < dgvFactor.Rows.Count; i++)
            {
                JameKalaHa = JameKalaHa + (Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
                //long gheymatKol;
                //gheymatKol = Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value);
                //dgvFactor.Rows[i].Cells[4].Value = gheymatKol;
            }
            txtJameMablaghKala.Text = JameKalaHa.ToString();
            maliat = Convert.ToInt32(JameKalaHa * txtArzeshafzoode.Value);
            txtAfzoodeKol.Value = maliat;
            jameKoleFac = (JameKalaHa + maliat + txtHazineKhadamat.Value) - txtTakhfif.Value;
            txtJameMablaghFactor.Value = (int)jameKoleFac;
        }

        private void btnSabtfactor_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < dgvFactor.Rows.Count - 1; i++)
                {
                    con.Close();
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "insert into FactorKharid (CodeFactor,TarikhFactor,IdMoshtari,NameMoshtari,Mobile,IdKala,NameKala,GheymatKharid,Tedad,MablaghFactor,MaliatKharid,HazineFacor,Takhfif,JameFactor,GheymatKol,Tozih)values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p)";
                    cmd.Parameters.AddWithValue("@a", txtShomareFac.Text);
                    cmd.Parameters.AddWithValue("@b", txtTarikhSabt.Text);
                    cmd.Parameters.AddWithValue("@c", txtIDMoshtari.Text);
                    cmd.Parameters.AddWithValue("@d", txtNameMoshtari.Text);
                    cmd.Parameters.AddWithValue("@e", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@f", Convert.ToInt32(dgvFactor.Rows[i].Cells[0].Value));
                    cmd.Parameters.AddWithValue("@g", dgvFactor.Rows[i].Cells[1].Value);
                    cmd.Parameters.AddWithValue("@h", Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value));
                    cmd.Parameters.AddWithValue("@i", Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
                    cmd.Parameters.AddWithValue("@o", Convert.ToInt32(dgvFactor.Rows[i].Cells[4].Value));
                    cmd.Parameters.AddWithValue("@j", txtJameMablaghKala.Text);
                    cmd.Parameters.AddWithValue("@k", txtAfzoodeKol.Text);
                    cmd.Parameters.AddWithValue("@l", txtHazineKhadamat.Text);
                    cmd.Parameters.AddWithValue("@m", txtTakhfif.Text);
                    cmd.Parameters.AddWithValue("@n", txtJameMablaghFactor.Text);
                    cmd.Parameters.AddWithValue("@p", txtTozih.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    string s;
                    int x;
                    long sum = 0;
                    // con.Open();
                    SqlCommand sc = new SqlCommand("select Tedad from Kala where IdKala='" + Convert.ToInt32(dgvFactor.Rows[i].Cells[0].Value) + "'", con);
                    s = Convert.ToString(sc.ExecuteScalar());
                    x = Convert.ToInt32((dgvFactor.Rows[i].Cells[3].Value));
                    sum += Convert.ToInt32(s) + x;
                    string UpdateQuery = "Update Kala set Tedad='" + sum + "' where IdKala='" + Convert.ToInt32(dgvFactor.Rows[i].Cells[0].Value) + "'";
                    SqlCommand com = new SqlCommand(UpdateQuery, con);
                    com.ExecuteNonQuery();
                    displayKala();
                    con.Close();
                    MessageBoxFarsi.Show("عملیات با موفقیت انجام شد.", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
                }
            }
            catch (Exception)
            {
                MessageBoxFarsi.Show("خطا در انجام عملیات!!", "پیغام", MessageBoxFarsiButtons.OK, MessageBoxFarsiIcon.Information, MessageBoxFarsiDefaultButton.Button1);
            }
        }

        private void btnPNaghdi_Click(object sender, EventArgs e)
        {
            frmPardakht frm = new frmPardakht();
            frm.txtTozih.Text = "پرداخت مبلغ فاکتور خرید به شماره فاکتور" + txtShomareFac.Text;
            frm.txtNamePardakhtkonnde.Text = txtNameMoshtari.Text;
            frm.txtMablagh.Text = txtJameMablaghFactor.Text;
            frm.txtTarikh.Text = txtTarikhSabt.Text;
            frm.ShowDialog();
        }

        private void btnPSanadi_Click(object sender, EventArgs e)
        {
            ChekPardakhti frm = new ChekPardakhti();
            frm.txtTozih.Text = "پرداخت مبلغ فاکتور خرید به شماره فاکتور" + txtShomareFac.Text;
            frm.txtPardakhtBe.Text = txtNameMoshtari.Text;
            frm.txtMablagh.Text = txtJameMablaghFactor.Text;
            frm.txtTarikhSabt.Text = txtTarikhSabt.Text;
            frm.ShowDialog();
        }

        private void btnListFactorHA_Click(object sender, EventArgs e)
        {
            new frmListFactorKharid().ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report1 = new StiReport();
            Report1.Load("Report/ReportFactorKharid.mrt");
            Report1.Compile();
            Report1["CodeFactor"] = Convert.ToInt32(txtShomareFac.Text);
            Report1["address"] = Address;
            Report1["tel"] = Tel;
            Report1.ShowWithRibbonGUI();
        }
    }
}
