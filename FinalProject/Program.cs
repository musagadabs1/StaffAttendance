using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalProject
{
    static class Program
    {       
        [STAThread]
        static void Main()
        {
            DbConnection.checkConnection();
            DbConnection.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT UserName FROM Admin", DbConnection.con);
            SqlDataReader read;
            read=cmd.ExecuteReader();
            if (read.Read())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Splash());
                DbConnection.con.Close();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new RegAdmin());
            }
        }
    }
}
