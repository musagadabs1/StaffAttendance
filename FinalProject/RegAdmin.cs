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
    public partial class RegAdmin : Form
    {
        public RegAdmin()
        {
            InitializeComponent();
        }

        #region Create Admin
        private void CreateUser()
        {
            DbConnection.checkConnection();
            if(textBoxID.Text==""||textBoxmail.Text==""||textBoxpass.Text==""||textBoxrepass.Text=="")
            {
                MessageBox.Show("Please fill all fields","Some field empty",MessageBoxButtons.OK,MessageBoxIcon.Stop);                
            }
            else if(textBoxpass.Text!=textBoxrepass.Text)
            {
            MessageBox.Show("Both password must be match","Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    DbConnection.con.Open();
                    cmd.Connection = DbConnection.con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "StAdmin";
                    cmd.Parameters.AddWithValue("@userName", textBoxID.Text);
                    cmd.Parameters.AddWithValue("@Email", textBoxmail.Text);
                    cmd.Parameters.AddWithValue("@password", textBoxpass.Text);
                    cmd.ExecuteNonQuery();
                    DbConnection.con.Close();
                    MessageBox.Show("Account successfully created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main main = new Main();
                    main.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion
        private void button3_Click(object sender, EventArgs e)
        {
            CreateUser();
            
        }
    }
}
