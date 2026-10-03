using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrappingNews.Data
{
    internal class DbConnection
    {
        private const string ConnectionString =
        "Server=nebulon;Database=db_news;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
