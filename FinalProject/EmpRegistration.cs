using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Business_Entities;
using DAL;

namespace FinalProject
{
    public partial class EmpRegistration : Form
    {
        string imageLocation = "";

        #region Add Staff Members
        public static List<Byte[]> prints = new List<byte[]>();
        Bus_Person busper = new Bus_Person();
        Dal_Person dalper = new Dal_Person();

      
        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate()
            {
                Template = template;
                if (Template != null)
                    MessageBox.Show("The Fingerprint Template is Saved.", "Fingerprint Enrollment");
                else
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
            }));
        }
        private DPFP.Template Template;
        delegate void Function();

        #endregion 
     
        public EmpRegistration()
        {
            InitializeComponent();
        }
        
        #region Button Instance
        EmployeeData empData = new EmployeeData();
        EmpCards cards = new EmpCards();
        #endregion

        #region gender
        string gender = "";
        public void Gender()
        {
            if (radioButtonmale.Checked == true)
            {
                gender = "Male";
            }
            else if (radioButtonfemale.Checked == true)
            {
                gender = "Female";
            }
        }
        #endregion Account status


        #region Account status
        string account = "";
        public void Account()
        {
            if (radioButtonactive.Checked == true)
            {
                account = "Active";
            }
            else if (radioButtondeactive.Checked == true)
            {
                account = "Deactive";
            }
        }
        #endregion

        #region fill to Datagrid view
        public void gridView()
        {
            SqlCommand cmd = new SqlCommand("select EmpName as'Name', EmpCNIC as'CNIC',Gender,Accountstatus as'Status',Mobile,Email,DOB,JoinDate as'Joining',JobType as'Job Type',EmpType as'Emp Type' From EmpRegistration", DbConnection.con);
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

        #region Reser fields
        public void Reset()
        {
            IdUpdate.Text = "";
            textBoxName.Text = "";
            TextBoxcnic.Text = "";
            textBoxaddress.Text = "";
            textBoxcity.Text = "";
            textBoxmail.Text = "";
            textBoxphone.Text = "";
            radioButtonactive.Checked = false;
            radioButtondeactive.Checked = false;
            radioButtonmale.Checked = false;
            radioButtonfemale.Checked = false;
            pictureBoxPicture.Image = null;
        }
        #endregion

        #region byte to image
        void image()
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                string sql = "SELECT Picture FROM EmpRegistration WHERE EmpCNIC='"+IdUpdate.Text+"';";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            empData.BringToFront();
            empData.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog pic = new OpenFileDialog();
            pic.Filter = "JPG Files|*.jpg|PNG Files|*.png";
            if (pic.ShowDialog() == DialogResult.OK)
            {
                imageLocation = pic.FileName.ToString();
                pictureBoxPicture.ImageLocation = imageLocation;
            }
        }

        private void saveprints(Byte[] array)
        {
            DbConnection.checkConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Finger (EmpCNIC,Finger)values(@cnic,@img)", DbConnection.con);

                cmd.Parameters.Add(new SqlParameter("@cnic", TextBoxcnic.Text));
                cmd.Parameters.Add(new SqlParameter("@img", array));
                if (DbConnection.con.State == System.Data.ConnectionState.Closed)
                {
                    DbConnection.con.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void EmpRegistration_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void EmpRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void EmpRegistration_Click(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (prints.Count != 0)
            {
                foreach (Byte[] b in prints)
                {                   
                    saveprints(b);
                }
            }
            else
            {
                MessageBox.Show("Please Enter Your Finger Prints");
            }
            
            Gender();
            Account();
            if (textBoxName.Text == "" || TextBoxcnic.Text == "" || textBoxaddress.Text == "" || textBoxcity.Text == "" || textBoxmail.Text == "" || textBoxphone.Text == "" || comboBoxjobType.Text == "" || comboBoxempType.Text == "")
            {
                Verification.Input();
            }
            else if (imageLocation == "")
            {
                Verification.Picture();
            }
            else if(gender=="")
            {
            Verification.Gender();
            }
            else if (account == "")
            {
                Verification.AccountStatus();
            }
            else
            {               
                SqlCommand cmdChk = new SqlCommand("select EmpCNIC from EmpRegistration where EmpCNIC='"+TextBoxcnic.Text+"';",DbConnection.con);
                SqlParameter param = new SqlParameter();
                if (DbConnection.con.State != ConnectionState.Open)
                {
                    DbConnection.con.Open();
                }
                param.Value = this.TextBoxcnic.Text;
                cmdChk.Parameters.Add(param);
                SqlDataReader read = cmdChk.ExecuteReader();
                if (read.HasRows)
                {
                    DbConnection.con.Close();
                    Verification.Duplicate();
                }
                else
                {
                    try
                    {
                        DbConnection.con.Close();
                        if (DbConnection.con.State != ConnectionState.Open)
                        {
                            DbConnection.con.Open();
                        }
                        byte[] image = null;
                        FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        image = br.ReadBytes((int)fs.Length);
                        //////////////////////////////////////////////////////////////////////////////
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = DbConnection.con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Registration";
                        cmd.Parameters.AddWithValue("@name", textBoxName.Text.TrimEnd());
                        cmd.Parameters.AddWithValue("@cnic", TextBoxcnic.Text);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@address", textBoxaddress.Text);
                        cmd.Parameters.AddWithValue("@city", textBoxcity.Text);
                        cmd.Parameters.AddWithValue("@accountStatus", account);
                        cmd.Parameters.AddWithValue("@email", textBoxmail.Text);
                        cmd.Parameters.AddWithValue("@mobile", textBoxphone.Text);
                        cmd.Parameters.AddWithValue("@dob", dateTimedob.Text);
                        cmd.Parameters.AddWithValue("@joinDate", dateTimejoin.Text);
                        cmd.Parameters.AddWithValue("@jobType", comboBoxjobType.Text);
                        cmd.Parameters.AddWithValue("@empType", comboBoxempType.Text);
                        //cmd.Parameters.Add(new SqlParameter("@picture", image));
                        cmd.Parameters.Add(new SqlParameter("@picture", image));
                        cmd.Parameters.Add(new SqlParameter("@finger", image));
                        cmd.ExecuteNonQuery();
                        Verification.Save();
                        DbConnection.con.Close();
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

        private void radioButtonmale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonfemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void EmpRegistration_Load(object sender, EventArgs e)
        {
           gridView();
        }

        private void dataGridViewEmployee_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr=dataGridViewEmployee.SelectedRows[0];
            textBoxName.Text = dr.Cells[0].Value.ToString();
            TextBoxcnic.Text=dr.Cells[1].Value.ToString();
            IdUpdate.Text=dr.Cells[1].Value.ToString();
            textBoxphone.Text=dr.Cells[4].Value.ToString();
            textBoxmail.Text=dr.Cells[5].Value.ToString();
            dateTimedob.Text=dr.Cells[6].Value.ToString();
            dateTimejoin.Text=dr.Cells[7].Value.ToString();
            comboBoxjobType.Text=dr.Cells[8].Value.ToString();
            comboBoxempType.Text=dr.Cells[9].Value.ToString();
        }

        private void TextBoxcnic_ModifiedChanged(object sender, EventArgs e)
        {
            //image();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            //image();
        }

        private void textBoxControl_TextChanged(object sender, EventArgs e)
        {
            image();
            SqlCommand cmd = new SqlCommand("select * from EmpRegistration where EmpCNIC='"+this.IdUpdate.Text+"';",DbConnection.con);
            DbConnection.con.Open();
            SqlDataReader read;
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                string gender = read["Gender"].ToString();
                string status=read["AccountStatus"].ToString();
                if (gender == "Female")
                {
                    radioButtonfemale.Checked = true;
                }
                else
                {
                    radioButtonmale.Checked = true;
                }
                if (status == "Deactive")
                {
                    radioButtondeactive.Checked = true;
                }
                else
                {
                    radioButtonactive.Checked = true;
                
                }
                textBoxaddress.Text=read["EmpAddress"].ToString();
                textBoxcity.Text=read["EmpCity"].ToString();

            }
            DbConnection.con.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (IdUpdate.Text == "")
            {
                MessageBox.Show("Please select Row that you want update", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                Gender();
                Account();
                if (textBoxName.Text == "" || TextBoxcnic.Text == "" || textBoxaddress.Text == "" || textBoxcity.Text == "" || textBoxmail.Text == "" || textBoxphone.Text == "" || comboBoxjobType.Text == "" || comboBoxempType.Text == "")
                {
                    Verification.Input();
                }
                //else if (imageLocation == "")
                //{
                //    Verification.Picture();
                //}
                else if (gender == "")
                {
                    Verification.Gender();
                }
                else if (account == "")
                {
                    Verification.AccountStatus();
                }
                else
                {
                    try
                    {
                        if (DbConnection.con.State != ConnectionState.Open)
                        {
                            DbConnection.con.Open();
                        }

                        //byte[] image = null;
                        //FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
                        //BinaryReader br = new BinaryReader(fs);
                        //image = br.ReadBytes((int)fs.Length);
                        //////////////////////////////////////////////////////////////////////////////
                        SqlCommand cmd = new SqlCommand();

                        cmd.Connection = DbConnection.con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EmpUpdate";
                        cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                        //cmd.Parameters.AddWithValue("@cnic", TextBoxcnic.Text);
                        cmd.Parameters.AddWithValue("@cnic1", IdUpdate.Text);
                        //cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@address", textBoxaddress.Text);
                        cmd.Parameters.AddWithValue("@city", textBoxcity.Text);
                        cmd.Parameters.AddWithValue("@accountStatus", account);
                        cmd.Parameters.AddWithValue("@email", textBoxmail.Text);
                        cmd.Parameters.AddWithValue("@mobile", textBoxphone.Text);
                        cmd.Parameters.AddWithValue("@dob", dateTimedob.Text);
                        cmd.Parameters.AddWithValue("@joinDate", dateTimejoin.Text);
                        cmd.Parameters.AddWithValue("@jobType", comboBoxjobType.Text);
                        cmd.Parameters.AddWithValue("@empType", comboBoxempType.Text);
                        //cmd.Parameters.Add(new SqlParameter("@picture", image));
                        //cmd.Parameters.Add(new SqlParameter("@picture", image));
                        cmd.ExecuteNonQuery();
                        Verification.Update();
                        DbConnection.con.Close();
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (IdUpdate.Text == "")
            {
                MessageBox.Show("Please select Row that you want delete", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                DialogResult Question = MessageBox.Show("Are you sure want to delete?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Question == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = DbConnection.con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EmpDelete";
                        cmd.Parameters.AddWithValue("@cnic", IdUpdate.Text);
                        DbConnection.con.Open();
                        cmd.ExecuteNonQuery();
                        Verification.Delete();                        
                        DbConnection.con.Close();
                        gridView();
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }       

        private void btnreport_Click(object sender, EventArgs e)
        {
            cards.BringToFront();
            cards.WindowState = FormWindowState.Normal;
            cards.ShowDialog();
        }       

        private void button1_Click_1(object sender, EventArgs e)
        {
            EnrollmentForm rn = new EnrollmentForm();
            rn.OnTemplate += this.OnTemplate;
            rn.ShowDialog();
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void pictureBoxfinger_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {            
            if (prints.Count != 0)
            {                
                foreach (Byte[] b in prints)
                {
                    saveprints(b);                    
                }
            }
            else
            {
                MessageBox.Show("Please Enter Your Finger Prints");
            }
        }
    }
}
           