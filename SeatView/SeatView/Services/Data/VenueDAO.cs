using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeatView.Services.Data
{
    // all functionality with venues
    public class VenueDAO
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeatViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        internal List<VenueModel> getAllVenues(int id)
        {
            // create a list of venues to be returned from the method
            List<VenueModel> returnListOfVenues = new List<VenueModel>();

            string queryString = "SELECT * FROM Venues WHERE ownerID = @ownerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@ownerID", System.Data.SqlDbType.Int).Value = id;

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

        // get a specific venue based on the given id
        internal VenueModel getVenueByID(int venueID)
        {
            string queryString = "SELECT * FROM Venues WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = venueID;

                VenueModel returnVenue = new VenueModel();
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            returnVenue.id = reader.GetInt32(0);
                            returnVenue.name = reader.GetString(1);
                            returnVenue.address = reader.GetString(2);
                            returnVenue.street = reader.GetString(3);
                            returnVenue.city = reader.GetString(4);
                            returnVenue.state = reader.GetString(5);
                            returnVenue.zipCode = reader.GetString(6);
                            returnVenue.layoutURL = reader.GetString(7);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return returnVenue;
            }
        }
    
        // add a venue to the database
        internal bool addVenue(VenueModel venue, int ownerID)
        {
            bool retVal = false;
            string queryString = "INSERT INTO Venues (name, address, street, city, state, zip, layoutURL, ownerID) VALUES (@name, @address, @street, @city, @state, @zip, @layoutURL, @ownerID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 50).Value = venue.name;
                command.Parameters.Add("@address", System.Data.SqlDbType.VarChar, 50).Value = venue.address;
                command.Parameters.Add("@street", System.Data.SqlDbType.VarChar, 50).Value = venue.street;
                command.Parameters.Add("@city", System.Data.SqlDbType.VarChar, 50).Value = venue.city;
                command.Parameters.Add("@state", System.Data.SqlDbType.VarChar, 50).Value = venue.state;
                command.Parameters.Add("@zip", System.Data.SqlDbType.VarChar, 50).Value = venue.zipCode;
                command.Parameters.Add("@layoutURL", System.Data.SqlDbType.VarChar, 50).Value = venue.layoutURL;
                command.Parameters.Add("@ownerID", System.Data.SqlDbType.Int).Value = ownerID;

                try
                {
                    connection.Open();
                    int rowsEffected = command.ExecuteNonQuery();

                    if (rowsEffected > 0)
                    {
                        retVal = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return retVal;
        }
    }
}