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
using System.Configuration;

namespace FinalProject
{
    public partial class EmpLeave : Form
    {        
        #region fill datagrid
        private void FillGrid()
        {
            try
            {
                DbConnection.checkConnection();
                SqlCommand cmd = new SqlCommand("SELECT T.EmpName AS'Name',T.EmpCNIC AS'CNIC',T.Days,EmpRegistration.Gender,EmpRegistration.EmpType AS'Emp Tpye',EmpRegistration.JobType AS'Job Tpye',EmpRegistration.JoinDate AS'Joining' FROM TimeTable T INNER JOIN EmpRegistration ON T.EmpCNIC=EmpRegistration.EmpCNIC WHERE Days='"+day.Text+"'AND TimeIn<>'';",DbConnection.con);
                DbConnection.con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridViewLeave.DataSource = dt;
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
                string sql = "SELECT Picture FROM EmpRegistration WHERE EmpCNIC='" + textBoxControl.Text + "';";
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

        #region Check Attendance
        private void Check_attendance()
        {
            try
            {
                ///////////////////Check Attendance In////////////////////////////////////////////////////////////////////
                SqlCommand cmd2 = new SqlCommand("Select * From Attendance Where EmpCNIC='" + labelID.Text + "' AND Date='" + date.Text + "';", DbConnection.con);
                DbConnection.con.Open();
                SqlDataReader reader;
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Processing Fial","Sorry",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    LeaveIn();
                    DbConnection.con.Close();
                }
                DbConnection.con.Close();
                //////////////////////////////////////
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DbConnection.checkConnection();
            }
        }
        #endregion

        #region Leave
        private void LeaveIn()
        {
            DbConnection.con.Close();
            try
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = DbConnection.con;
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "Leave";
                cmd1.Parameters.AddWithValue("@cnic", labelID.Text);
                cmd1.Parameters.AddWithValue("@name", labelName.Text);
                cmd1.Parameters.AddWithValue("@date", date.Text);
                cmd1.Parameters.AddWithValue("@days", day.Text);
                cmd1.Parameters.AddWithValue("@status", comboBoxLeave.Text);
                cmd1.Parameters.AddWithValue("@month",labelMonth.Text);
                cmd1.Parameters.AddWithValue("@description",textBoxdescription.Text);
                MessageBox.Show("Leave Successfully saved","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                DbConnection.con.Open();
                cmd1.ExecuteNonQuery();
                DbConnection.con.Close();
            }
            catch(Exception  ex)
            {
            MessageBox.Show(ex.Message);
            }
            finally
            {
                DbConnection.checkConnection();
            }
        }
        #endregion

        #region Reset
        private void reset()
        {
            textBoxControl.Text = "";
            labelName.Text = "";
            labelID.Text = "";            
        }
        #endregion

        public EmpLeave()
        {
            InitializeComponent();
        }

        private void EmpLeave_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void EmpLeave_Load(object sender, EventArgs e)
        {
            timer1.Start();
            //FillCombo();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelMonth.Text = System.DateTime.Now.ToString("MMMMMMMMM yyyy");
            day.Text = System.DateTime.Now.ToString("dddd");
            date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            time.Text = System.DateTime.Now.ToString("HH:mm");
            EmpLeave leave = new EmpLeave();
            leave.Refresh();
            FillGrid();
            timer1.Stop();
        }

        private void dataGridViewEmployee_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr=dataGridViewLeave.SelectedRows[0];
            labelID.Text=dr.Cells[1].Value.ToString();
            textBoxControl.Text=dr.Cells[1].Value.ToString();
            labelName.Text=dr.Cells[0].Value.ToString();

        }

        private void textBoxControl_TextChanged(object sender, EventArgs e)
        {
            image();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (textBoxdescription.Text == "" || comboBoxLeave.Text == ""||textBoxControl.Text=="")
            {
                MessageBox.Show("Fill the all Box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxLeave.Focus();
                textBoxdescription.Focus();
            }
            else
            {
                DbConnection.checkConnection();
                try
                {
                    if (comboBoxLeave.Text == "Leave")
                    {
                        Check_attendance();
                        reset();
                    }
                    else if (comboBoxLeave.Text == "Sick leave")
                    {
                        MessageBox.Show("Sick Leave");
                    }
                    else
                    {
                        MessageBox.Show("Please select ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        
        }
    }
}
