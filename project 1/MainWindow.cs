using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using project_1.Queries;

namespace project_1
{
    public partial class MainWindow : Form
    {
        private static int trigger;
        DbConnect conn = new DbConnect();
        ApplicantDetails editApplicant = new ApplicantDetails();
        ExamHallDetails editHall = new ExamHallDetails();
        UserDetails editUser = new UserDetails();
        TresholdDetails editTreshold = new TresholdDetails();
        CheckQualifiedApplicant checkQualification = new CheckQualifiedApplicant();

        MySqlDataAdapter adap;
        DataSet ds;
        MySqlCommandBuilder cmdbl;

        private static int check = 0;
        private static int payslip = 0;
        private static int AlCertificate = 0;
        private static int OlCertificate = 0;

        private string bio = "";
        private string maths = "";
        private string chem = "";
        private string IT = "";
        private int physicalChecked = 0;



        public MainWindow()
        {

            InitializeComponent();
            // yearAdjustment();
            radioButton3.Hide();

            for (int i = 2014; i <= 2100; i++)
                comboAcademicYr.Items.Add(i + "/" + (i + 1));
            

            comboAcademicYr.SelectedItem = (Convert.ToInt32(DateTime.Today.Year.ToString()) - 1) + "/" + DateTime.Today.Year.ToString();
            
            hidePanels();
            pnlButonsAdmin.Visible = false;



            

        }

        public void hidePanels()
        {
            pnl_AppDetail.Visible = false;
            pnlResults.Visible = false;
            pnlAppList.Visible = false;
            pnlThresold.Visible = false;
            pnladduser.Visible = false;
            pnlExamHalls.Visible = false;
            pnlAnalyse.Visible = false;
        }

        public void updateExamHallTable()
        {

            try
            {
                conn.connection();
                MySqlCommand command = new MySqlCommand();
                command.Connection = conn.connection();
                command.CommandText = "select * from examhall";

                DataTable data = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(data);


                dataGridExamHalls.DataSource = data;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
        }

        public void RemoveExamHallTable()
        {

            string ID;
            ID = Convert.ToString(dataGridExamHalls.CurrentRow.Cells[0].Value);

            try
            {

                conn.connection();
                MySqlCommand command = new MySqlCommand();
                command.Connection = conn.connection();
                command.CommandText = "DELETE from examhall where hallID='" + ID + "'  ";

                DataTable data = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(data);


                //dataGridExamHalls.DataSource = data;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }

        }

        public void updateUserTable()
        {

            try
            {
                conn.connection();
                MySqlCommand command = new MySqlCommand();
                command.Connection = conn.connection();
                command.CommandText = "select FName,LName,EmpID,Privilage from user";

                DataTable data = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(data);


                dataGridUser.DataSource = data;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
        }

        public void updateResultsTable()
        {

            try
            {
                conn.connection();

                adap = new MySqlDataAdapter("select ExamIndex, A1, A0, B1, B0, C1, C0, Total from exam", conn.connection());
                ds = new DataSet();
                adap.Fill(ds, "Results");
                dataGridResults.DataSource = ds.Tables[0];


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
        }

        public void updateApplicantListTables()
        {
            try
            {
                conn.connection();

                adap = new MySqlDataAdapter("select ALIndex, FName, SName, OLCertificate,ALCertificate,PaySlip from applicant where OLCertificate = '1' and ALCertificate = '1' and PaySlip = '1'", conn.connection());
                ds = new DataSet();
                adap.Fill(ds, "list");
                dataGridFinalized.DataSource = ds.Tables[0];


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }

            try
            {
                //conn.connection();

                adap = new MySqlDataAdapter("select ALIndex, FName, SName, OLCertificate,ALCertificate,PaySlip from applicant where OLCertificate = '0' or ALCertificate = '0' or PaySlip = '0'", conn.connection());
                ds = new DataSet();
                adap.Fill(ds, "list");
                dataGridMissingDocs.DataSource = ds.Tables[0];


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
        }

        public void getThreshold()
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                conn.connection();
                MySqlCommand command = new MySqlCommand("select ANumeric,BNumeric,CNumeric,TotalNumeric,APercentage,BPercentage,CPercentage,TotalPercentage from thresholdmarks where year ='" + comboAcademicYr.SelectedItem.ToString() + "'", conn.connection());
                MySqlDataReader read2 = command.ExecuteReader();

                while (read2.Read())
                {
                    textBox39.Text = (read2["ANumeric"].ToString());
                    textBox38.Text = (read2["BNumeric"].ToString());
                    textBox37.Text = (read2["CNumeric"].ToString());
                    textBox36.Text = (read2["TotalNumeric"].ToString());
                    textBox45.Text = (read2["APercentage"].ToString());
                    textBox46.Text = (read2["BPercentage"].ToString());
                    textBox47.Text = (read2["CPercentage"].ToString());
                    textBox48.Text = (read2["TotalPercentage"].ToString());

                }
                conn.closeConnection();
                read2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillApplicant(string id)
        {
            maths = "";
            bio = "";
            chem = "";
            IT = "";



            try
            {
                if (conn.connection().State == System.Data.ConnectionState.Closed)
                    conn.connection().Open();

                conn.connection();
                MySqlCommand command2 = new MySqlCommand("select p1,p2,p3,p4,p5,p6,p7,p8,p9,ALindex,pref from ugc_pref where ALindex ='" + id.ToString() + "'", conn.connection());
                MySqlDataReader read5 = command2.ExecuteReader();
                try
                {
                    while (read5.Read())
                    {
                        textBox22.Text = (read5["p1"].ToString());
                        textBox23.Text = (read5["p2"].ToString());
                        textBox24.Text = (read5["p3"].ToString());
                        textBox25.Text = (read5["p4"].ToString());
                        textBox26.Text = (read5["p5"].ToString());
                        textBox27.Text = (read5["p6"].ToString());
                        textBox28.Text = (read5["p7"].ToString());
                        textBox29.Text = (read5["p8"].ToString());
                        textBox31.Text = (read5["p9"].ToString());
                        comboBox2.SelectedItem = (read5["pref"].ToString());
                    }


                    read5.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



                MySqlCommand command = new MySqlCommand("select Title,FName,SName,NIC,year,Address,Tel,District,OLCertificate,ALIndex,ALCertificate,PaySlip,Zscore, IT,GenEng,Bio,Physics,Chemistry,CombinedMaths,YearAL,YearOL,Math,eng,OLindex from applicant where ALindex ='" + id.ToString() + "'", conn.connection());
                MySqlDataReader read4 = command.ExecuteReader();


                while (read4.Read())
                {
                    comboBox1.SelectedItem = (read4["Title"].ToString());
                    textBox1.Text = (read4["FName"].ToString());
                    textBox2.Text = (read4["SName"].ToString());
                    textBox3.Text = (read4["NIC"].ToString());
                    comboAcademicYr.SelectedItem = (read4["year"].ToString());
                    textBox4.Text = (read4["Address"].ToString());
                    textBox6.Text = (read4["Tel"].ToString());
                    textBox7.Text = (read4["District"].ToString());

                    AlCertificate = (Convert.ToInt32(read4["ALCertificate"]));
                    if (AlCertificate == 1)
                    {
                        checkBox6.Checked = true;
                    }

                    textBox9.Text = (read4["ALIndex"].ToString());

                    OlCertificate = (Convert.ToInt32(read4["OLCertificate"]));
                    if (OlCertificate == 1)
                    {
                        checkBox5.Checked = true;
                    }

                    payslip = (Convert.ToInt32(read4["PaySlip"]));
                    if (payslip == 1)
                    {
                        checkBox1.Checked = true;
                    }
                    textBox8.Text = (read4["Zscore"].ToString());

                    chem = (read4["Chemistry"].ToString());
                    if (chem == "")
                    {
                        label72.Text = "IT";
                        radioButton3.Checked = true;
                    }
                    else if (chem != "")
                        textBox15.Text = chem;

                    IT = (read4["IT"].ToString());
                    if (IT == "")
                    {
                        label72.Text = "IT";
                        radioButton3.Checked = true;
                    }
                    else if (IT != "")
                        textBox15.Text = IT;


                    bio = (read4["Bio"].ToString());
                    if (bio == "")
                    {
                        label20.Text = "Combined Maths";
                        radioButton2.Checked = true;
                    }
                    else if (bio != "")
                        textBox13.Text = bio;

                    maths = (read4["CombinedMaths"].ToString());
                    if (maths == "")
                    {
                        label20.Text = "Biology";
                        radioButton1.Checked = true;
                    }
                    else if (maths != "")
                        textBox13.Text = maths;

                    textBox14.Text = (read4["Physics"].ToString());
                    textBox49.Text = (read4["YearAL"].ToString());
                    textBox19.Text = (read4["YearOL"].ToString());
                    textBox21.Text = (read4["Math"].ToString());
                    textBox20.Text = (read4["eng"].ToString());
                    textBox16.Text = (read4["OLindex"].ToString());
                    textBox17.Text = (read4["GenEng"].ToString());
                }
                conn.closeConnection();
                read4.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private void searchAlNo()
        {
            if (conn.connection().State == ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                listBox1.Items.Add("");
                int count = 0;
                conn.connection();
                MySqlCommand command = new MySqlCommand("select ALIndex from applicant where ALIndex like @0", conn.connection());
                command.Parameters.Add(new MySqlParameter("@0", "%" + textBox5.Text + "%"));
                MySqlDataReader read3 = command.ExecuteReader();

                while (read3.Read())
                {
                    listBox1.Items.Add(read3["ALindex"].ToString());
                    count++;

                }
                listBox1.Height = (count+1) * 16;

                conn.closeConnection();
                read3.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearApplicant()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox49.Text = "";
            textBox8.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox17.Text = "";
            textBox16.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
            textBox26.Text = "";
            textBox27.Text = "";
            textBox28.Text = "";
            textBox29.Text = "";
            textBox31.Text = "";

            checkBox1.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;



        }

        private void searchApplicant()
        {
            listBox1.Visible = true;
            listBox1.Items.Clear();
            if (textBox5.Text != "")
                searchAlNo();

            else
                listBox1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void yearAdjustment()
        {
            var time = DateTime.Now;
            string time1 = time.ToString(" yyyy/MM/dd , hh:mm ");
            //label21.Text = time1;
            string formattedTime = time.ToString("yyyy");
            int yearBefore = int.Parse(formattedTime) - 1;
            //label4.Text = yearBefore + "/" + formattedTime; 
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                panel3.Visible = true;
                panel4.Visible = false;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void Ctrl_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }


        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox8.Visible = true;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 1)
                pictureBox8.Visible = false;
        }


        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 2)
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
            if (trigger != 3)
                pictureBox11.Visible = false;
        }

        private void pictureBox13_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 6) pictureBox13.Visible = false;
        }





        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

            trigger = 1;
            hidePanels();
            pnl_AppDetail.Visible = true;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox19.Visible = false;
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
            Login.setLogin(textBox30.Text, maskedTextBox1.Text);



        }


        private void pictureBox10_Click(object sender, EventArgs e)
        {
            //  tabControl1.SelectedTab = tabPage2;
            trigger = 4;
            hidePanels();
            pnlThresold.Visible = true;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            //pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox19.Visible = false;
        }

        private void label43_Click(object sender, EventArgs e)
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
            //  tabControl1.SelectedTab = tabPage6;

            trigger = 6;
            hidePanels();
            pnladduser.Visible = true;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            //pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox19.Visible = false;
            pnlAdduserPrivilage.Visible = true;
            updateUserTable();
        }





        private void pictureBox9_Click(object sender, EventArgs e)
        {
            hidePanels();
            trigger = 2;
            pnlAppList.Visible = true;

            pictureBox8.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox19.Visible = false;

            updateApplicantListTables();
        }

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {
            hidePanels();
            trigger = 3;
            pnlResults.Visible = true;

            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            //pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox19.Visible = false;

            updateResultsTable();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            // tabControl1.SelectedTab = tabPage5;
            trigger = 5;
            hidePanels();
            pnlAnalyse.Visible = true;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox19.Visible = false;
            //pictureBox14.Visible = false;
        }

        private void pictureBox12_Click_1(object sender, EventArgs e)
        {
            // tabControl1.SelectedTab = tabPage7;

            trigger = 7;
            hidePanels();
            pnlLogin.Visible = true;
            pnlButonsAdmin.Visible = false;
            pictureBox12.Visible = false;
            pnlAcademicYear.Visible = false;

            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            //pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox19.Visible = false;

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
            if (radioButton1.Checked == true)
            {
                bio = textBox13.Text;
                maths = "";
            }

            else if (radioButton2.Checked == true)
            {
                maths = textBox13.Text;
                bio = "";
            }

            if (radioButton3.Checked == true)
            {
                IT = textBox15.Text;
                chem = "";
            }

            else if (radioButton3.Checked == false)
            {
                chem = textBox15.Text;
                IT = "";
            }

            try
            {
                editApplicant.addNewApplicant(
                comboBox1.SelectedItem.ToString(),
                textBox1.Text.ToString(),
                textBox2.Text.ToString(),
                textBox3.Text.ToString(),
                comboAcademicYr.SelectedItem.ToString(),
                textBox4.Text.ToString(),
                textBox6.Text.ToString(),
                textBox7.Text.ToString(),
                AlCertificate,
                Convert.ToInt32(textBox9.Text),
                OlCertificate,
                payslip,
                Convert.ToDouble(textBox8.Text),
                IT,
                textBox17.Text.ToString(),
                bio,
                textBox14.Text.ToString(),
                chem,
                maths,
                textBox49.Text.ToString(),
                textBox19.Text.ToString(),
                textBox21.Text.ToString(),
                textBox20.Text.ToString(),
                textBox16.Text.ToString());

                editApplicant.addPref(
                    textBox22.Text.ToString(),
                    textBox23.Text.ToString(),
                    textBox24.Text.ToString(),
                    textBox25.Text.ToString(),
                    textBox26.Text.ToString(),
                    textBox27.Text.ToString(),
                    textBox28.Text.ToString(),
                    textBox29.Text.ToString(),
                    textBox31.Text.ToString(),
                    Convert.ToInt32(textBox9.Text.ToString()),
                    Convert.ToInt32(comboBox2.SelectedItem));

                if (radioButton2.Checked == true)
                {
                    physicalChecked = 1;
                }
                else
                    physicalChecked = 0;

                 
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
            checkQualification.checkApplicant(physicalChecked, textBox13.Text.ToString(), textBox14.Text.ToString(), textBox15.Text.ToString(), textBox9.Text.ToString(), comboAcademicYr.SelectedItem.ToString());

        }

        private void btnsbmit_Click(object sender, EventArgs e)
        {
            pnlWrn.Visible = true;

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

            if (label50.Visible == true || label51.Visible == true || label52.Visible == true || label53.Visible == true || label54.Visible == true)
            {
                pnlWrn.Visible = true;
            }

            else
            {
                editUser.addNewUser(textBox32.Text.ToString(), textBox33.Text.ToString(), textBox34.Text.ToString(), comboPrivilage.SelectedItem.ToString(), maskedTextBox2.Text.ToString());
                this.Refresh();
            }


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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trigger == 4)
            {
                getThreshold();
            }
        }

        private void pictureBox26_MouseHover(object sender, EventArgs e)
        {
            pictureBox19.Visible = true;

        }

        private void pictureBox19_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 8) pictureBox19.Visible = false;
        }

        private void pictureBox19_Click_1(object sender, EventArgs e)
        {
            trigger = 8;
            hidePanels();
            pnlExamHalls.Visible = true;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;

            updateExamHallTable();


        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox62_MouseHover(object sender, EventArgs e)
        {
            pictureBox59.Visible = true;
        }

        private void pictureBox61_MouseHover(object sender, EventArgs e)
        {
            pictureBox60.Visible = true;
        }

        private void pictureBox58_MouseHover(object sender, EventArgs e)
        {
            pictureBox57.Visible = true;
        }

        private void pictureBox53_MouseHover(object sender, EventArgs e)
        {
            pictureBox51.Visible = true;
        }

        private void pictureBox54_MouseHover(object sender, EventArgs e)
        {
            //   pictureBox48.Visible = true;
        }

        private void pictureBox59_Click(object sender, EventArgs e)
        {
            trigger = 1;
            hidePanels();
            pnl_AppDetail.Visible = true;
            pictureBox47.Visible = false;
            //   pictureBox48.Visible = false;
            pictureBox51.Visible = false;
            pictureBox60.Visible = false;
            pictureBox57.Visible = false;
            //pictureBox59.Visible = false;
        }

        private void pictureBox60_Click(object sender, EventArgs e)
        {
            trigger = 2;
            hidePanels();
            pnlAppList.Visible = true;
            pictureBox47.Visible = false;
            //  pictureBox48.Visible = false;
            pictureBox51.Visible = false;
            //pictureBox60.Visible = false;
            pictureBox57.Visible = false;
            pictureBox59.Visible = false;
        }

        private void pictureBox57_Click(object sender, EventArgs e)
        {
            trigger = 8;
            hidePanels();
            pnlExamHalls.Visible = true;
            pictureBox47.Visible = false;
            //    pictureBox48.Visible = false;
            pictureBox51.Visible = false;
            pictureBox60.Visible = false;
            //pictureBox57.Visible = false;
            pictureBox59.Visible = false;
            updateExamHallTable();
        }

        private void pictureBox51_Click(object sender, EventArgs e)
        {
            trigger = 3;
            hidePanels();
            pnlResults.Visible = true;
            pictureBox47.Visible = false;
            //  pictureBox48.Visible = false;
            //pictureBox51.Visible = false;
            pictureBox60.Visible = false;
            pictureBox57.Visible = false;
            pictureBox59.Visible = false;
        }

        private void pictureBox48_Click(object sender, EventArgs e)
        {
            trigger = 6;
            hidePanels();
            pnladduser.Visible = true;
            pictureBox47.Visible = false;
            //pictureBox48.Visible = false;
            pictureBox51.Visible = false;
            pictureBox60.Visible = false;
            pictureBox57.Visible = false;
            pictureBox59.Visible = false;
            comboPrivilage.SelectedItem = "Local";
            pnlAdduserPrivilage.Visible = false;



        }

        private void pictureBox47_Click(object sender, EventArgs e)
        {
            trigger = 7;
            hidePanels();
            pnlLogin.Visible = true;
            pnlButtonsLocal.Visible = false;
            pnlAcademicYear.Visible = false;

            pictureBox47.Visible = false;
            //  pictureBox48.Visible = false;
            pictureBox51.Visible = false;
            pictureBox60.Visible = false;
            pictureBox57.Visible = false;
            pictureBox59.Visible = false;
        }

        private void pictureBox56_MouseHover(object sender, EventArgs e)
        {
            pictureBox47.Visible = true;
        }

        private void pictureBox59_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 1)
                pictureBox59.Visible = false;
        }

        private void pictureBox60_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 2)
                pictureBox60.Visible = false;
        }

        private void pictureBox57_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 8)
                pictureBox57.Visible = false;
        }

        private void pictureBox51_MouseLeave(object sender, EventArgs e)
        {
            if (trigger != 3)
                pictureBox51.Visible = false;
        }

        private void pictureBox48_MouseLeave(object sender, EventArgs e)
        {
            //   if (trigger != 6)
            //   pictureBox48.Visible = false;
        }

        private void pictureBox47_MouseLeave(object sender, EventArgs e)
        {
            pictureBox47.Visible = false;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pnlAnalyse_Paint(object sender, PaintEventArgs e)
        {



        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox35.Text == "")
                    label60.Visible = true;
                else
                    label60.Visible = false;

                if (textBox41.Text == "")
                    label64.Visible = true;
                else
                    label64.Visible = false;

                if (textBox42.Text == "")
                    label58.Visible = true;
                else
                    label58.Visible = false;



                if (label60.Visible == true || label64.Visible == true || label58.Visible == true)
                {
                    pnlWrn.Visible = true;
                }

                else
                {
                    editHall.addNewHall(textBox35.Text.ToString(), textBox41.Text.ToString(), Convert.ToInt32(textBox42.Text),Convert.ToInt32(textBox40.Text));
                    updateExamHallTable();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void pictureBox28_MouseHover(object sender, EventArgs e)
        {
            pictureBox27.Visible = true;
        }

        private void pictureBox27_MouseLeave(object sender, EventArgs e)
        {
            pictureBox27.Visible = false;
        }

        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            label60.Visible = false;
            label64.Visible = false;
            label58.Visible = false;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dataGridExamHalls_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridExamHalls.Rows[e.RowIndex];
                textBox35.Text = row.Cells["HallID"].Value.ToString();
                textBox41.Text = row.Cells["hallName"].Value.ToString();
                textBox42.Text = row.Cells["capacity"].Value.ToString();
            }
        }

        private void dataGridExamHalls_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridUser.Rows[e.RowIndex];
                textBox32.Text = row.Cells["FName"].Value.ToString();
                textBox33.Text = row.Cells["LName"].Value.ToString();
                textBox34.Text = row.Cells["EmpID"].Value.ToString();
                comboPrivilage.SelectedItem = row.Cells["Privilage"].Value.ToString();
            }
        }

        private void dataGridUser_SizeChanged(object sender, EventArgs e)
        {

        }

        private void dataGridUser_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dataGridUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridUser.Rows[e.RowIndex];
                textBox32.Text = row.Cells["FName"].Value.ToString();
                textBox33.Text = row.Cells["LName"].Value.ToString();
                textBox34.Text = row.Cells["EmpID"].Value.ToString();
                comboPrivilage.SelectedItem = row.Cells["Privilage"].Value.ToString();
            }
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                editTreshold.addNumaricTreshold(Convert.ToDouble(textBox39.Text), Convert.ToDouble(textBox38.Text), Convert.ToDouble(textBox37.Text), Convert.ToDouble(textBox36.Text), comboAcademicYr.Text.ToString());
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                editTreshold.addPercentageTreshold(Convert.ToDouble(textBox45.Text), Convert.ToDouble(textBox46.Text), Convert.ToDouble(textBox47.Text), Convert.ToDouble(textBox48.Text), comboAcademicYr.Text.ToString());
            }
        }

        private void pictureBox18_MouseHover(object sender, EventArgs e)
        {
            pictureBox17.Visible = true;
        }

        private void pictureBox17_MouseLeave(object sender, EventArgs e)
        {
            pictureBox17.Visible = false;
        }

        private void dataGridResults_KeyUp(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Return && check == 0)
            {
                try
                {

                    cmdbl = new MySqlCommandBuilder(adap);
                    adap.Update(ds, "Results");
                    MessageBox.Show("done");
                    check = 1;

                    SendKeys.Send("^{LEFT}");
                }

                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
        }

        private void dataGridResults_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            check = 0;
        }

        private void maskedTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (textBox30.Text != "")
                {
                    Login.setLogin(textBox30.Text, maskedTextBox1.Text);
                }

                if (textBox30.Text == "")
                {
                    SendKeys.Send("+{TAB}");
                }
            }
        }

        private void textBox30_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (maskedTextBox1.Text != "")
                {
                    Login.setLogin(textBox30.Text, maskedTextBox1.Text);
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }

                if (maskedTextBox1.Text == "")
                {
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                payslip = 1;
            }
            else
            {
                payslip = 0;
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label20.Text = "Biology";
                radioButton3.Hide();
            }
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label20.Text = "Combined Maths";
                radioButton3.Show();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            searchApplicant();

            if (e.KeyCode == Keys.Return)

                fillApplicant(listBox1.Items[1].ToString());
            listBox1.Visible = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            fillApplicant(listBox1.SelectedItem.ToString());
        }
        int check2 = 0;
        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true && check2 == 0)
            {
                label72.Text = "IT";
                check2 = 1;
            }

            else if (check2 == 1)
            {
                label72.Text = "Chemistry";
                radioButton3.Checked = false;
                check2 = 0;
            }
        }

        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                OlCertificate = 1;
            }
            else
            {
                OlCertificate = 0;
            }
        }

        private void checkBox6_CheckedChanged_1(object sender, EventArgs e)
        {
            {
                if (checkBox6.Checked == true)
                {
                    AlCertificate = 1;
                }
                else
                {
                    AlCertificate = 0;
                }
            }
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            clearApplicant();
        }

        private void listBox1_MouseLeave(object sender, EventArgs e)
        {
            listBox1.Visible = false;
        }

        private void pictureBox33_MouseHover(object sender, EventArgs e)
        {
            pictureBox22.Visible = true;
        }

        private void pictureBox22_MouseLeave(object sender, EventArgs e)
        {
            pictureBox22.Visible = false;
        }

        private void pictureBox22_Click_1(object sender, EventArgs e)
        {
            searchApplicant();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                if (this.dataGridExamHalls.SelectedRows.Count > 0)
                {
                    RemoveExamHallTable();

                }
                dataGridExamHalls.Rows.RemoveAt(this.dataGridExamHalls.SelectedRows[0].Index);
            }catch(Exception ex)
            {
                MessageBox.Show("Error!; Please Select the entire Row. Not a cell. ");
            }
            }

        private void dataGridFinalized_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pnlAppList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void label74_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            clearApplicant();
            fillApplicant(listBox1.Items[listBox1.SelectedIndex].ToString());
            listBox1.Visible = false;
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.dataGridResults.Sort(this.co, ListSortDirection.Descending);
        }

        private void label75_Click(object sender, EventArgs e)
        {

        }

        private void textBox40_TextChanged(object sender, EventArgs e)
        {

        }
    }

    
}
