using System;

namespace WebApplication.Models
{
	public class UserModel
	{
		public string Username { get; set; }
		public byte[] PasswordHash { get; set; }
		public string HashSalt { get; set; }
		public string Token { get; set; }
		public DateTime TokenDate { get; set; }
		public int LoginAttempts { get; set; }
		public UserRole Role { get; set; }

		public bool IsValid()
		{
			return Username != null &&
			       PasswordHash != null &&
			       HashSalt != null &&
			       Token != null;
		}
	}

	public enum UserRole
	{
		User, Moderator, Admin
	}
}