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

namespace project_1
{
    public partial class test : Form
    {
        DbConnect conn = new DbConnect();
        public test()
        {
            InitializeComponent();


        }

        private void test_Load(object sender, EventArgs e)
        {
            //conn.connection();
            //MySqlCommand command = new MySqlCommand();
            //command.Connection = conn.connection();
            //command.CommandText = "select * from examhall";

            //DataTable data = new DataTable();

            //MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            //adapter.Fill(data);

            //MainWindow mainwin = new MainWindow();
            //mainwin.dataGridExamHalls.DataSource = data;
            //MessageBox.Show("sdfdff");
            //mainwin.Show();
        }
    }
}
