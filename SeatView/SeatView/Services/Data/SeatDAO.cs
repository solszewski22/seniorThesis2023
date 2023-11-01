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
        // method to retrieve a list of all seats for one venue
        internal List<SeatModel> getSeats(int venueID)
        {
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

        // method to retrieve one seat by an id
        internal SeatModel getSeatByID(int seatID)
        {
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

        // method to delete one seat by given id
        internal bool deleteSeat(int seatID)
        {
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

        // method to get the media linked to a specific seat (id)
        private int queryMediaId(int seatID)
        {
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

        // count the number of rows that the mediaID appear in
        private int isMultiUseMedia(int mediaID)
        {
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

        // updates a seat in the Seats table from a seatModel
        internal bool updateSeat(SeatModel seatModel)
        {
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

        // inserts a new seat given the SeatModel passed in
        internal bool insertSeat(SeatModel seatModel)
        {
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
    }
}