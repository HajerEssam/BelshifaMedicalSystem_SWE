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
    public partial class Form2 : Form
    {
        string ordb = "Data Source = ORCL ; User Id = scott; Password = tiger";
        OracleConnection conn;
        int Available;
        string originalQuantity;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand(ordb);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT ProdName From Product";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(ordb);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * From Product where ProdName = : name";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("name", comboBox1.SelectedItem.ToString());

            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["Price"].ToString();
                textBox2.Text = dr["Offer"].ToString();

                textBox3.Text = Convert.ToString(float.Parse(dr["Price"].ToString()) * float.Parse(dr["offer"].ToString()) / 100);
                decimal price = Convert.ToDecimal(dr["Price"]);
                int offer = Convert.ToInt32(dr["Offer"]);

                decimal discountedPrice = price - (price * offer / 100);
                textBox3.Text = discountedPrice.ToString();
                textBox4.Text = dr["ProdCategory"].ToString();
                textBox5.Text = dr["ProdID"].ToString();
                Available = Convert.ToInt32(dr["Quantity"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int MID, DID;
            OracleCommand cmd1 = new OracleCommand(ordb);
            cmd1.Connection = conn;
            cmd1.CommandText = "OrdersMaxID";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            cmd1.ExecuteNonQuery();
            try
            {
                MID = Convert.ToInt32(cmd1.Parameters["id"].Value.ToString());
                MID= MID+1;
            }
            catch
            {
                MID = 1;
            }
            OracleCommand cmd2 = new OracleCommand(ordb);
            cmd2.Connection = conn;
            cmd2.CommandText = "INSERT into Orders values (:Oid , :CID)";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("Oid", MID.ToString());
            cmd2.Parameters.Add("CID", Form1.UID.ToString());
            int r2 = cmd2.ExecuteNonQuery();
            if (r2 == -1)
                MessageBox.Show("faild");

            OracleCommand cmd3 = new OracleCommand(ordb);
            cmd3.Connection = conn;
            cmd3.CommandText = "OrderDetailsMaxID";
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            cmd3.ExecuteNonQuery();
            try
            {
                DID = Convert.ToInt32(cmd3.Parameters["id"].Value.ToString());
                DID = DID + 1;
            }
            catch
            {
                DID = 1;
            }
            originalQuantity = textBox6.Text;
            if (Convert.ToInt32(textBox6.Text) > Available)
            {
                MessageBox.Show("Available Quantity is " + Available.ToString());
            }
            OracleCommand c = new OracleCommand(ordb);
            c.Connection = conn;
            c.CommandText = "INSERT into OrderDetails values (:id , :OID , :ProdID , :Quantity)";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("id", DID.ToString());
            c.Parameters.Add("OID", MID.ToString());
            c.Parameters.Add("ProdID", textBox5.Text);
            c.Parameters.Add("Quantity", textBox6.Text);
        }
    }
}
