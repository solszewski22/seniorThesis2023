using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeatView.Services.Data
{
    public class InsertDAO
    {
        // connection string to connect to the database on the server (database location path)
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeatViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // method that returns true if the query insert is successful
        internal bool InsertOwner(OwnerModel ownerModel)
        {
            bool retVal = false;

            // database query string to insert the entered new owner
            string queryString = "INSERT INTO dbo.Owners (firstName, lastName, company, username, password) VALUES (@firstName, @lastName, @company, @username, @password)";

            // create a connection to the database using the path to the database on the server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create an instance of a command to fill the empty parts of the queryString
                SqlCommand command = new SqlCommand(queryString, connection);

                // assign values based on what the user entered
                command.Parameters.Add("@firstName", System.Data.SqlDbType.VarChar, 50).Value = ownerModel.firstName;
                command.Parameters.Add("@lastName", System.Data.SqlDbType.VarChar, 50).Value = ownerModel.lastName;
                command.Parameters.Add("@company", System.Data.SqlDbType.VarChar, 50).Value = ownerModel.company;
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = ownerModel.username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = ownerModel.password;

                try
                {
                    // attempt to open the database and execute a non-query statement (like INSERT, DELETE, UPDATE)
                    connection.Open();

                    // this returns the number of rows effected (in this case 1 row is effected); returns -1 for everything else
                    int rowsEffected = command.ExecuteNonQuery();

                    // if more than one row is effected, the insert was successful
                    if (rowsEffected > 0)
                    {
                        retVal = true;
                    }
                }
                catch (Exception e)
                {
                    // print out a message if an exception is thrown during the prcess of an insert statement
                    Console.WriteLine(e.Message);
                }
            }
            return retVal;
        }

        public OwnerModel getOwnerByID(int id)
        {
            OwnerModel owner = new OwnerModel();
            string queryString = "SELECT * FROM Owners WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create an instance of a command to fill the empty parts of the queryString
                SqlCommand command = new SqlCommand(queryString, connection);

                // assign values based on what the user entered
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // loop through each row and add the contents to a local venue variable that can be added to the return list of venues
                        while (reader.Read())
                        {
                            owner.id = reader.GetInt32(0);
                            owner.firstName = reader.GetString(1);
                            owner.lastName = reader.GetString(2);
                            owner.company = reader.GetString(3);
                        }
                    }
                }
                catch (Exception e)
                {
                    // print out a message if an exception is thrown during the prcess of an insert statement
                    Console.WriteLine(e.Message);
                }
            }
            return owner;
        }
    }
}