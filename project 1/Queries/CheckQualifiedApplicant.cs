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
    class CheckQualifiedApplicant
    {
        DbConnect conn = new DbConnect();
        private int examNo=100000;
        private int NumberofHalls;
        private int Numberofapplicant;
        int capacity;
        int HallNo;

        public void checkApplicant(int physicalchecked,string bioMaths, string physics, string chemIT,string ALIndex,string year)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();

            try
            {
                if ((physicalchecked == 1 && (bioMaths == "A" || bioMaths == "B" || bioMaths == "C") || physics == "A" || physics == "B" || physics == "C" || physics == "S") && (chemIT =="A" || chemIT == "B" || chemIT == "C" || chemIT == "S") ||
                    (physicalchecked == 1 && (bioMaths == "A" || bioMaths == "B" || bioMaths == "C" || bioMaths == "S") || physics == "A" || physics == "B" || physics == "C") && (chemIT == "A" || chemIT == "B" || chemIT == "C" || chemIT == "S") ||
                    (physicalchecked == 0 && (bioMaths == "A" || bioMaths == "B" || bioMaths == "C" || bioMaths == "S") || physics == "A" || physics == "B" || physics == "C") && (chemIT == "A" || chemIT == "B" || chemIT == "C" || chemIT == "S"))
                {
                    MessageBox.Show("ok");
                    //getting the exam no
                    conn.connection();
                    MySqlCommand command = new MySqlCommand("Select ExamIndex from qualifiedapplicants where year like '"+year+"' order by ExamIndex desc limit 1 offset 1", conn.connection());
                    MySqlDataReader read = command.ExecuteReader();
                    try
                    {
                        while (read.Read())
                        {
                            examNo = (Convert.ToInt32(read["ExamIndex"]));
                        }
                        examNo++;
                    }

                    catch(Exception ex)
                    {
                        MessageBox.Show("Error : " + ex);
                    }
                    read.Close();


                    //getting number of hall allocated
                    /*MySqlCommand command2 = new MySqlCommand("Select count(distinct HallNo) from `examhall`", conn.connection());
                    MySqlDataReader read2 = command.ExecuteReader();
                    try
                    {
                        while (read2.Read())
                        {
                            NumberofHalls = (Convert.ToInt32(read2["count(distinct HallNo)"]));
                        } 
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex);
                    }
                    read2.Close();*/

                    //getting number of applicant
                    MySqlCommand command3 = new MySqlCommand("Select count(ExamIndex) from qualifiedapplicants", conn.connection());
                    MySqlDataReader read3 = command.ExecuteReader();
                    try
                    {
                        while (read3.Read())
                        {
                            Numberofapplicant = (Convert.ToInt32(read3["count(ExamIndex)"]));
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex);
                    }
                    read3.Close();





                    //getting number of applicant
                    MySqlCommand command4 = new MySqlCommand("Select hallNo,capacity from examhall", conn.connection());
                    var dt = new DataTable();
                    dt.Load(command4.ExecuteReader());
                    var rows = dt.AsEnumerable().ToArray();
                    NumberofHalls = rows.Length;
                   // rows[0]["id"];
                    for (int i = 0; i < NumberofHalls; i++)
                    {
                        capacity = Convert.ToInt32(rows[i]["capacity"]);

                        if (Numberofapplicant <= capacity)
                        {
                            HallNo = Convert.ToInt32(rows[i]["hallNo"]);
                            break;
                        }
                    }
                    
                    //if (number)

                    //insrting to DB
                    MySqlCommand insertCommand = new MySqlCommand("INSERT INTO qualifiedapplicants (ALIndex,ExamIndex,year,hallno) VALUES (@0,@01,@2)", conn.connection());
                    insertCommand.Parameters.Add(new MySqlParameter("@0", ALIndex));
                    insertCommand.Parameters.Add(new MySqlParameter("@1", examNo));
                    insertCommand.Parameters.Add(new MySqlParameter("@2", year));
                    insertCommand.Parameters.Add(new MySqlParameter("@3", HallNo));
                    insertCommand.ExecuteNonQuery();
                    insertCommand.Dispose();
                    conn.closeConnection();
                }
            }

            catch(Exception ex)
            {

            }
        }



    }
}
