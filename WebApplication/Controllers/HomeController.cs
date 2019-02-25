using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View("Test");
		}

		public IActionResult Player1()
		{
			Response.Cookies.Delete("PlayerId");
			var options = new CookieOptions
			{
				Expires = DateTime.Now.AddDays(1),
				SameSite = SameSiteMode.None
			};
			Response.Cookies.Append("PlayerId", "0", options);

			return RedirectToAction("Index");
		}

		public IActionResult Player2()
		{
			Response.Cookies.Delete("PlayerId");
			var options = new CookieOptions
			{
				Expires = DateTime.Now.AddDays(1),
				SameSite = SameSiteMode.None
			};
			Response.Cookies.Append("PlayerId", "1", options);

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
