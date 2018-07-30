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
    public partial class EmployeeData : Form
    {
        public EmployeeData()
        {
            InitializeComponent();
            
        }
        
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


        private void EmployeeData_Load(object sender, EventArgs e)
        {
            gridView();
        }

        private void dataGridViewEmployee_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridViewEmployee.SelectedRows[0];
            this.Hide();
            TimeSheet frmTimeSheet = new TimeSheet();
            frmTimeSheet.Show();
            frmTimeSheet.textBoxName.Text=dr.Cells[0].Value.ToString();
            frmTimeSheet.textBoxCnic.Text=dr.Cells[1].Value.ToString();
        }

        private void EmployeeData_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimeSheet frm = new TimeSheet();
            this.Hide();
            frm.Show();
        }
    }
}
