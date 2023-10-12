using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeatView.Services.Data
{
    public class VenueDAO
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeatViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        internal List<VenueModel> getAllVenues(VenueModel venue, OwnerModel owner)
        {
            // create a list of venues to be returned from the method
            List<VenueModel> returnListOfVenues = new List<VenueModel>();

            string queryString = "SELECT * FROM Venues WHERE ownerID = @ownerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@ownerID", System.Data.SqlDbType.VarChar, 50).Value = owner.id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // loop through each row and add the contents to a local venue variable that can be added to the return list of venues
                        while (reader.Read())
                        {
                            VenueModel currVenue = new VenueModel();
                            currVenue.id = reader.GetInt32(0);
                            currVenue.name = reader.GetString(1);
                            currVenue.address = reader.GetString(2);
                            currVenue.street = reader.GetString(3);
                            currVenue.city = reader.GetString(4);
                            currVenue.state = reader.GetString(5);
                            currVenue.zipCode = reader.GetString(6);
                            currVenue.layoutURL = reader.GetString(7);

                            returnListOfVenues.Add(currVenue);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return returnListOfVenues;
            }
        } 
    }
}