using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace WebFormsApplication
{
    public class DatabaseConnection
    {

        public static void OpenConnection(SqlConnection con)
        {
            string ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ShoppingCartDemoDBConnectionString"].ConnectionString;
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                //con.ConnectionString = "Data Source=LP-5CD720CW14\\SQLEXPRESS;Initial Catalog=SocialGoaldB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                con.ConnectionString = ConnString;
                con.Open();
            }
            catch (Exception)
            {
                
            }
        }
        public static void CloseConnection(SqlConnection con)
        {
            try
            {
                con.Close();
            }
            catch (Exception)
            {
                
            }
        }

    }
}