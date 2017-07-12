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
        private int examNo;
        private int NumberofHalls;
        int HallNo=0;
        int applicants;

        public void updateHall(int i,string year,string ALIndex)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                MySqlCommand command = new MySqlCommand("select applicants from examhall where hallNo like @1", conn.connection());
                command.Parameters.Add(new MySqlParameter("@1", i));
                MySqlDataReader read2 = command.ExecuteReader();
                try
                {
                    while (read2.Read())
                    {
                        applicants = Convert.ToInt32(read2[0]);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex);
                }
                read2.Close();
                
                MySqlCommand insertCommand2 = new MySqlCommand("update examhall set applicants =@0 where HallNo = @1", conn.connection());

                insertCommand2.Parameters.Add(new MySqlParameter("@0", applicants+1));
                insertCommand2.Parameters.Add(new MySqlParameter("@1", i));
                insertCommand2.ExecuteNonQuery();
                insertCommand2.Dispose();
                conn.closeConnection();

                if (conn.connection().State == System.Data.ConnectionState.Closed)
                    conn.connection().Open();

                //inserting to DB
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO qualifiedapplicants (ALIndex,ExamIndex,year,hallno) VALUES (@0,@1,@2,@3)", conn.connection());
                insertCommand.Parameters.Add(new MySqlParameter("@0", ALIndex));
                insertCommand.Parameters.Add(new MySqlParameter("@1", examNo));
                insertCommand.Parameters.Add(new MySqlParameter("@2", year));
                insertCommand.Parameters.Add(new MySqlParameter("@3", HallNo));

                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        public void checkApplicant(int physicalchecked, string bioMaths, string physics, string chemIT, string ALIndex, string year)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();

            try
            {
                if ((physicalchecked == 1 && (bioMaths == "A" || bioMaths == "B" || bioMaths == "C") || physics == "A" || physics == "B" || physics == "C" || physics == "S") && (chemIT == "A" || chemIT == "B" || chemIT == "C" || chemIT == "S") ||
                    (physicalchecked == 1 && (bioMaths == "A" || bioMaths == "B" || bioMaths == "C" || bioMaths == "S") || physics == "A" || physics == "B" || physics == "C") && (chemIT == "A" || chemIT == "B" || chemIT == "C" || chemIT == "S") ||
                    (physicalchecked == 0 && (bioMaths == "A" || bioMaths == "B" || bioMaths == "C" || bioMaths == "S") || physics == "A" || physics == "B" || physics == "C") && (chemIT == "A" || chemIT == "B" || chemIT == "C" || chemIT == "S"))
                {

                    //getting the exam no
                    conn.connection();
                    MySqlCommand command = new MySqlCommand("Select ExamIndex from qualifiedapplicants where year = '" + year + "' order by ExamIndex desc limit 1", conn.connection());
                    MySqlDataReader read = command.ExecuteReader();
                    try
                    {
                        while (read.Read())
                        {
                            examNo = (Convert.ToInt32(read["ExamIndex"]));
                        }
                        examNo++;
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error 2 : " + ex);
                    }
                    read.Close();





                    //getting hall count

                    MySqlCommand command4 = new MySqlCommand("Select hallNo,capacity,applicants from examhall order by hallNo asc", conn.connection());
                     var dt = new DataTable();
                     dt.Load(command4.ExecuteReader());
                     var halls = dt.AsEnumerable().ToArray();
                     NumberofHalls = halls.Length;


                    HallNo = 0;                 
                    for (int i = 0; i < NumberofHalls; i++)
                    {
                        if (Convert.ToInt32(halls[i]["capacity"]) > Convert.ToInt32(halls[i]["applicants"]))
                        {
                            MessageBox.Show("loop in to hall number checked"+i);

                            HallNo = i+1;
                            updateHall(HallNo,year,ALIndex);
                            MessageBox.Show(HallNo.ToString());
                            break;

                        }
                        if (HallNo==0 && i == NumberofHalls - 1)
                        {
                            MessageBox.Show("No enough space in Hall to allocate");
                        }
                        
                    }
                    MessageBox.Show("APPLICANTS,NUNBER OF HALLS,hallNo" + applicants + " " + NumberofHalls +" "+HallNo);

                }
            }

            catch(Exception ex)
            {
                MessageBox.Show("Error 1 :"+ex);
            }
        }



    }
}
