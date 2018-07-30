using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace FinalProject
{
    class TimeTable
    {
        public static string fillDataGrid = "SELECT EmpRegistration.EmpName,EmpRegistration.EmpCNIC,EmpRegistration.AccountStatus,TimeTable.Academic,TimeTable.Semester FROM EmpRegistration inner JOIN TimeTable ON EmpRegistration.EmpCNIC=TimeTable.EmpCNIC INTERSECT SELECT EmpRegistration.EmpName,EmpRegistration.EmpCNIC,EmpRegistration.AccountStatus,TimeTable.Academic,TimeTable.Semester FROM EmpRegistration inner JOIN TimeTable ON EmpRegistration.EmpCNIC=TimeTable.EmpCNIC";
        public static TimeSheet time = new TimeSheet();
        public static void Reset()
        {
            string text1, text2;
            text1=time.textBoxCnic.Text = "";
            text2=time.textBoxName.Text = "";

        }
        
        #region
        public static void Tmonday()
        {
            DbConnection.con.Close();
            if (DbConnection.con.State != ConnectionState.Open)
            {
                DbConnection.con.Open();
            }
            //////////////////////////////////////////////////////////////////////////////
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DbConnection.con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TimeSheet";
            cmd.Parameters.AddWithValue("@name",time.textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", time.textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", time.year.Text);
            cmd.Parameters.AddWithValue("@semester", time.semester.Text);
            cmd.Parameters.AddWithValue("@day", time.Mon.Text);
            cmd.Parameters.AddWithValue("@timeIn", time.MonIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", time.MonOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }

        public static void Ttuesday()
        {
            DbConnection.con.Close();
            if (DbConnection.con.State != ConnectionState.Open)
            {
                DbConnection.con.Open();
            }
            //////////////////////////////////////////////////////////////////////////////
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DbConnection.con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TimeSheet";
            cmd.Parameters.AddWithValue("@name", time.textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", time.textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", time.year.Text);
            cmd.Parameters.AddWithValue("@semester", time.semester.Text);
            cmd.Parameters.AddWithValue("@day", time.Tues.Text);
            cmd.Parameters.AddWithValue("@timeIn", time.TuesIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", time.TuesOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public static void Twednesday()
        {
            DbConnection.con.Close();
            if (DbConnection.con.State != ConnectionState.Open)
            {
                DbConnection.con.Open();
            }
            //////////////////////////////////////////////////////////////////////////////
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DbConnection.con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TimeSheet";
            cmd.Parameters.AddWithValue("@name", time.textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", time.textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", time.year.Text);
            cmd.Parameters.AddWithValue("@semester", time.semester.Text);
            cmd.Parameters.AddWithValue("@day", time.Wed.Text);
            cmd.Parameters.AddWithValue("@timeIn", time.WedIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", time.WedOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public static void Tthursday()
        {
            DbConnection.con.Close();
            if (DbConnection.con.State != ConnectionState.Open)
            {
                DbConnection.con.Open();
            }
            //////////////////////////////////////////////////////////////////////////////
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DbConnection.con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TimeSheet";
            cmd.Parameters.AddWithValue("@name", time.textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", time.textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", time.year.Text);
            cmd.Parameters.AddWithValue("@semester", time.semester.Text);
            cmd.Parameters.AddWithValue("@day", time.Thu.Text);
            cmd.Parameters.AddWithValue("@timeIn", time.ThuIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", time.ThuOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public static void Tfriday()
        {
            DbConnection.con.Close();
            if (DbConnection.con.State != ConnectionState.Open)
            {
                DbConnection.con.Open();
            }
            //////////////////////////////////////////////////////////////////////////////
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DbConnection.con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TimeSheet";
            cmd.Parameters.AddWithValue("@name", time.textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", time.textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", time.year.Text);
            cmd.Parameters.AddWithValue("@semester", time.semester.Text);
            cmd.Parameters.AddWithValue("@day", time.Fri.Text);
            cmd.Parameters.AddWithValue("@timeIn", time.FriIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", time.FriOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public static void Tsaturdat()
        {
            DbConnection.con.Close();
            if (DbConnection.con.State != ConnectionState.Open)
            {
                DbConnection.con.Open();
            }
            //////////////////////////////////////////////////////////////////////////////
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DbConnection.con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TimeSheet";
            cmd.Parameters.AddWithValue("@name", time.textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", time.textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", time.year.Text);
            cmd.Parameters.AddWithValue("@semester", time.semester.Text);
            cmd.Parameters.AddWithValue("@day", time.Sat.Text);
            cmd.Parameters.AddWithValue("@timeIn", time.SatIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", time.SatOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        #endregion

        #region
        //public static void FillTimeTable()
        //{
           
        //    SqlCommand cmd = new SqlCommand("select * from TimeTable where EmpCNIC='"+textBoxControl.Text+"';", DbConnection.con);
        //    SqlDataReader read;
        //    try
        //    {
        //        DbConnection.con.Open();
        //        read = cmd.ExecuteReader();
        //        if (read.Read())
        //        {
        //            MessageBox.Show("ok");
        //            time.WedIn.Text= read["TimeIn"].ToString();
        //            time.WedOut.Text = read["TimeOut"].ToString();
        //        }
        //        DbConnection.con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        #endregion
    }
}
