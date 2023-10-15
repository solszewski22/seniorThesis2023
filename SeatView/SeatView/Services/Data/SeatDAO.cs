using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeatView.Services.Data
{
    public class SeatDAO
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeatViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // method to retrieve a list of all seats for one venue
        internal List<SeatModel> getSeats(int venueID)
        {
            // create a list of venues to be returned from the method
            List<SeatModel> returnListOfSeats = new List<SeatModel>();

            string queryString = "SELECT * FROM Seats WHERE venueID = @venueID";

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
    }
}