using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace project_1
{
    class UserDetails
    {
        DbConnect conn = new DbConnect();


        public void addNewUser(String fName, String lName, String EmpID, String privilage,String password)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO user (FName,LName,EmpID,Privilage,Password) VALUES (@0, @1, @2, @3, @4)", conn.connection());

                insertCommand.Parameters.Add(new MySqlParameter("@0", fName));
                insertCommand.Parameters.Add(new MySqlParameter("@1", lName));
                insertCommand.Parameters.Add(new MySqlParameter("@2", EmpID));
                insertCommand.Parameters.Add(new MySqlParameter("@3", privilage));
                insertCommand.Parameters.Add(new MySqlParameter("@4", password));
                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();

                MessageBox.Show("Done Succesfully");
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("Do you want to save changes?");
                    updateUser(fName, lName, EmpID, privilage, password);
                }
                else
                    MessageBox.Show("Error : " + ex);
            }
        }

        public void updateUser(String fName, String lName, String EmpID, String privilage, String password)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {

                MySqlCommand insertCommand = new MySqlCommand("update user set FName=@0, LName=@1, EmpID=@2,Privilage=@3,Password=@4 where EmpID = @=2", conn.connection());


                insertCommand.Parameters.Add(new MySqlParameter("@0", fName));
                insertCommand.Parameters.Add(new MySqlParameter("@1", lName));
                insertCommand.Parameters.Add(new MySqlParameter("@2", EmpID));
                insertCommand.Parameters.Add(new MySqlParameter("@3", privilage));
                insertCommand.Parameters.Add(new MySqlParameter("@4", password));
                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();

                MessageBox.Show("Done Succesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        
    }
}
