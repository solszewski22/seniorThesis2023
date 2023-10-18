using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeatView.Services.Data
{
    public class MediaDAO
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeatViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // method to retrieve one media from the Media table
        internal ImageModel getMediaByID(int mediaID)
        {
            string queryString = "SELECT * FROM Media WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = mediaID;

                ImageModel returnMedia = new ImageModel();
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            returnMedia.id = reader.GetInt32(0);
                            returnMedia.mediaURL = reader.GetString(1);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return returnMedia;
            }
        }
        
        // update the media table with new information
        internal bool updateMedia(ImageModel imgModel)
        {
            bool retVal = false;

            string queryString = "UPDATE Media SET imgURL = @url WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@url", System.Data.SqlDbType.VarChar, 50).Value = imgModel.mediaURL;
                command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 50).Value = imgModel.id;

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