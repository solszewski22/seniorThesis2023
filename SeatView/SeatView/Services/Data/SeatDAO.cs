using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeatView.Services.Data
{
    // SeatDAO inherits from VenueDAO to reuse the deleteMediaID and deleteSeatByID methods
    public class SeatDAO : VenueDAO
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeatViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        internal List<SeatModel> getSeats(int venueID)
        {
            // method to retrieve a list of all seats for one venue
            // create a list of venues to be returned from the method
            List<SeatModel> returnListOfSeats = new List<SeatModel>();

            string queryString = "SELECT * FROM Seats WHERE venueID = @venueID ORDER BY section, row, seatNum";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@venueID", System.Data.SqlDbType.Int).Value = venueID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SeatModel currSeat = new SeatModel();
                            currSeat.id = reader.GetInt32(0);
                            currSeat.x_coord = reader.GetString(1);
                            currSeat.y_coord = reader.GetString(2);
                            currSeat.section = reader.GetString(3);
                            currSeat.row = reader.GetString(4);
                            currSeat.seatNum = reader.GetString(5);
                            currSeat.venueID = reader.GetInt32(6);
                            currSeat.mediaID = reader.GetInt32(7);

                            returnListOfSeats.Add(currSeat);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return returnListOfSeats;
            }
        }
        internal SeatModel getSeatByID(int seatID)
        {
            // method to retrieve one seat by an id
            string queryString = "SELECT * FROM Seats WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = seatID;

                SeatModel returnSeat = new SeatModel();
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            returnSeat.id = reader.GetInt32(0);
                            returnSeat.section = reader.GetString(3);
                            returnSeat.row = reader.GetString(4);
                            returnSeat.seatNum = reader.GetString(5);
                            returnSeat.mediaID = reader.GetInt32(7);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return returnSeat;
            }
        }        
        internal bool deleteSeat(int seatID)
        {
            // method to delete one seat by given id
            bool retVal = false;

            // get media id that is attached to the seat
            int mediaID = queryMediaId(seatID);
            bool deleteMedia = false;
            if(isMultiUseMedia(mediaID) == 1)
            {
                deleteMedia = true;
            }

            // delete the seat
            if (deleteBySeatID(seatID))
            {
                // is the media id connected to more than one seat? (does it appear in the Seat table more than once)
                if (deleteMedia)
                {
                    // delete the media 
                    deleteByMediaID(mediaID);
                }
                retVal = true;
            }
            return retVal;
        }
        private int queryMediaId(int seatID)
        {
            // method to get the media linked to a specific seat (id)
            int returnID = 0;
            string queryString = "SELECT mediaID FROM Seats WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 50).Value = seatID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            returnID = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return returnID;
        }
        private int isMultiUseMedia(int mediaID)
        {
            // count the number of rows that the mediaID appear in
            int idCount = 0;
            string queryString = "SELECT COUNT(mediaID) AS mediaCount FROM Seats WHERE mediaID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 50).Value = mediaID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idCount = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return idCount;
        }
        internal bool updateSeat(SeatModel seatModel)
        {
            // updates a seat in the Seats table from a seatModel
            bool retVal = false;

            string queryString = "UPDATE Seats SET section = @section, row = @row, seatNum = @seatNum WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@section", System.Data.SqlDbType.VarChar, 50).Value = seatModel.section;
                command.Parameters.Add("@row", System.Data.SqlDbType.VarChar, 50).Value = seatModel.row;
                command.Parameters.Add("@seatNum", System.Data.SqlDbType.VarChar, 50).Value = seatModel.seatNum;
                command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 50).Value = seatModel.id;

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
        internal bool insertSeat(SeatModel seatModel)
        {
            // inserts a new seat given the SeatModel passed in
            bool retVal = false;

            string queryString = "INSERT INTO Seats (xCoord, yCoord, section, row, seatNum, venueID, mediaID) VALUES (@x, @y, @section, @row, @seatNum, @venueId, @mediaId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@x", System.Data.SqlDbType.VarChar, 50).Value = seatModel.x_coord;
                command.Parameters.Add("@y", System.Data.SqlDbType.VarChar, 50).Value = seatModel.y_coord;
                command.Parameters.Add("@section", System.Data.SqlDbType.VarChar, 50).Value = seatModel.section;
                command.Parameters.Add("@row", System.Data.SqlDbType.VarChar, 50).Value = seatModel.row;
                command.Parameters.Add("@seatNum", System.Data.SqlDbType.VarChar, 50).Value = seatModel.seatNum;
                command.Parameters.Add("@venueId", System.Data.SqlDbType.VarChar, 50).Value = seatModel.venueID;
                command.Parameters.Add("@mediaId", System.Data.SqlDbType.VarChar, 50).Value = seatModel.mediaID;

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
        internal List<SeatModel> getSeatsByCoordinates(int x_coord, int y_coord, int venueID)
        {
            List<SeatModel> returnListOfSeats = new List<SeatModel>();

            string queryString = "SELECT * FROM Seats WHERE venueID = @venueID AND xCoord > (@x - 10) AND xCoord < (@x + 10) AND yCoord > (@y - 10) AND yCoord < (@y + 10)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@venueID", System.Data.SqlDbType.Int).Value = venueID;
                command.Parameters.Add("@x", System.Data.SqlDbType.Int).Value = x_coord;
                command.Parameters.Add("@y", System.Data.SqlDbType.Int).Value = y_coord;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SeatModel currSeat = new SeatModel();
                            currSeat.id = reader.GetInt32(0);
                            currSeat.x_coord = reader.GetString(1);
                            currSeat.y_coord = reader.GetString(2);
                            currSeat.section = reader.GetString(3);
                            currSeat.row = reader.GetString(4);
                            currSeat.seatNum = reader.GetString(5);
                            currSeat.venueID = reader.GetInt32(6);
                            currSeat.mediaID = reader.GetInt32(7);

                            returnListOfSeats.Add(currSeat);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return returnListOfSeats;
            }
        }
        internal List<SeatModel> getSeatsBySearch(string searchString)
        {
            string search = '%' + searchString + '%';
            List<SeatModel> returnListOfSeats = new List<SeatModel>();

            string queryString = "SELECT * FROM Seats WHERE section LIKE @search OR row LIKE @search OR seatNum LIKE @search";

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
                            SeatModel currSeat = new SeatModel();
                            currSeat.id = reader.GetInt32(0);
                            currSeat.section = reader.GetString(3);
                            currSeat.row = reader.GetString(4);
                            currSeat.seatNum = reader.GetString(5);
                            
                            returnListOfSeats.Add(currSeat);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return returnListOfSeats;
            }
        }
    }
}