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
    public partial class Year : Form
    {
        public Year()
        {
            InitializeComponent();
        }

        private void Year_Load(object sender, EventArgs e)
        {

        }

        private void Year_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "" || comboBox1.Text == "")
            {
                Verification.Input();
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DbConnection.con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Academic";
                    DbConnection.con.Open();
                    cmd.Parameters.AddWithValue("@year",maskedTextBox1.Text);
                    cmd.Parameters.AddWithValue("@semester",comboBox1.Text);
                    cmd.ExecuteNonQuery();
                    DbConnection.con.Close();
                    Verification.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
