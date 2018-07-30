using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Main : Form
    {
        EmpRegistration emp = new EmpRegistration();
        TimeSheet time = new TimeSheet();
        FormReports rpt = new FormReports();
        MonthlyReport rptMonth = new MonthlyReport();
        Year year = new Year();
        EmpLeave leave = new EmpLeave();
        About about = new About();
        Help help = new Help();
        public Main()
        {
            InitializeComponent();
           
        }

        #region Form Main Load

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        #endregion

        public void load()
        {
            time.BringToFront();
            time.MdiParent = this;
            time.Show();
            time.WindowState = FormWindowState.Normal;
        }

        #region for time
        private void timer1_Tick(object sender, EventArgs e)
        {
            day.Text = System.DateTime.Now.ToString("dddd");
            date.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
            timeNow.Text = System.DateTime.Now.ToString("HH:mm:ss");
        }
        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {           
           
        }

        private void StudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void SearchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Splash splash = new Splash();
            splash.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void dailyReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rpt.BringToFront();
            rpt.MdiParent = this;
            rpt.Show();
            rpt.WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            about.BringToFront();
            //about.MdiParent = this;
            about.ShowDialog();
            about.WindowState = FormWindowState.Normal;
        }

        private void newEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DbConnection.checkConnection();
            emp.BringToFront();
            emp.MdiParent = this;
            emp.Show();
            emp.WindowState = FormWindowState.Normal;
        }

        private void employeeTimeSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load();
        }

        private void monthlyReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptMonth.BringToFront();
            rptMonth.MdiParent = this;
            rptMonth.Show();
            rptMonth.WindowState = FormWindowState.Normal;
        }

        private void acamedicYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            year.ShowDialog();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void BookReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help.BringToFront();
            help.MdiParent = this;
            help.Show();
            help.WindowState = FormWindowState.Normal;
        }

        private void timeMarginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeMargin margin = new TimeMargin();
            margin.ShowDialog();
        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Attendance1 attend = new Attendance1();
            attend.Show();
        }

        private void leaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leave.BringToFront();
            leave.MdiParent = this;
            leave.Show();
            leave.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }      
        
    }
}
