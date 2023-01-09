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
    public partial class frmGozareshPD : Form
    {
        public frmGozareshPD()
        {
            InitializeComponent();
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {
            Stimulsoft.Report.StiReport Report1 = new Stimulsoft.Report.StiReport();
            Report1.Load("Report/ReportPardakhti.mrt");
            Report1.Compile();
            Report1["NameMoshtari"] = txtPardakhtKonnde.Text;      
            Report1.ShowWithRibbonGUI();
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            Stimulsoft.Report.StiReport Report1 = new Stimulsoft.Report.StiReport();
            Report1.Load("Report/ReportDaryafti.mrt");
            Report1.Compile();
            Report1["NameMoshtari"] = txtDaryaftkonnde.Text;           
            Report1.ShowWithRibbonGUI();
        }
    }
}
