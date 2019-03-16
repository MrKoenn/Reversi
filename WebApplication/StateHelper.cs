using System;
using Microsoft.AspNetCore.Http;
using WebApplication.DAL;
using WebApplication.Models;

namespace WebApplication
{
	public static class StateHelper
	{
		public const int ValidTokenDuration = 30;
		public const string UserTokenKey = "UserToken";
		public const int TokenLength = 32;

		public static void SetUserCookie(UserModel user, HttpResponse response)
		{
			response.Cookies.Delete(UserTokenKey);
			var options = new CookieOptions
			{
				Expires = DateTime.Now.AddMinutes(ValidTokenDuration),
				SameSite = SameSiteMode.Lax,
				HttpOnly = false,
				Secure = true
			};
			response.Cookies.Append(UserTokenKey, user.Token, options);
		}

		public static UserModel GetUserFromCookie(HttpRequest request)
		{
			if (!request.Cookies.TryGetValue(UserTokenKey, out var token)) return null;
			var ual = new UserAccessLayer();
			var user = ual.GetUserByToken(token);
			if (user == null) return null;

			var now = DateTime.Now;
			if (now > user.TokenDate)
			{
				return null;
			}

			user.TokenDate = now.AddMinutes(ValidTokenDuration);
			ual.UpdateUser(user);

			return user;
		}

		public static string GenerateUniqueToken()
		{
			var ual = new UserAccessLayer();

			while (true)
			{
				var token = Crypto.GenerateRandomString(TokenLength);
				if (!ual.DoesTokenExist(token))
				{
					return token;
				}
			}
		}
	}
}
