using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.DAL
{
	public class UserAccessLayer
	{
		private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Reversi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		public UserModel GetUserByToken(string token)
		{
			Logger.Log($"GetUserByToken()");

			var user = new UserModel();
			using (var connection = new SqlConnection(ConnectionString))
			{
				const string query = "SELECT Username, PasswordHash, HashSalt, Token, TokenDate, LoginAttempts, Role " +
				                     "FROM [User] " +
									 "WHERE Token = @Token";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Token", token);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					user.Username = reader["Username"].ToString();
					user.PasswordHash = (byte[]) reader["PasswordHash"];
					user.HashSalt = reader["HashSalt"].ToString();
					user.Token = reader["Token"].ToString();
					user.TokenDate = DateTime.Parse(reader["TokenDate"].ToString());
					user.LoginAttempts = Convert.ToInt32(reader["LoginAttempts"]);
					user.Role = Enum.TryParse(reader["Role"].ToString(), out UserRole role) ? role : UserRole.User;
				}

				connection.Close();
			}

			return user.IsValid() ? user : null;
		}

		public async Task<UserModel> GetUserByUsername(string username)
		{
			Logger.Log($"GetUserByUsername()");

			var user = new UserModel();
			using (var connection = new SqlConnection(ConnectionString))
			{
				const string query = "SELECT Username, PasswordHash, HashSalt, Token, TokenDate, LoginAttempts, Role " +
				                     "FROM [User] " +
									 "WHERE Username = @Username";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Username", username);

				await connection.OpenAsync();
				var reader = await command.ExecuteReaderAsync();

				while (await reader.ReadAsync())
				{
					user.Username = reader["Username"].ToString();
					user.PasswordHash = (byte[]) reader["PasswordHash"];
					user.HashSalt = reader["HashSalt"].ToString();
					user.Token = reader["Token"].ToString();
					user.TokenDate = DateTime.Parse(reader["TokenDate"].ToString());
					user.LoginAttempts = Convert.ToInt32(reader["LoginAttempts"]);
					user.Role = Enum.TryParse(reader["Role"].ToString(), out UserRole role) ? role : UserRole.User;
				}

				connection.Close();
			}

			return user.IsValid() ? user : null;
		}

		public async Task<bool> AddUser(UserModel user)
		{
			Logger.Log($"AddUser()");

			int result;
			using (var connection = new SqlConnection(ConnectionString))
			{
				const string query = "INSERT INTO [User] " +
									 "VALUES (@Username, @PasswordHash, @HashSalt, @Token, @TokenDate, 0, @Role)";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Username", user.Username);
				command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
				command.Parameters.AddWithValue("@HashSalt", user.HashSalt);
				command.Parameters.AddWithValue("@Token", user.Token);
				command.Parameters.AddWithValue("@TokenDate", user.TokenDate);
				command.Parameters.AddWithValue("@Role", Enum.GetName(typeof(UserRole), user.Role));

				await connection.OpenAsync();
				result = await command.ExecuteNonQueryAsync();
				connection.Close();
			}

			return result == 1;
		}

		public async Task<bool> UpdateUser(UserModel user)
		{
			Logger.Log($"UpdateUser()");

			int result;
			using (var connection = new SqlConnection(ConnectionString))
			{
				const string query = "UPDATE [User] " +
				                     "SET " +
				                     "	PasswordHash = @PasswordHash, " +
				                     "	HashSalt = @HashSalt, " +
				                     "	Token = @Token, " +
				                     "	TokenDate = @TokenDate, " +
				                     "	LoginAttempts = @LoginAttempts " +
				                     "WHERE Username = @Username";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
				command.Parameters.AddWithValue("@HashSalt", user.HashSalt);
				command.Parameters.AddWithValue("@Token", user.Token);
				command.Parameters.AddWithValue("@TokenDate", user.TokenDate);
				command.Parameters.AddWithValue("@LoginAttempts", user.LoginAttempts);
				command.Parameters.AddWithValue("@Username", user.Username);

				await connection.OpenAsync();
				result = await command.ExecuteNonQueryAsync();
				connection.Close();
			}

			return result == 1;
		}

		public bool DoesTokenExist(string token)
		{
			Logger.Log($"DoesTokenExist()");

			string found = null;
			using (var connection = new SqlConnection(ConnectionString))
			{
				const string query = "SELECT Username, PasswordHash, HashSalt, Token, TokenDate " +
				                     "FROM [User] " +
				                     "WHERE Token = @Token";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Token", token);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					found = reader["Token"].ToString();
				}

				connection.Close();
			}

			return found != null;
		}
	}
}
