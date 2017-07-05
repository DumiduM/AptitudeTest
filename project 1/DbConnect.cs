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
    public class DbConnect
    {
        MySqlConnection mycon = new MySqlConnection();

        public DbConnect()
        {
            mycon.ConnectionString = @"server=localhost; user id=root; password=''; database=aptitudetest";
            try
            {
                mycon.Open();
                Console.WriteLine("ServerVersion: {0}", mycon.ServerVersion);
                Console.WriteLine("State: {0}", mycon.State);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public MySqlConnection connection()
        {
            return mycon;
        }

        public void closeConnection()
        {
            if (mycon.State == System.Data.ConnectionState.Open)
                mycon.Close();
        }
    }



}


