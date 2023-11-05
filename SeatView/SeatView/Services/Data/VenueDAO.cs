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
        internal List<VenueModel> getAllVenues()
        {
            // create a list of venues to be returned from the method
            List<VenueModel> returnListOfVenues = new List<VenueModel>();

            string queryString = "SELECT * FROM Venues";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

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
        internal VenueModel getVenueByID(int venueID)
        {
            // get a specific venue based on the given id
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
        internal List<VenueModel> getVenueBySearch(string searchString)
        {
            string search = '%' + searchString + '%';
            List<VenueModel> returnListOfVenues = new List<VenueModel>();

            string queryString = "SELECT * FROM Venues WHERE street LIKE @search OR name LIKE @search OR address LIKE @search OR city LIKE @search OR state LIKE @search OR zip LIKE @search";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@search", System.Data.SqlDbType.VarChar).Value = search;

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
        internal bool addOrUpdateVenue(VenueModel venue, int ownerID)
        {
            // add a venue to the database
            bool retVal = false;
            string queryString = "";

            if (venue.id <= 0)
            {
                // insert venue
                queryString = "INSERT INTO Venues (name, address, street, city, state, zip, layoutURL, ownerID) VALUES (@name, @address, @street, @city, @state, @zip, @layoutURL, @ownerID)";
            }
            else
            {
                // update venue
                queryString = "UPDATE Venues SET name = @name, address = @address, street = @street, city = @city, state = @state, zip = @zip, layoutURL = @layoutURL WHERE id = @id;";
            }

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

                if (venue.id <= 0)
                {
                    // insert -- associate with an owner
                    command.Parameters.Add("@ownerID", System.Data.SqlDbType.Int).Value = ownerID;
                }
                else
                {
                    // update -- associate with an exisiting id
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = venue.id;
                }

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
        internal bool deleteVenue(int id)
        {
            // delete a venue in the database
            bool retVal = false;

            // gather media IDs
            List<int> mediaIDs;
            mediaIDs = queryMediaIDs(id);

            // gather the seat IDs and 
            List<int> seatIDs;
            seatIDs = querySeatIDs(id);

            // delete the seats that are linked to this venue (id)
            for (int i = 0; i < seatIDs.Count(); i++)
            {
                deleteBySeatID(seatIDs[i]);
            }

            // not deleting the media in the database

            // delete the media that is linked to the seats at this venue (id)
            for (int i = 0; i < mediaIDs.Count(); i++)
            {
                deleteByMediaID(mediaIDs[i]);
            }

            // delete venue
            string queryString = "DELETE FROM Venues WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 50).Value = id;

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
        private List<int> queryMediaIDs(int venueID)
        {
            // get a list of media ids to delete that are related to the seats in a venue to be deleted
            List<int> returnIDs = new List<int>();
            string queryString = "SELECT Media.id FROM Seats, Media WHERE Seats.mediaID = Media.id AND Seats.venueID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 50).Value = venueID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int currID = 0;
                            currID = reader.GetInt32(0);
                            returnIDs.Add(currID);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return returnIDs;
        }
        private List<int> querySeatIDs (int venueID)
        {
            // get a list of seat ids to delete that are related to a venue to be deleted
            List<int> returnIDs = new List<int>();
            string queryString = "SELECT Seats.id  FROM Seats WHERE Seats.venueID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 50).Value = venueID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int currID = 0;
                            currID = reader.GetInt32(0);
                            returnIDs.Add(currID);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return returnIDs;
        }
        public bool deleteByMediaID(int id)
        {
            // loop through media ids and call a sql query to delete
            bool retVal = false;
            string queryString = "DELETE FROM Media WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

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
        public bool deleteBySeatID(int id)
        {
            // loop through seat ids and call a sql query to delete
            bool retVal = false;
            string queryString = "DELETE FROM Seats WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

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