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
    class ApplicantDetails
    {
        DbConnect conn = new DbConnect();

        public void addNewApplicant(String title, String fname, String sname, String nic, String year, String address, String tel_no, String district, int alcertificate, int alindex, int olcertificate, int certifyinvoice, double zscore
            , string IT, string genEng, string bio, string physic, string chem, string cMath, string yearAL, string yearOL, string math, string eng, string olindex)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO applicant (Title,FName,SName,NIC,year,Address,Tel,District,OLCertificate,ALIndex,ALCertificate,PaySlip,Zscore, IT,GenEng,Bio,Physics,Chemistry,CombinedMaths,YearAL,YearOL,Math,eng,OLindex) VALUES (@0, @1, @2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22,@23)", conn.connection());

                insertCommand.Parameters.Add(new MySqlParameter("@0", title));
                insertCommand.Parameters.Add(new MySqlParameter("@1", fname));
                insertCommand.Parameters.Add(new MySqlParameter("@2", sname));
                insertCommand.Parameters.Add(new MySqlParameter("@3", nic));
                insertCommand.Parameters.Add(new MySqlParameter("@4", year));
                insertCommand.Parameters.Add(new MySqlParameter("@5", address));
                insertCommand.Parameters.Add(new MySqlParameter("@6", tel_no));
                insertCommand.Parameters.Add(new MySqlParameter("@7", district));
                insertCommand.Parameters.Add(new MySqlParameter("@8", alcertificate));
                insertCommand.Parameters.Add(new MySqlParameter("@9", alindex));
                insertCommand.Parameters.Add(new MySqlParameter("@10", olcertificate));
                insertCommand.Parameters.Add(new MySqlParameter("@11", certifyinvoice));
                insertCommand.Parameters.Add(new MySqlParameter("@12", zscore));
                insertCommand.Parameters.Add(new MySqlParameter("@13", IT));
                insertCommand.Parameters.Add(new MySqlParameter("@14", genEng));
                insertCommand.Parameters.Add(new MySqlParameter("@15", bio));
                insertCommand.Parameters.Add(new MySqlParameter("@16", physic));
                insertCommand.Parameters.Add(new MySqlParameter("@17", chem));
                insertCommand.Parameters.Add(new MySqlParameter("@18", cMath));
                insertCommand.Parameters.Add(new MySqlParameter("@19", yearAL));
                insertCommand.Parameters.Add(new MySqlParameter("@20", yearOL));
                insertCommand.Parameters.Add(new MySqlParameter("@21", math));
                insertCommand.Parameters.Add(new MySqlParameter("@22", eng));
                insertCommand.Parameters.Add(new MySqlParameter("@23", olindex));

                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    if (MessageBox.Show("Do you want to save changes?", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes) { }
                    updateApplicant(title, fname, sname, nic, year, address, tel_no, district, alcertificate, alindex, olcertificate, certifyinvoice, zscore, IT, genEng, bio, physic, chem, cMath, yearAL, yearOL, math, eng, olindex);

                }
                else
                    MessageBox.Show("Error : " + ex);
            }

        }

        public void updateApplicant(String title, String fname, String sname, String nic, String year, String address, String tel_no, String district, int alcertificate, int alindex, int olcertificate, int certifyinvoice, double zscore
            , string IT, string genEng, string bio, string physic, string chem, string cMath, string yearAL, string yearOL, string math, string eng, string olindex)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {

                MySqlCommand insertCommand = new MySqlCommand("update applicant set Title=@0, FName=@1, SName=@2, NIC=@3, year=@4, Address=@5, Tel=@6, District=@7, ALCertificate=@8, ALIndex=@9,OLCertificate=@10, PaySlip=@11, Zscore=@12, IT=@13, GenEng=@14, Bio=@15, Physics=@16, Chemistry=@17, CombinedMaths=@18, YearAL=@19, YearOL=@20, Math=@21, eng=@22, OLindex=@23 where ALIndex=@9", conn.connection());

                insertCommand.Parameters.Add(new MySqlParameter("@0", title));
                insertCommand.Parameters.Add(new MySqlParameter("@1", fname));
                insertCommand.Parameters.Add(new MySqlParameter("@2", sname));
                insertCommand.Parameters.Add(new MySqlParameter("@3", nic));
                insertCommand.Parameters.Add(new MySqlParameter("@4", year));
                insertCommand.Parameters.Add(new MySqlParameter("@5", address));
                insertCommand.Parameters.Add(new MySqlParameter("@6", tel_no));
                insertCommand.Parameters.Add(new MySqlParameter("@7", district));
                insertCommand.Parameters.Add(new MySqlParameter("@8", alcertificate));
                insertCommand.Parameters.Add(new MySqlParameter("@9", alindex));
                insertCommand.Parameters.Add(new MySqlParameter("@10", olcertificate));
                insertCommand.Parameters.Add(new MySqlParameter("@11", certifyinvoice));
                insertCommand.Parameters.Add(new MySqlParameter("@12", zscore));
                insertCommand.Parameters.Add(new MySqlParameter("@13", IT));
                insertCommand.Parameters.Add(new MySqlParameter("@14", genEng));
                insertCommand.Parameters.Add(new MySqlParameter("@15", bio));
                insertCommand.Parameters.Add(new MySqlParameter("@16", physic));
                insertCommand.Parameters.Add(new MySqlParameter("@17", chem));
                insertCommand.Parameters.Add(new MySqlParameter("@18", cMath));
                insertCommand.Parameters.Add(new MySqlParameter("@19", yearAL));
                insertCommand.Parameters.Add(new MySqlParameter("@20", yearOL));
                insertCommand.Parameters.Add(new MySqlParameter("@21", math));
                insertCommand.Parameters.Add(new MySqlParameter("@22", eng));
                insertCommand.Parameters.Add(new MySqlParameter("@23", olindex));

                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void addPref(string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8, string p9, int alindex, int pref)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO ugc_pref (p1,p2,p3,p4,p5,p6,p7,p8,p9,ALindex,pref) VALUES (@0, @1, @2,@3,@4,@5,@6,@7,@8,@9,@10)", conn.connection());

                insertCommand.Parameters.Add(new MySqlParameter("@0", p1));
                insertCommand.Parameters.Add(new MySqlParameter("@1", p2));
                insertCommand.Parameters.Add(new MySqlParameter("@2", p3));
                insertCommand.Parameters.Add(new MySqlParameter("@3", p4));
                insertCommand.Parameters.Add(new MySqlParameter("@4", p5));
                insertCommand.Parameters.Add(new MySqlParameter("@5", p6));
                insertCommand.Parameters.Add(new MySqlParameter("@6", p7));
                insertCommand.Parameters.Add(new MySqlParameter("@7", p8));
                insertCommand.Parameters.Add(new MySqlParameter("@8", p9));
                insertCommand.Parameters.Add(new MySqlParameter("@9", alindex));
                insertCommand.Parameters.Add(new MySqlParameter("@10", pref));

                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    if (MessageBox.Show("Do you want to save changes?", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes) { }
                    updatePref(p1, p2, p3, p4, p5, p6, p7, p8, p9, alindex, pref);

                }
                else
                    MessageBox.Show("Error : " + ex);
            }

        }

        public void updatePref(string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8, string p9, int alindex, int pref)
        {
            if (conn.connection().State == System.Data.ConnectionState.Closed)
                conn.connection().Open();
            try
            {
                MySqlCommand insertCommand = new MySqlCommand("update ugc_pref set p1=@1,p2=@2,p3=@3,p4=@4,p5=@5,p6=@6,p7=@7,p8=@8,p9=@9,ALindex=@10,pref=@11 where ALindex = @10", conn.connection());

                insertCommand.Parameters.Add(new MySqlParameter("@0", p1));
                insertCommand.Parameters.Add(new MySqlParameter("@1", p2));
                insertCommand.Parameters.Add(new MySqlParameter("@2", p3));
                insertCommand.Parameters.Add(new MySqlParameter("@3", p4));
                insertCommand.Parameters.Add(new MySqlParameter("@4", p5));
                insertCommand.Parameters.Add(new MySqlParameter("@5", p6));
                insertCommand.Parameters.Add(new MySqlParameter("@6", p7));
                insertCommand.Parameters.Add(new MySqlParameter("@7", p8));
                insertCommand.Parameters.Add(new MySqlParameter("@8", p9));
                insertCommand.Parameters.Add(new MySqlParameter("@9", alindex));
                insertCommand.Parameters.Add(new MySqlParameter("@10", pref));

                insertCommand.ExecuteNonQuery();
                insertCommand.Dispose();
                conn.closeConnection();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error : " + ex);
            }
        }
    }
}
