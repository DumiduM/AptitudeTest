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
    public partial class Applicantion : Form
    {
        private static int trigger;

        public Applicantion()
        {
            InitializeComponent();
            yearAdjustment();
            //panel3.Hide();
           // panel4.Hide();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0 ;
            tabControl1.SelectedTab = tabPage7;
            for (int i = 2000; i <= 2050; i++)
                comboBox4.Items.Add(i+"/"+(i+1));
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void yearAdjustment()
        {
             var time = DateTime.Now;
             string time1 = time.ToString(" yyyy/MM/dd , hh:mm ");
             //label21.Text = time1;
             string formattedTime = time.ToString("yyyy");
             int yearBefore = int.Parse(formattedTime)-1;
             //label4.Text = yearBefore + "/" + formattedTime; 
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) 
            {
                panel3.Visible = true ;
                panel4.Visible = false;
                    
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = true ;
            panel3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.doenets.lk/exam/");
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error:" + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.doenets.lk/exam/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }


        private void Ctrl_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            
            
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox8.Visible = true;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            if(trigger!=1)
                pictureBox8.Visible = false;
        }

        private void pictureBox9_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            if(trigger!=2)
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
            if (trigger != 4)
                pictureBox10.Visible = false;
        }

        private void pictureBox14_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 5)
                pictureBox14.Visible = false;
        }

        private void pictureBox12_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 7)
                pictureBox12.Visible = false;
        }

        private void pictureBox11_MouseLeave(object sender, EventArgs e)
        {
            if(trigger!=3)
                pictureBox11.Visible = false;
        }

        private void pictureBox13_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 6) pictureBox13.Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MainDashboard obj1 = new MainDashboard();
            this.Hide();
            obj1.Show(); 
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            trigger = 1;

            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl_login_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_MouseHover(object sender, EventArgs e)
        {
            btn_login.Visible = true;
        }

        private void pictureBox16_MouseHover(object sender, EventArgs e)
        {
            btn_exit.Visible = true;
        }

        private void btn_login_MouseLeave(object sender, EventArgs e)
        {
            btn_login.Visible = false;
        }

        private void btn_exit_MouseLeave(object sender, EventArgs e)
        {
            btn_exit.Visible = false;
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            lbl_forgotPs.ForeColor = Color.Blue;
        }

        private void lbl_forgotPs_DragLeave(object sender, EventArgs e)
        {

        }

        private void lbl_forgotPs_MouseLeave(object sender, EventArgs e)
        {
            lbl_forgotPs.ForeColor = Color.Black;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            pnl_buttons.Enabled = true;
            //pnl_login.Visible = false;
            tabControl1.SelectedTab = tabPage8;

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            trigger = 4;

            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            //pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsubmit1_MouseHover(object sender, EventArgs e)
        {
            btnsbmit.Visible = true;
        }

        private void btnsbmit_MouseLeave(object sender, EventArgs e)
        {
            btnsbmit.Visible = false;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
            trigger = 6;

            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            //pictureBox13.Visible = false;
            pictureBox14.Visible = false;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            trigger = 2;

            pictureBox8.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;

        }

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            trigger = 3;

            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            //pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
            trigger = 5;

            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            //pictureBox14.Visible = false;
        }

        private void pictureBox12_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage7;
            trigger = 7;

            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            //pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;

        }

        private void pictureBox20_MouseHover(object sender, EventArgs e)
        {
            pictureBox21.Visible = true;
        }

        private void pictureBox21_MouseLeave(object sender, EventArgs e)
        {
            pictureBox21.Visible = false;
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox23_MouseEnter(object sender, EventArgs e)
        {
            pictureBox22.Visible = true;
        }

        private void pictureBox22_MouseLeave(object sender, EventArgs e)
        {
            pictureBox22.Visible = false;
        }

        private void pictureBox25_MouseHover(object sender, EventArgs e)
        {
            pictureBox24.Visible = true;
        }

        private void pictureBox24_MouseLeave(object sender, EventArgs e)
        {
            pictureBox24.Visible = false;
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            
        }

        private void btnsbmit_Click(object sender, EventArgs e)
        {
            if (textBox32.Text == "")
                label50.Visible = true;
            else
                label50.Visible = false;

            if (textBox33.Text == "")
                label51.Visible = true;
            else
                label51.Visible = false;

            if (textBox34.Text == "")
                label52.Visible = true;
            else
                label52.Visible = false;

            if (maskedTextBox2.Text == "")
                label53.Visible = true;
            else
                label53.Visible = false;

            if (maskedTextBox3.Text != maskedTextBox2.Text && maskedTextBox2.Text != "")
                label54.Visible = true;
            else
                label54.Visible = false;

            pnlWrn.Visible = true;
        }

        private void pnladduser1_MouseClick(object sender, MouseEventArgs e)
        {
            pnlWrn.Visible = false;
        }

        private void pnlWrn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnladduser1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnladduser1_MouseEnter(object sender, EventArgs e)
        {
            pnlWrn.Visible = false;
        }

        private void pictureBox8_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pnl_buttons_MouseMove(object sender, MouseEventArgs e)
        { }
    }
}
