using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace FinalProject
{
    class DbConnection
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

       public static void checkConnection()
       {
           if (DbConnection.con.State == ConnectionState.Open)
           {
               DbConnection.con.Close();
           }
       }
    }
        
}
