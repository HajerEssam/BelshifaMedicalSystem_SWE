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
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace SWEProject
{
    public partial class Form1 : Form
    {
        OracleConnection conn;
        string ordb = "Data Source = ORCL ; User Id = scott; Password = tiger";
        public static int UID;
        public static string UType;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT UserID, UType From Users where Email =: mail and UPassword =: pass"; //SelectUser
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("mail", textBox1.Text.ToString());
            cmd.Parameters.Add("pass", textBox2.Text.ToString());

            //cmd.Parameters.Add("Userid", OracleDbType.Int32, ParameterDirection.Output);
            //cmd.Parameters.Add("utype", OracleDbType.Varchar2, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                UID = int.Parse(dr[0].ToString());
                UType = dr[1].ToString();
                Form5 form5 = new Form5();
                form5.Show();
            }
            else
            {
                MessageBox.Show("Log in failed");
            }
            dr.Close();
            /*int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                UID = int.Parse(cmd.Parameters[0].Value.ToString());
                string userType = cmd.Parameters[1].Value.ToString();

                MessageBox.Show("Logged in successfully as " + userType);

                if (userType == "user")
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                }
                else if (userType == "admin")
                {
                    Form3 form3 = new Form3();
                    form3.Show();
                }
            }
            else
            {
                MessageBox.Show("Log in failed");
            }*/
        }
    }
}
