using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anbardari
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttontanzimat_Click(object sender, EventArgs e)
        {
            new FrmTanzimat().ShowDialog();
        }

        private void btnkarbar_Click(object sender, EventArgs e)
        {
            new frmKarbar().ShowDialog();
        }

        private void btnAnbar_Click(object sender, EventArgs e)
        {
            new frmAnbar().ShowDialog();
        }

        private void btnGrooh_Click(object sender, EventArgs e)
        {
            new frmGroup().ShowDialog();
        }

        private void btnKala_Click(object sender, EventArgs e)
        {
            new frmKala().ShowDialog();
        }

        private void btnMoshtari_Click(object sender, EventArgs e)
        {
            new frmMoshtari().ShowDialog();
        }

        private void btnHesabha_Click(object sender, EventArgs e)
        {
            new frmHesab().ShowDialog();
        }

        private void btnVariz_Click(object sender, EventArgs e)
        {
            new frmVariz().ShowDialog();
        }

        private void btnPardakht_Click(object sender, EventArgs e)
        {
            new frmPardakht().ShowDialog();
        }

        private void btnChekPardakhti_Click(object sender, EventArgs e)
        {
            new ChekPardakhti().ShowDialog();
        }

        private void btnDaryafti_Click(object sender, EventArgs e)
        {
            new frmChekDaryafti().ShowDialog();
        }

        private void btnHazine_Click(object sender, EventArgs e)
        {
            new frmHazine().ShowDialog();
        }

        private void btnDarAmad_Click(object sender, EventArgs e)
        {
            new frmDarAmad().ShowDialog();
        }

        private void btnMaliat_Click(object sender, EventArgs e)
        {
            new frmMaliat().ShowDialog();
        }

        private void btnFactorKharid_Click(object sender, EventArgs e)
        {
            new frmFactorKharid().ShowDialog();
        }

        private void btnFactorForosh_Click(object sender, EventArgs e)
        {
            new frmFactorForosh().ShowDialog();
        }

        private void btnSoodvZian_Click(object sender, EventArgs e)
        {
            new frmSood().ShowDialog();
        }

        private void BtnSarResid_Click(object sender, EventArgs e)
        {
            new frmSarResidcs().ShowDialog();
        }

        private void btnJabejai_Click(object sender, EventArgs e)
        {
            new frmJabejaiAnbar().ShowDialog();
        }

        private void btnGozareshJabejai_Click(object sender, EventArgs e)
        {
            new frmListJabeJai().ShowDialog();
        }

        private void BtnContacts_Click(object sender, EventArgs e)
        {
            new frmContacts().ShowDialog();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("wordpad");
        }
    }
}
