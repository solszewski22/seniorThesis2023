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

        internal ImageModel getMediaByID(int mediaID)
        {
            // method to retrieve one media from the Media table
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
        internal bool updateMedia(ImageModel imgModel)
        {
            // update the media table with new information
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
        internal int insertMedia(ImageModel imgModel)
        {
            // insert an image into the database Media table and returned the new row's id
            Int32 retValID = 0;

            string queryString = "INSERT INTO MEDIA (imgUrl) VALUES (@url);" + "SELECT CAST(scope_identity() AS int);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@url", System.Data.SqlDbType.VarChar, 50).Value = imgModel.mediaURL;

                try
                {
                    connection.Open();
                    // get the row id of the insert returned from the query
                    Int32 latestRowId = (Int32)command.ExecuteScalar();

                    // is the row id is a valid id, return it from the function
                    if (latestRowId > 0)
                    {
                        retValID = latestRowId;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return retValID;
        }
    }
}