using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
namespace SWEProject
{
    public partial class Form6 : Form
    {
        CrystalReport1 c;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            c = new CrystalReport1();
            foreach(ParameterDiscreteValue v in c.ParameterFields[0].DefaultValues)
            {
                comboBox1.Items.Add(v.Value);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            c.SetParameterValue(0, comboBox1.Text);
            crystalReportViewer1.ReportSource = c;
        }
    }
}
