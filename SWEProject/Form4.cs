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
    public partial class Form4 : Form
    {
        string ordb = "Data Source = ORCL ; User Id = scott; Password = tiger";
        OracleConnection conn;
        int OrderID = 0;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "ShowUserOrders";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Uid",2);
            cmd.Parameters.Add("ProdNames", OracleDbType.RefCursor, ParameterDirection.Output);

            OracleDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
        }
    }
}
