using MySql.Data.MySqlClient;
using CryptoExchangeAPI.Models;
using System.Collections.Generic;

namespace CryptoExchangeAPI
{
    public class UserService
    {
        private readonly string _connectionString = "server=localhost;database=CryptoExchange;user=root;password=yourpassword;";

        public void CreateUser(User user)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("INSERT INTO Users (Name, Email, PasswordHash) VALUES (@Name, @Email, @PasswordHash)", connection);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

            command.ExecuteNonQuery();
        }

        public User GetUserByEmail(string email)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM Users WHERE Email = @Email", connection);
            command.Parameters.AddWithValue("@Email", email);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Email = reader.GetString("Email"),
                    PasswordHash = reader.GetString("PasswordHash"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                };
            }

            return null;
        }
    }
}
