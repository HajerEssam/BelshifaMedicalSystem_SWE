using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWEProject
{
    public partial class Form3 : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet dataset;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string ordb = "Data Source = ORCL ; User Id = scott; Password = tiger";
            OracleConnection conn;
            conn = new OracleConnection(ordb);
            string s = "SELECT * From Product";
            dataset = new DataSet();
            adapter = new OracleDataAdapter(s, conn);
            adapter.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(dataset.Tables[0]);
        }
    }
}
