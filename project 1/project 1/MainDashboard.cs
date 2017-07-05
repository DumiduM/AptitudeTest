using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_1
{
    public partial class MainDashboard : Form
    {
        public MainDashboard()
        {
            InitializeComponent();
        }

        

        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        

        private void MainDashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox8.Visible = true;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Visible = false;
        }

        private void pictureBox9_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.Visible = false;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.Visible = true;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox11.Visible = true;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox10.Visible = true;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox14.Visible = true;
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox13.Visible = true;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox12.Visible = true;
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox10.Visible = false;
        }

        private void pictureBox14_MouseLeave(object sender, EventArgs e)
        {
            pictureBox14.Visible = false;
        }

        private void pictureBox12_MouseLeave(object sender, EventArgs e)
        {
            pictureBox12.Visible = false;
        }

        private void pictureBox11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox11.Visible = false;
        }

        private void pictureBox13_MouseLeave(object sender, EventArgs e)
        {
            pictureBox13.Visible = false;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            //panel1.Visible = true;
        }
    }
}
