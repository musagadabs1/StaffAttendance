using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalProject
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }
         ////////////////////if (forgot.Text == "")
         ////////////////////   {
         ////////////////////       MessageBox.Show("Enter valid Email");
         ////////////////////   }
         ////////////////////   else
         ////////////////////   {
         ////////////////////       try
         ////////////////////       {
         ////////////////////           MailMessage mail = new MailMessage(retrivePass.Text, forgot.Text, label3.Text, Body.Text+txtPass.Text);
         ////////////////////           SmtpClient client = new SmtpClient("smtp.gmail.com");
         ////////////////////           client.Port = 587;
         ////////////////////           client.Credentials = new System.Net.NetworkCredential("habdulrahman1@gmail.com", "Your Mail Password");
         ////////////////////           client.EnableSsl = true;
         ////////////////////           client.Send(mail);

         ////////////////////           MessageBox.Show("Password sent in your Email");
         ////////////////////       }
         ////////////////////       catch(Exception ex)
         ////////////////////       {
         ////////////////////           MessageBox.Show(ex.Message,"Check Internet connection",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
         ////////////////////       }
         ////////////////////   }

        #region Change PAssword
        private void Change_PAssword()
        {
            if (this.checkBox1.Checked)
            {
                labelLogin.Visible = false;
                this.groupLogin.Visible = false;
                labelPassChange.Visible = true;
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
                labelPassChange.Visible = false;
                labelLogin.Visible = true;
                this.groupLogin.Visible = true;
            }
        }
        #endregion

        #region LogIN
        private void Login()
        {
            DbConnection.checkConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Admin where UserName='" + this.textBoxUserName.Text + "' AND Password='" + this.textBoxPassword.Text + "';",DbConnection.con);
                SqlDataReader mreader;
                DbConnection.con.Open();
                mreader = cmd.ExecuteReader();
                int count = 0;
                while (mreader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    Main main = new Main();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    Verification.InvalidUser();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region ChangePassword
        private void Change()
        {
            DbConnection.checkConnection();
            if(textBoxCurrent.Text==""||textBoxnew.Text==""||textBoxrepass.Text=="")
            {
                Verification.Input();
            }
            else if (textBoxnew.Text != textBoxrepass.Text)
            {
                Verification.PasswordMatch();
            }
            else
            {
                try
                {
                    DbConnection.con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Password FROM Admin WHERE Password='" + this.textBoxCurrent.Text + "';", DbConnection.con);
                    SqlDataReader read;
                    read = cmd.ExecuteReader();
                    if (read.Read())
                    {
                        DbConnection.checkConnection();
                        DbConnection.con.Open();
                        SqlCommand change = new SqlCommand();
                        change.Connection = DbConnection.con;
                        change.CommandType = CommandType.StoredProcedure;
                        change.CommandText = "UpdatePassword";
                        change.Parameters.AddWithValue("@password", textBoxnew.Text);
                        change.ExecuteNonQuery();
                        DbConnection.con.Close();
                        Verification.Update();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Current password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DbConnection.con.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Change_PAssword();
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;//Change Visibility 
            labelPassChange.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Splash splash = new Splash();
            splash.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void AdminLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Splash splash = new Splash();
            splash.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Change();
        }
    }
}
