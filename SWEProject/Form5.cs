using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWEProject
{
    public partial class Form5 : Form
    {
        bool isuser = true;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            if (Form1.UType == "user")
            {
                isuser = true;
            }
            else if (Form1.UType == "admin")
            {
                isuser = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(isuser)
            {
                Form4 form4 = new Form4();
                form4.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isuser)
            {
                Form2 form2 = new Form2();
                form2.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //////crystal Report here
            if (!isuser)
            {
                Form6 form6 = new Form6();
                form6.Show();
            }
            else
            {
                MessageBox.Show("this page is for admins!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!isuser)
            {
                Form3 form3 = new Form3();
                form3.Show();
            }
            else
            {
                MessageBox.Show("this page is for admins!");
            }
        }
    }
}
