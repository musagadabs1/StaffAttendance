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

namespace FinalProject
{
     

    public partial class TimeSheet : Form
    {

        DataTable dtable = new DataTable();
        DataTable dt = new DataTable();
        public TimeSheet()
        {
            InitializeComponent();
        }
        #region Fill Time Sheet
        public void FillMonday()
        {

            SqlCommand cmd = new SqlCommand("select * from TimeTable where EmpCNIC='" + textBoxControl.Text + "' AND Days='" + Mon.Text + "';", DbConnection.con);
            SqlDataReader read;
            try
            {
                DbConnection.con.Open();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    MonIn.Text = read["TimeIn"].ToString();
                    MonOut.Text = read["TimeOut"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillTuesday()
        {

            SqlCommand cmd = new SqlCommand("select * from TimeTable where EmpCNIC='" + textBoxControl.Text + "' AND Days='" + Tues.Text + "';", DbConnection.con);
            SqlDataReader read;
            try
            {
                DbConnection.con.Open();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    TuesIn.Text = read["TimeIn"].ToString();
                    TuesOut.Text = read["TimeOut"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillWednesday()
        {

            SqlCommand cmd = new SqlCommand("select * from TimeTable where EmpCNIC='" + textBoxControl.Text + "' AND Days='" + Wed.Text + "';", DbConnection.con);
            SqlDataReader read;
            try
            {
                DbConnection.con.Open();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    WedIn.Text = read["TimeIn"].ToString();
                    WedOut.Text = read["TimeOut"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillThursday()
        {

            SqlCommand cmd = new SqlCommand("select * from TimeTable where EmpCNIC='" + textBoxControl.Text + "' AND Days='" + Thu.Text + "';", DbConnection.con);
            SqlDataReader read;
            try
            {
                DbConnection.con.Open();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    ThuIn.Text = read["TimeIn"].ToString();
                    ThuOut.Text = read["TimeOut"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillFriday()
        {

            SqlCommand cmd = new SqlCommand("select * from TimeTable where EmpCNIC='" + textBoxControl.Text + "' AND Days='" + Fri.Text + "';", DbConnection.con);
            SqlDataReader read;
            try
            {
                DbConnection.con.Open();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    FriIn.Text = read["TimeIn"].ToString();
                    FriOut.Text = read["TimeOut"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillSaturday()
        {

            SqlCommand cmd = new SqlCommand("select * from TimeTable where EmpCNIC='" + textBoxControl.Text + "' AND Days='" + Sat.Text + "';", DbConnection.con);
            SqlDataReader read;
            try
            {
                DbConnection.con.Open();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    SatIn.Text = read["TimeIn"].ToString();
                    SatOut.Text = read["TimeOut"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region fill to Datagrid view
        public void gridView()
        {
            SqlCommand cmd = new SqlCommand(TimeTable.fillDataGrid, DbConnection.con);
            try
            {
                DbConnection.con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridViewEmployee.DataSource = dt;
                da.Update(dt);
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region byte to image
        void image()
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                string sql = "SELECT Picture FROM EmpRegistration WHERE EmpCNIC='" + textBoxCnic.Text + "';";
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

        #region Academic
        private void academic()
        {
            SqlCommand cmd = new SqlCommand("select * from AcademicYear",DbConnection.con);
            SqlDataReader read;
            try
            {
                DbConnection.con.Open();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    year.Text = read["AcademicYear"].ToString();
                    semester.Text=read["Semester"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        #endregion

        //#region TIme in VS Time Out
        //public void Monday()
        //{
        //    decimal Start, Stop;
        //    Start = Convert.ToDecimal(MonIn.Text);
        //    Stop = Convert.ToDecimal(MonOut.Text);
        //    if (Start >= Stop)
        //    {
        //        Verification.time();
        //        this.MonOut.Focus();
        //    }
        //}
        //public void Tuesday()
        //{
        //    decimal Start, Stop;
        //    Start = Convert.ToDecimal(TuesIn.Text);
        //    Stop = Convert.ToDecimal(TuesOut.Text);
        //    if (Start >= Stop)
        //    {
        //        Verification.time();
        //        this.TuesOut.Focus();
        //    }
        //}
        //public void Wednesday()
        //{
        //    decimal Start, Stop;
        //    Start = Convert.ToDecimal(WedIn.Text);
        //    Stop = Convert.ToDecimal(WedOut.Text);
        //    if (Start >= Stop)
        //    {
        //        Verification.time();
        //        this.WedOut.Focus();
        //    }
        //}
        //public void Thursday()
        //{
        //    decimal Start, Stop;
        //    Start = Convert.ToDecimal(ThuIn.Text);
        //    Stop = Convert.ToDecimal(ThuOut.Text);
        //    if (Start >= Stop)
        //    {
        //        Verification.time();
        //        this.ThuOut.Focus();
        //    }
        //}
        //public void Friday()
        //{
        //    decimal Start, Stop;
        //    Start = Convert.ToDecimal(FriIn.Text);
        //    Stop = Convert.ToDecimal(FriOut.Text);
        //    if (Start >= Stop)
        //    {
        //        Verification.time();
        //        this.FriOut.Focus();
        //    }
        //}
        //public void Saturday()
        //{
        //    decimal Start, Stop;
        //    Start = Convert.ToDecimal(SatIn.Text);
        //    Stop = Convert.ToDecimal(SatOut.Text);
        //    if (Start >= Stop)
        //    {
        //        Verification.time();
        //        this.SatOut.Focus();
        //    }
        //}
        //#endregion

        #region Update Time Table
        public void Tmonday()
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
            cmd.CommandText = "UpdateTimeTable";
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxControl.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Mon.Text);
            cmd.Parameters.AddWithValue("@timeIn", MonIn.Text.Trim());
            cmd.Parameters.AddWithValue("@timeOut", MonOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }

        public void Ttuesday()
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
            cmd.CommandText = "UpdateTimeTable";
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxControl.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Tues.Text);
            cmd.Parameters.AddWithValue("@timeIn", TuesIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", TuesOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public void Twednesday()
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
            cmd.CommandText = "UpdateTimeTable";
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxControl.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Wed.Text);
            cmd.Parameters.AddWithValue("@timeIn", WedIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", WedOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public void Tthursday()
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
            cmd.CommandText = "UpdateTimeTable";
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxControl.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Thu.Text);
            cmd.Parameters.AddWithValue("@timeIn", ThuIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", ThuOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public void Tfriday()
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
            cmd.CommandText = "UpdateTimeTable";
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxControl.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Fri.Text);
            cmd.Parameters.AddWithValue("@timeIn", FriIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", FriOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public void Tsaturdat()
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
            cmd.CommandText = "UpdateTimeTable";
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxControl.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Sat.Text);
            cmd.Parameters.AddWithValue("@timeIn", SatIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", SatOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        #endregion

        #region Save Time Table
        public void STmonday()
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
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Mon.Text);
            cmd.Parameters.AddWithValue("@timeIn", MonIn.Text.Trim());
            cmd.Parameters.AddWithValue("@timeOut", MonOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }

        public void STtuesday()
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
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Tues.Text);
            cmd.Parameters.AddWithValue("@timeIn", TuesIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", TuesOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public void STwednesday()
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
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Wed.Text);
            cmd.Parameters.AddWithValue("@timeIn", WedIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", WedOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public void STthursday()
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
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Thu.Text);
            cmd.Parameters.AddWithValue("@timeIn", ThuIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", ThuOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public void STfriday()
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
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Fri.Text);
            cmd.Parameters.AddWithValue("@timeIn", FriIn.Text);
            cmd.Parameters.AddWithValue("@timeOut", FriOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        public void STsaturdat()
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
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@cnic", textBoxCnic.Text);
            cmd.Parameters.AddWithValue("@academic", year.Text);
            cmd.Parameters.AddWithValue("@semester", semester.Text);
            cmd.Parameters.AddWithValue("@day", Sat.Text);
            cmd.Parameters.AddWithValue("@timeIn", SatIn.Text.Trim());
            cmd.Parameters.AddWithValue("@timeOut", SatOut.Text);
            cmd.ExecuteNonQuery();
            //Verification.Save();
            DbConnection.con.Close();
        }
        #endregion

        private void Reset()
        {
            pictureBoxPicture.Image = null;
            textBoxControl.Text = "";
            textBoxName.Text = "";
            textBoxCnic.Text = "";
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TimeSheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SatIn.Text != "")
            {
                SatOut.Visible = true;
            }
            else
            {
                SatOut.Visible = false;
            }
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FriIn.Text != "")
            {
                FriOut.Visible = true;
            }
            else
            {
                FriOut.Visible = false;
            }
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ThuIn.Text != "")
            {
                ThuOut.Visible = true;
            }
            else
            {
                ThuOut.Visible = false;
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WedIn.Text != "")
            {
                WedOut.Visible = true;
            }
            else
            {
                WedOut.Visible = false;
            }
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void TimeSheet_Load(object sender, EventArgs e)
        {
            gridView();
            academic();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeData empData = new EmployeeData();
            academic();
            empData.Show();
               
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            image();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (textBoxCnic.Text == "" || textBoxName.Text == "")
            {
                MessageBox.Show("Please select the employee that you want manage time table","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmdChk = new SqlCommand("select EmpCNIC from TimeTable where EmpCNIC='" + textBoxCnic.Text + "';", DbConnection.con);
                SqlParameter param = new SqlParameter();
                if (DbConnection.con.State != ConnectionState.Open)
                {
                    DbConnection.con.Open();
                }
                param.Value = this.textBoxCnic.Text;
                cmdChk.Parameters.Add(param);
                SqlDataReader read = cmdChk.ExecuteReader();
                if (read.HasRows)
                {
                    DbConnection.con.Close();
                    MessageBox.Show("Employee Time Table is alreary manage","Sory!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        STmonday();
                        STtuesday();
                        STwednesday();
                        STthursday();
                        STfriday();
                        STsaturdat();
                        Verification.Save();
                        gridView();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxControl.Text == "")
            {
                MessageBox.Show("Please select Employee in Below that you want delete", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DbConnection.con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DeleteTimeTable";
                        cmd.Parameters.AddWithValue("@cnic",textBoxControl.Text);
                    DbConnection.con.Open();
                    cmd.ExecuteNonQuery();
                    DbConnection.con.Close();
                    Verification.Delete();
                    gridView();
                    Reset();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox7_Leave(object sender, EventArgs e)
        {
            if (MonOut.Text!="")
            {
                //Monday();
            }
        }

        private void TuesOut_Leave(object sender, EventArgs e)
        {
            //Tuesday();
        }

        private void WedOut_Leave(object sender, EventArgs e)
        {
            //Wednesday();
        }

        private void ThuOut_Leave(object sender, EventArgs e)
        {
            //Thursday();
        }

        private void FriOut_Leave(object sender, EventArgs e)
        {
            //Friday();
        }

        private void SatOut_Leave(object sender, EventArgs e)
        {
            //Saturday();
        }

        private void MonIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (MonIn.Text != "")
            //{
            //    MonOut.Visible = true;
            //}
            //else
            //{
            //    MonOut.Visible = false;
            //}
        }

        private void TuesIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TuesIn.Text != "")
            {
                TuesOut.Visible = true;
            }
            else
            {
                TuesOut.Visible = false;
            }
        }

        private void textBoxControl_TextChanged(object sender, EventArgs e)
        {
            FillMonday();
            FillTuesday();
            FillWednesday();
            FillThursday();
            FillFriday();
            FillSaturday();
        }

        private void dataGridViewEmployee_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr=dataGridViewEmployee.SelectedRows[0];
            textBoxName.Text=dr.Cells[0].Value.ToString();
            textBoxControl.Text=dr.Cells[1].Value.ToString();
            textBoxCnic.Text=dr.Cells[1].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxControl.Text == "")
            {
                MessageBox.Show("Please select Employee that you want update", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    Tmonday();
                    Ttuesday();
                    Twednesday();
                    Tthursday();
                    Tfriday();
                    Tsaturdat();
                    Verification.Save();
                    gridView();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ViewTimeTable frm = new ViewTimeTable();
            //this.Hide();
            frm.ShowDialog();
        }

        private void MonOut_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
