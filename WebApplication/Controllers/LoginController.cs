using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DAL;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	public class LoginController : Controller
	{
		// GET: Login
		public ActionResult Index()
		{
			var user = StateHelper.GetUserFromCookie(Request);
			if (user == null)
			{
				return View("Login");
			}

			StateHelper.SetUserCookie(user, Response);
			return RedirectToAction("Index", "Home");
		}

		// POST: Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Index(RegisterFormModel userInput)
		{
			if (StateHelper.GetUserFromCookie(Request) != null) return RedirectToAction(nameof(Index));

			if (!ModelState.IsValid || !userInput.IsValid()) return View("Login");

			try
			{
				var ual = new UserAccessLayer();
				var user = await ual.GetUserByUsername(userInput.Username);
				if (user == null)
				{
					ViewData["Error"] = "Invalid username or password.";
					return View("Login");
				}

				if (user.LoginAttempts > 3)
				{
					ViewData["Error"] = "Account has been locked.";
					return View("Login");
				}

				var passwordHash = Crypto.CalculateArgon2Hash(userInput.Password, user.HashSalt);
				if (!Crypto.SecureCompareByteArrays(passwordHash, user.PasswordHash))
				{
					ViewData["Error"] = "Invalid username or password.";
					user.LoginAttempts++;
					await ual.UpdateUser(user);
					return View("Login");
				}

				user.Token = StateHelper.GenerateUniqueToken();
				user.TokenDate = DateTime.Now.AddMinutes(StateHelper.ValidTokenDuration);
				user.LoginAttempts = 0;
				await ual.UpdateUser(user);

				StateHelper.SetUserCookie(user, Response);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ViewData["Error"] = "An unknown error has occured.";
				return View("Login");
			}
		}

		// GET: Login/Register
		public ActionResult Register()
		{
			if (StateHelper.GetUserFromCookie(Request) != null) return RedirectToAction(nameof(Index));

			return View();
		}

		// POST: Login/Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterFormModel userInput)
		{
			if (StateHelper.GetUserFromCookie(Request) != null) return RedirectToAction(nameof(Index));
			if (!ModelState.IsValid || !userInput.IsValid()) return View();

			try
			{
				var ual = new UserAccessLayer();
				if (await ual.GetUserByUsername(userInput.Username) != null)
				{
					ViewData["Error"] = "A user with that username already exists";
					return View();
				}

				if (userInput.Username.Length < 3)
				{
					ViewData["Error"] = "Your username must have at least 3 characters";
					return View();
				}

				if (!Regex.IsMatch(userInput.Password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{10,}$"))
				{
					ViewData["Error"] =
						"Your password must contain at least 1 number, 1 uppercase letter, 1 lowercase letter, 1 special character and must be at least 10 characters long.";
					return View();
				}

				if (userInput.Password.Length > 128)
				{
					ViewData["Error"] = "Your password cannot be longer than 128 characters";
					return View();
				}

				var token = StateHelper.GenerateUniqueToken();
				var hashSalt = Crypto.GenerateRandomString(64);
				var passwordHash = Crypto.CalculateArgon2Hash(userInput.Password, hashSalt);
				var user = new UserModel
				{
					Username = userInput.Username,
					HashSalt = hashSalt,
					PasswordHash = passwordHash,
					Token = token,
					TokenDate = DateTime.Now.AddMinutes(StateHelper.ValidTokenDuration),
					Role = UserRole.User
				};

				if (!await ual.AddUser(user)) return View();

				StateHelper.SetUserCookie(user, Response);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ViewData["Error"] = "An unknown error has occured.";
				return View();
			}
		}

		// POST: Login/Logout
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Logout()
		{
			var user = StateHelper.GetUserFromCookie(Request);
			if (user == null)
			{
				return View("Login");
			}

			var ual = new UserAccessLayer();
			user.TokenDate = DateTime.Now;
			await ual.UpdateUser(user);

			return View("Login");
		}
	}
}