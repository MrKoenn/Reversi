using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Reversi;
using Reversi.Model;

namespace WebApplication.Controllers
{
	[Route("api/[controller]")]
	public class GameController : Controller
	{
		public static readonly Board Board = new Board(
			PlayerManager.CreatePlayer(new Player("White")),
			PlayerManager.CreatePlayer(new Player("Black"))
		);

		// GET: api/game/board
		[HttpGet("board")]
		public ActionResult GetBoard()
		{
			var player = GetPlayerFromCookie();
			if (player != null)
			{
				player.Refresh = false;
			}

			return new OkObjectResult(Board);
		}

		// GET: api/game/turn
		[HttpGet("turn")]
		public Player GetTurn()
		{
			return Board.Controller.Turn;
		}

		// GET: api/game/self
		[HttpGet("self")]
		public ActionResult GetSelf()
		{
			var player = GetPlayerFromCookie();
			if (player == null) return new ForbidResult();

			return new OkObjectResult(player);
		}

		// GET: api/game/refresh
		[HttpGet("refresh")]
		public ActionResult GetNeedsRefresh()
		{
			var player = GetPlayerFromCookie();
			if (player == null) return new ForbidResult();

			return new OkObjectResult(player.Refresh);
		}

		// POST: api/game/move
		[HttpPost("move")]
		public ActionResult MakeMove(HttpRequestMessage request, [FromForm] Vector tile)
		{
			var player = GetPlayerFromCookie();
			if (player == null) return new ForbidResult();

			var result = Board.Controller.MakeMove(player, tile);
			return new OkObjectResult(result);
		}

		private Player GetPlayerFromCookie()
		{
			if (!Request.Cookies.TryGetValue("PlayerId", out var value)) return null;
			return int.TryParse(value, out var id) ? PlayerManager.GetPlayer(id) : null;
		}
	}
}