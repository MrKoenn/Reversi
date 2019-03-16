using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reversi;
using Reversi.Model;
using WebApplication.DAL;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly GameContext _context;

		public HomeController(GameContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var user = StateHelper.GetUserFromCookie(Request);
			if (user == null)
			{
				return RedirectToAction("Index", "Login");
			}
			
			return GameController.GetPlayerAndGame(out var player, out var game, Request, _context) 
				? View(!game.Started ? "Waiting" : "Game") 
				: ShowGameList(player);
		}

		public IActionResult CreateGame()
		{
			var user = StateHelper.GetUserFromCookie(Request);
			if (user == null)
			{
				return RedirectToAction("Index", "Login");
			}

			var player = PlayerManager.GetOrCreatePlayer(user.Username);
			var game = new Game(player);
			GameManager.RememberGame(game);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> JoinGame(int id)
		{
			var user = StateHelper.GetUserFromCookie(Request);
			if (user == null)
			{
				return RedirectToAction("Index", "Login");
			}
			
			var player = PlayerManager.GetOrCreatePlayer(user.Username);
			var game = GameManager.GetGame(id);
			if (game == null)
			{
				return new NotFoundResult();
			}

			game.Start(player);
			await GameManager.SaveGame(game, _context);

			return RedirectToAction(nameof(Index));
		}

		public IActionResult RemoveGame(int id)
		{
			var user = StateHelper.GetUserFromCookie(Request);
			if (user == null)
			{
				return RedirectToAction("Index", "Login");
			}

			var player = PlayerManager.GetOrCreatePlayer(user.Username);

			if ((int)user.Role < (int)UserRole.Moderator)
			{
				ViewData["message"] = "You don't have permission to do this";
				return ShowGameList(player);
			}

			var game = GameManager.GetGame(id);
			if (game == null)
			{
				return new NotFoundResult();
			}

			GameManager.Games.Remove(game);
			ViewData["message"] = "Game removed succesfully";
			return ShowGameList(player);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private IActionResult ShowGameList(Player player)
		{
			ViewData["Player"] = player;
			ViewData["Games"] = GameManager.Games
				.FindAll(g => g.Players[1] == null);
			return View("GameList");
		}
	}
}
