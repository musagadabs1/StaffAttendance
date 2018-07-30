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
    public partial class TimeMargin : Form
    {
        #region Retrive Time
        private void Retrive()
        {
            try
            {
                DbConnection.checkConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM TimeMargin", DbConnection.con);
                SqlDataReader reader;
                DbConnection.con.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox1.Text = reader["Margin"].ToString();
                    MarginID.Text=reader["Id"].ToString();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region
        private void UpdateMargin()
        {
            try
            {
                DbConnection.checkConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DbConnection.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateMargin";
                cmd.Parameters.AddWithValue("@Margin",comboBox1.Text);
                cmd.Parameters.AddWithValue("Id",MarginID.Text);
                DbConnection.con.Open();
                cmd.ExecuteNonQuery();
                Verification.Save();
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        public TimeMargin()
        {
            InitializeComponent();
        }

        private void TimeMargin_Load(object sender, EventArgs e)
        {
            Retrive();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateMargin();
        }
    }
}
