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
    class TresholdDetails
    {
        DbConnect conn = new DbConnect();


        public void addNumaricTreshold(double a,double b,double c,double total, string year)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO threshold (ANumeric,BNumeric,CNumeric,TotalNumeric,year) VALUES (@1, @2, @3,@4,@5)", conn.connection());


                insertCommand.Parameters.Add(new MySqlParameter("@1", a));
                insertCommand.Parameters.Add(new MySqlParameter("@2", b));
                insertCommand.Parameters.Add(new MySqlParameter("@3", c));
                insertCommand.Parameters.Add(new MySqlParameter("@4", total));
                insertCommand.Parameters.Add(new MySqlParameter("@5", year));
                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();

                
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    if (MessageBox.Show("Do you want to save changes?", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        updateNumericTreshold(a, b, c, total, year);
                }
                else
                    MessageBox.Show("Error : " + ex);
            }
        }

        public void updateNumericTreshold(double a, double b, double c, double total, string year)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {

                MySqlCommand insertCommand = new MySqlCommand("update thresholdmarks set ANumeric=@1, BNumeric=@2,CNumeric=@3,TotalNumeric=@4 where year = @5", conn.connection());

                insertCommand.Parameters.Add(new MySqlParameter("@1", a));
                insertCommand.Parameters.Add(new MySqlParameter("@2", b));
                insertCommand.Parameters.Add(new MySqlParameter("@3", c));
                insertCommand.Parameters.Add(new MySqlParameter("@4", total));
                insertCommand.Parameters.Add(new MySqlParameter("@5", year));
                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        public void addPercentageTreshold(double a, double b, double c, double total, string year)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO thresholdmarks (APercentage,BPercentage,CPercentage,TotalPercentage,year) VALUES (@1, @2, @3,@4,@5)", conn.connection());


                insertCommand.Parameters.Add(new MySqlParameter("@1", a));
                insertCommand.Parameters.Add(new MySqlParameter("@2", b));
                insertCommand.Parameters.Add(new MySqlParameter("@3", c));
                insertCommand.Parameters.Add(new MySqlParameter("@4", total));
                insertCommand.Parameters.Add(new MySqlParameter("@5", year));
                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();

                
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    if (MessageBox.Show("Do you want to save changes?", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        updatePercentageTreshold(a, b, c, total, year);
                }
                else
                    MessageBox.Show("Error : " + ex);
            }
        }

        public void updatePercentageTreshold(double a, double b, double c, double total, string year)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {

                MySqlCommand insertCommand = new MySqlCommand("update thresholdmarks set APercentage=@1, BPercentage=@2,CPercentage=@3,TotalPercentage=@4 where year = @5", conn.connection());

                insertCommand.Parameters.Add(new MySqlParameter("@1", a));
                insertCommand.Parameters.Add(new MySqlParameter("@2", b));
                insertCommand.Parameters.Add(new MySqlParameter("@3", c));
                insertCommand.Parameters.Add(new MySqlParameter("@4", total));
                insertCommand.Parameters.Add(new MySqlParameter("@5", year));
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
