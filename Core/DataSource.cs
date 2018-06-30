using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using Google.Maps;

namespace Core
{
    class DataSource
    {
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["Images"].ConnectionString;

        internal static Image GetInitialImage()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = @"SELECT ImageBinary FROM ApplicationImages
                    WHERE ImageName = 'StartupImage'";

                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;

                var result = command.ExecuteScalar();

                return Utils.BytesArrayToBitmap((byte[])result);
            }
        }

        internal static Image GetCachedImage(string text, int zoom, MapTypes mapType)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = @"SELECT ImageBinary FROM CachedImages
                    WHERE SearchText = @SearchText
                        AND Zoom = @Zoom
                        AND MapType = @MapType";

                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                var searchTextParam = new SqlParameter("@SearchText", System.Data.SqlDbType.NVarChar, 255)
                {
                    Value = text
                };
                var zoomParam = new SqlParameter("@Zoom", System.Data.SqlDbType.Int)
                {
                    Value = zoom
                };
                var mapTypeParam = new SqlParameter("@MapType", System.Data.SqlDbType.Int)
                {
                    Value = (int)mapType
                };

                command.Parameters.Add(searchTextParam);
                command.Parameters.Add(zoomParam);
                command.Parameters.Add(mapTypeParam);

                var result = command.ExecuteScalar();

                if (result == null)
                {
                    return null;
                }

                return Utils.BytesArrayToBitmap((byte[])result);
            }
        }

        internal static void CacheImage(string text, int zoom, MapTypes mapType, byte[] byteArray)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = @"IF NOT EXISTS
                                (SELECT 1 FROM CachedImages
                                WHERE SearchText = @SearchText
                                    AND Zoom = @Zoom
                                    AND MapType = @MapType)
                            BEGIN
                                INSERT INTO CachedImages (SearchText, Zoom, MapType, ImageBinary)
                                VALUES (@SearchText, @Zoom, @MapType, @ImageBinary)
                            END";

                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                var searchTextParam = new SqlParameter("@SearchText", System.Data.SqlDbType.NVarChar, 255)
                {
                    Value = text
                };
                var zoomParam = new SqlParameter("@Zoom", System.Data.SqlDbType.Int)
                {
                    Value = zoom
                };
                var mapTypeParam = new SqlParameter("@MapType", System.Data.SqlDbType.Int)
                {
                    Value = (int)mapType
                };
                var imageBinaryParam = new SqlParameter("@ImageBinary", System.Data.SqlDbType.VarBinary)
                {
                    Value = byteArray
                };

                command.Parameters.Add(searchTextParam);
                command.Parameters.Add(zoomParam);
                command.Parameters.Add(mapTypeParam);
                command.Parameters.Add(imageBinaryParam);

                var result = command.ExecuteNonQuery();
            }
        }
    }
}
