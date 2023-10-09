using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeatView.Services.Data
{
    // queries the database for matching username and password credentials
    public class SecurityDAO
    {
        // define a connection strin to the database -- where is it on the server to access it
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeatViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // is there a matching owner in the database for the input credentials?
        internal bool FindByOwner(OwnerModel owner)
        {
            bool retVal = false;

            // string to house the SQL query to check if the credentials can be found
            string queryString = "SELECT * FROM dbo.Owners WHERE username = @username AND password = @password";

            // connect to the database with a SqlConnection passing in the path to the database on the server
            using (SqlConnection connection  = new SqlConnection(connectionString))
            {
                // create a SqlCommand passing in the query and the established connection to the database
                SqlCommand command = new SqlCommand(queryString, connection);

                // replace the placeholder values of '@username' and '@password' with the values that were entered
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = owner.username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = owner.password;

                try
                {
                    // attempt to open the database and execute the command
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // if rows are returned, return true
                    if (reader.HasRows)
                    {
                        retVal = true;
                    }
                }
                catch(Exception e)
                {
                    // print out any exceptions that were thrown in the process of querying the database
                    Console.WriteLine(e.Message);
                }

            }
            return retVal;
        }
    }
}