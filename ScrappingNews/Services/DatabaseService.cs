using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ScrappingNews.Data;
using ScrappingNews.Models;

namespace ScrappingNews.Services;
public class DatabaseService
{
    public async Task InsertAsync(News article)
    {
        using var connection = DbConnection.GetConnection();

        await connection.OpenAsync();

        const string sql = """
            INSERT INTO news
            (
                title,
                url,
                img_url,
                img_alt
            )
            VALUES
            (
                @title,
                @url,
                @image_url,
                @image_alt
            )
            """;

        using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@title", article.Title);
        command.Parameters.AddWithValue("@url", article.Url);
        command.Parameters.AddWithValue("@image_url", article.ImageUrl);
        command.Parameters.AddWithValue("@image_alt", article.ImageAlt);

        await command.ExecuteNonQueryAsync();
    }
}