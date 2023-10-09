using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeatView.Services.Data
{
    public class SecurityDAO
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeatViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        internal bool FindByOwner(OwnerModel owner)
        {
            bool retVal = false;
            string queryString = "SELECT * FROM dbo.Owners WHERE username = @username AND password = @password";

            using (SqlConnection connection  = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = owner.username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = owner.password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        retVal = true;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return retVal;
        }
    }
}