using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using DAL;
using Business_Entities;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace FinalProject
{
    public partial class Attendance1 : Form
    {
        /// <summary>
        /// ////////////////////////////
        /// </summary>
        Bus_Person busper = new Bus_Person();
        Dal_Person dalper = new Dal_Person();
        Bus_Attendance busatn = new Bus_Attendance();
        Dal_Attendance dalatn = new Dal_Attendance();
        List<string> cnics = new List<string>();
        int a;

        public Attendance1()
        {            
            InitializeComponent();
        }
        
        #region Time_Margin
        private void Margin()
        {
            DbConnection.checkConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Margin FROM TimeMargin", DbConnection.con);
                SqlDataReader reader;
                DbConnection.con.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    labelMargin.Text = reader["Margin"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region time differance late In

        private void TimeSpane()
        {
            DateTime Mar = Convert.ToDateTime(labelMargin.Text);//Set time margin
            DateTime d1 = Convert.ToDateTime(time.Text);        //Current time
            DateTime d2 = Convert.ToDateTime(StartTime.Text);   //Start time
            TimeSpan d21 = d1.Subtract(Mar);                    //Late relextation
            string time1 = d21.ToString();
            DateTime time2 = Convert.ToDateTime(time1);
            if (time2 > d2)
            {
                TimeSpan ts = d1.Subtract(d2);
                string differenceString = ts.ToString();
                lateIn.Text = differenceString.ToString();
            }
        }
        #endregion

        #region Late In
        private void RetunToLate()
        {
            if (this.lateIn.Text != "--------")
            {
                AttendanceLate();
            }
            else
            {
                AttendancIn();
            }
        }
        #endregion

        #region time diffenance Early out
        private void EarlyOut()
        {
            DateTime d1 = Convert.ToDateTime(time.Text);
            DateTime d2 = Convert.ToDateTime(EndTime.Text);
            if (d1 < d2)
            {
                TimeSpan ts = d2.Subtract(d1);
                string differance = ts.ToString();
                earlyOut.Text = differance.ToString();
            }
        }
        #endregion

        #region Early Out
        private void ReturnEarlyOut()
        {
            if (this.earlyOut.Text != "--------")
            {
                AttendanceEarly();
            }
            else
            {
                AttendanceOut();
            }
        }
        #endregion

        #region byte to image
        void image()
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                string sql = "SELECT Picture FROM EmpRegistration WHERE EmpCNIC='" + textBox1.Text + "';";
                if (DbConnection.con.State != ConnectionState.Open)
                    DbConnection.con.Open();
                cmd = new SqlCommand(sql, DbConnection.con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    byte[] img = (byte[])(reader[0]);
                    if (img == null)
                        pictureBoxPicture.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBoxPicture.Image = Image.FromStream(ms);
                    }
                }

                DbConnection.con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region Check Authentication
        private void CheckAuthentication()
        {
            try
            {
                image();
                SqlCommand cmd = new SqlCommand("select * from EmpRegistration where EmpCNIC='" + this.textBox1.Text + "' AND AccountStatus='" + labelAccountStatus.Text + "';", DbConnection.con);
                DbConnection.con.Open();
                SqlDataReader read;
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    labelname.Text = read["EmpName"].ToString();
                    labelcnic.Text = read["EmpCNIC"].ToString();
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid user OR maybe Your Account is Deactive", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region check Attendance In
        private void CheckIN()
        {
            try
            {
                ///////////////////Check Attendance In////////////////////////////////////////////////////////////////////
                SqlCommand cmd2 = new SqlCommand("Select * From Attendance Where EmpCNIC='" + textBox1.Text + "' AND Date='" + date.Text + "';", DbConnection.con);
                DbConnection.con.Open();
                SqlDataReader reader;
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    string status = reader["Status"].ToString();
                    if (status == "Leave")
                    {
                        MessageBox.Show("Processing Fail\n You maybe leaving", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DbConnection.checkConnection();
                    }
                    else
                    {
                        ///////////////////////Check Attendance Out//////////////////////////////////////////////////////////////   
                        DbConnection.con.Close();
                        SqlCommand cmdOut = new SqlCommand("Select * From Attendance Where EmpCNIC='" + textBox1.Text + "' AND Date='" + date.Text + "';", DbConnection.con);
                        SqlDataReader readerOut;
                        DbConnection.con.Open();
                        readerOut = cmdOut.ExecuteReader();
                        if (readerOut.Read())
                        {
                            string timeOut = readerOut["TimeOut"].ToString();
                            string timeInnn = readerOut["TimeIn"].ToString();
                            labeltimeIn.Text = timeInnn;
                            labeltimeOut.Text = timeOut;
                            if (timeOut != "")
                            {
                                fillTimeINOut();
                                MessageBox.Show("Your Attendance is complete", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Reset();
                            }
                            else
                            {
                                fillTimeINOut();
                                EarlyOut();
                                ReturnEarlyOut();
                                //AttendanceOut();
                                timer2.Start();
                            }
                        }
                        DbConnection.con.Close();
                    }
                }
                else
                {
                    ////////////////////////Check Time Table/////////////////////////////////////////////////////////
                    DbConnection.con.Close();
                    SqlCommand cmd = new SqlCommand("SELECT * From TimeTable WHERE EmpCNIC='" + textBox1.Text + "' AND Days='" + day.Text + "';", DbConnection.con);
                    DbConnection.con.Open();
                    SqlDataReader readerchk;
                    readerchk = cmd.ExecuteReader();
                    if (readerchk.Read())
                    {
                        string timeInn = readerchk["TimeIn"].ToString();
                        if (timeInn == "")
                        {                           
                            MessageBox.Show("Sorry your Time Table is't manage", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);                            
                            Reset();
                        }
                        else
                        {
                            fillTimeINOut();
                            TimeSpane();
                            RetunToLate();
                            //AttendancIn();
                            timer2.Start();
                            DbConnection.con.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry your Time Table is't manage", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Reset();
                    }
                    DbConnection.con.Close();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DbConnection.checkConnection();
            }
        }
        #endregion

        #region Attendance In
        private void AttendancIn()
        {
            WELBAY.Text = "WELCOME";
            labeltimeIn.Text = time.Text;
            DbConnection.con.Close();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = DbConnection.con;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "AttendanceIn";
            cmd1.Parameters.AddWithValue("@name", labelname.Text);
            cmd1.Parameters.AddWithValue("@cnic", labelcnic.Text);
            cmd1.Parameters.AddWithValue("@date", date.Text);
            cmd1.Parameters.AddWithValue("@days", day.Text);
            cmd1.Parameters.AddWithValue("@timeIn", labeltimeIn.Text);
            cmd1.Parameters.AddWithValue("@month", label7.Text);
            cmd1.Parameters.AddWithValue("@status", Status.Text);
            DbConnection.con.Open();
            cmd1.ExecuteNonQuery();
            DbConnection.con.Close();
            //MessageBox.Show("Attendance in Saved");
        }
        #endregion

        #region Attendance Late
        private void AttendanceLate()
        {
            WELBAY.Text = "WELCOME";
            labeltimeIn.Text = time.Text;
            DbConnection.con.Close();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = DbConnection.con;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "AttendanceLate";
            cmd1.Parameters.AddWithValue("@name", labelname.Text);
            cmd1.Parameters.AddWithValue("@cnic", labelcnic.Text);
            cmd1.Parameters.AddWithValue("@date", date.Text);
            cmd1.Parameters.AddWithValue("@days", day.Text);
            cmd1.Parameters.AddWithValue("@timeIn", labeltimeIn.Text);
            cmd1.Parameters.AddWithValue("@late", lateIn.Text);
            cmd1.Parameters.AddWithValue("@month", label7.Text);
            cmd1.Parameters.AddWithValue("@status", Status.Text);
            DbConnection.con.Open();
            cmd1.ExecuteNonQuery();
            DbConnection.con.Close();
            //MessageBox.Show("Attendance in Saved");
        }
        #endregion

        #region Attendance Out
        private void AttendanceOut()
        {
            Status.Text = "Present";
            WELBAY.Text = "GOOD BYE";
            labeltimeOut.Text = time.Text;
            DbConnection.con.Close();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = DbConnection.con;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "UpdateAttendance";
            //cmd1.Parameters.AddWithValue("@name", labelname.Text);
            cmd1.Parameters.AddWithValue("@cnic", labelcnic.Text);
            cmd1.Parameters.AddWithValue("@date", date.Text);
            //cmd1.Parameters.AddWithValue("@timeIn", labeltimeIn.Text);
            cmd1.Parameters.AddWithValue("@timeOut", time.Text);
            cmd1.Parameters.AddWithValue("@status", Status.Text);
            DbConnection.con.Open();
            cmd1.ExecuteNonQuery();
            DbConnection.con.Close();
            //MessageBox.Show("Attendance complete");
        }
        #endregion

        #region Attendance Early Out
        private void AttendanceEarly()
        {
            Status.Text = "EarlyOut";
            WELBAY.Text = "GOOD BYE";
            labeltimeOut.Text = time.Text;
            DbConnection.con.Close();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = DbConnection.con;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "UpdateAttendanceEarly";
            //cmd1.Parameters.AddWithValue("@name", labelname.Text);
            cmd1.Parameters.AddWithValue("@cnic", labelcnic.Text);
            cmd1.Parameters.AddWithValue("@date", date.Text);
            cmd1.Parameters.AddWithValue("@timeOut", time.Text);
            cmd1.Parameters.AddWithValue("@earlyOut", earlyOut.Text);
            cmd1.Parameters.AddWithValue("@status", Status.Text);
            DbConnection.con.Open();
            cmd1.ExecuteNonQuery();
            DbConnection.con.Close();
            //MessageBox.Show("Attendance complete");
        }
        #endregion

        #region Reset time in out
        private void Reset()
        {
           
            labelname.Text = "";
            labelcnic.Text = "";
            labeltimeIn.Text = "";
            labeltimeOut.Text = "";
            WELBAY.Text = "";
            StartTime.Text = "";
            EndTime.Text = "";
            pictureBoxPicture.Image = null;
        }
        #endregion

        #region fill Time IN Out
        private void fillTimeINOut()
        {
            DbConnection.con.Close();
            SqlCommand cmd = new SqlCommand("SELECT * From TimeTable WHERE EmpCNIC='" + textBox1.Text + "' AND Days='" + day.Text + "';", DbConnection.con);
            DbConnection.con.Open();
            SqlDataReader readerchk;
            readerchk = cmd.ExecuteReader();
            if (readerchk.Read())
            {
                StartTime.Text = readerchk["TimeIn"].ToString();
                EndTime.Text = readerchk["TimeOut"].ToString();
            }
            DbConnection.con.Close();
        }
        #endregion

        private void Attendance_Load(object sender, EventArgs e)
        {
            //loadReport();         
            //textBox1.Text = VerificationForm.cnic.ToString();
            Margin();
            timer1.Start();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = System.DateTime.Now.ToString("MMMMMMMMM yyyy");
            day.Text = System.DateTime.Now.ToString("dddd");
            date.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
            time.Text = System.DateTime.Now.ToString("HH:mm");
            TimeFor.Text = System.DateTime.Now.ToString("HH:mm");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Splash splash = new Splash();
            splash.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Attendance_FormClosing(object sender, FormClosingEventArgs e)
        {
            DbConnection.con.Close();
            System.Environment.Exit(1);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminLogin frm = new AdminLogin();
            this.Hide();
            frm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DbConnection.checkConnection();
            if (textBox1.Text == "")
            {
               
            }
            else
            {
                CheckAuthentication();
                CheckIN();
                AttendPage1.Refresh();
                textBox1.Text = "";
                crystalReportViewer1.RefreshReport();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(6);
            if (progressBar1.Value == 100)
            {
                timer2.Stop();
                MessageBox.Show("Attendance Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressBar1.Value = 0;
                labelname.Text = "";
                labelcnic.Text = "";
                labeltimeIn.Text = "";
                labeltimeOut.Text = "";
                WELBAY.Text = "";
                StartTime.Text = "--------";
                EndTime.Text = "--------";
                lateIn.Text = "--------";
                earlyOut.Text = "--------";
                Status.Text = "--------";
                pictureBoxPicture.Image = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void labeltimeIn_Click(object sender, EventArgs e)
        {

        }

        private void lateIn_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            VerificationForm var = new VerificationForm();
            var.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            if (var.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = VerificationForm.cnic;
            }
        }

        private void AttendPage1_InitReport(object sender, EventArgs e)
        {

        }
        private void loadReport()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                //timer1.Enabled = true;
                CrystalReport1 rpt = new CrystalReport1();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                DailtAttendanceOnFront myDS = new DailtAttendanceOnFront();
                //The DataSet you created.


                myConnection = new SqlConnection("Data Source=khalid\\sqlexpress;Initial Catalog=FinalProject;Integrated Security=True");
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "SELECT Emp.EmpName,	Emp.EmpType,	Emp.Picture,	Attendance.TimeIn,	Attendance.TimeOut,	(CONVERT(NVARCHAR, Date, 105))		FROM EmpRegistration Emp INNER JOIN Attendance on Emp.EmpCNIC=Attendance.EmpCNIC AND Date=convert(varchar(10),getdate(),101) ";

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "AttendanceOnFront");
                rpt.SetDataSource(myDS);
                crystalReportViewer1.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        
    }
}