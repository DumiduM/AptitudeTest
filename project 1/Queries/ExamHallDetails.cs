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
    class ExamHallDetails
    {
        DbConnect conn = new DbConnect();


        public void addNewHall(String HallID, String hallName, int capacity)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO ExamHall (HallID,hallName,capacity) VALUES (@1, @2, @3)", conn.connection());

                
                insertCommand.Parameters.Add(new MySqlParameter("@1", HallID));
                insertCommand.Parameters.Add(new MySqlParameter("@2", hallName));
                insertCommand.Parameters.Add(new MySqlParameter("@3", capacity));
                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();

                MessageBox.Show("Done Succesfully");
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                   if (MessageBox.Show("Do you want to save changes?","Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        updateHall(HallID, hallName, capacity);
                }
                else
                    MessageBox.Show("Error : " + ex);
            }
        }

        public void updateHall(String HallID, String hallName, int capacity)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {

                MySqlCommand insertCommand = new MySqlCommand("update ExamHall set HallID=@1, hallName=@2,capacity=@3 where hallID = @1", conn.connection());
                
                insertCommand.Parameters.Add(new MySqlParameter("@1", HallID));
                insertCommand.Parameters.Add(new MySqlParameter("@2", hallName));
                insertCommand.Parameters.Add(new MySqlParameter("@3", capacity));
                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        


        

    }
}
