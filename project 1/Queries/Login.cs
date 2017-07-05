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

namespace project_1.Queries
{
    class Login
    {
        static MainWindow main = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

        public static void setLogin(string userName, string password)
        {
            DbConnect conn = new DbConnect();
            
            try
            {
                
                MySqlCommand readcommand = new MySqlCommand("select * from user where FName=@0 and Password=@1 and privilage = 'local';", conn.connection());
                readcommand.Parameters.Add(new MySqlParameter("@0", userName));
                readcommand.Parameters.Add(new MySqlParameter("@1", password));
                
                MySqlDataReader myReader;

                if (conn.connection().State == System.Data.ConnectionState.Closed)
                    conn.connection().Open();
                myReader = readcommand.ExecuteReader();

                int count = 0;

                while (myReader.Read())
                {
                    count = count + 1;
                }

                readcommand = new MySqlCommand("select * from user where FName=@0 and Password=@1 and Privilage = 'Administrator';", conn.connection());
                readcommand.Parameters.Add(new MySqlParameter("@0", userName));
                readcommand.Parameters.Add(new MySqlParameter("@1", password));


                myReader.Close();

                MySqlDataReader myReader2;

                if (conn.connection().State == System.Data.ConnectionState.Closed)
                    conn.connection().Open();
                myReader2 = readcommand.ExecuteReader();

                while (myReader2.Read())
                {
                    count = count + 10;
                }

                if (count != 0)
                {
                    

                    main.pnlAcademicYear.Visible = true;
                    main.pnlLogin.Visible = false;

                    if (count == 10)
                        main.pnlButonsAdmin.Visible = true;
                    else if (count == 1)
                        main.pnlButtonsLocal.Visible = true;

                }
                else
                    MessageBox.Show("Usernme and password is not correct");
                
                myReader2.Close();
                conn.connection().Close();
                main.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

        }
    }
}
