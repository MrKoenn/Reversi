using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Reversi;
using Reversi.Model;
using WebApplication.DAL;

namespace WebApplication.Controllers
{
	[Route("api/game")]
	public class GameController : Controller
	{
		private readonly GameContext _context;

		public GameController(GameContext context)
		{
			_context = context;
		}

		// GET: api/game/board
		[HttpGet("board")]
		public ActionResult GetBoard()
		{
			if (!GetPlayerAndGame(out var player, out var game, Request, _context)) return new ForbidResult();

			player.Refresh = false;
			Logger.Log($"<{player.Username}> GetBoard() => {game.Board}");
			return new OkObjectResult(game.Board);
		}

		// GET: api/game/turn
		[HttpGet("turn")]
		public ActionResult GetTurn()
		{
			if (!GetPlayerAndGame(out var player, out var game, Request, _context)) return new ForbidResult();

			//Logger.Log($"<{player.Username}> GetTurn() => {game.Board.Controller.Turn.Username}#{game.Board.Controller.Turn.Color}");
			return new OkObjectResult(game.Players[game.Board.Controller.Turn]);
		}

		// GET: api/game/self
		[HttpGet("self")]
		public ActionResult GetSelf()
		{
			if (!GetPlayerAndGame(out var player, out var game, Request, _context)) return new ForbidResult();

			Logger.Log($"<{player.Username}> GetSelf() => {player.Color}");
			return new OkObjectResult(player);
		}

		// GET: api/game/refresh
		[HttpGet("refresh")]
		public ActionResult GetNeedsRefresh()
		{
			if (!GetPlayer(out var player, Request, _context)) return new ForbidResult();

			return new OkObjectResult(player.Refresh);
		}

		// POST: api/game/move
		[HttpPost("move")]
		public async Task<ActionResult> MakeMove([FromForm] Vector tile)
		{
			if (!GetPlayerAndGame(out var player, out var game, Request, _context)) return new ForbidResult();
			
			var result = game.Board.Controller.MakeMove(game.Players.IndexOf(player), tile);
			Logger.Log($"<{player.Username}> MakeMove() => {result}");
			if (!result) return new OkObjectResult(false);

			foreach (var gamePlayer in game.Players)
			{
				gamePlayer.Refresh = true;
			}

			await GameManager.UpdateGame(game, _context);
			return new OkObjectResult(true);
		}

		// GET: api/game/score
		[HttpGet("score")]
		public ActionResult GetScore()
		{
			if (!GetPlayerAndGame(out var player, out var game, Request, _context)) return new ForbidResult();

			var score = game.Board.Controller.GetScore();
			Logger.Log($"<{player.Username}> GetScore() => {string.Join(",", score)}");
			return new OkObjectResult(score);
		}

		// GET: api/game/score
		[HttpGet("moves")]
		public ActionResult GetMoves()
		{
			if (!GetPlayerAndGame(out var player, out var game, Request, _context)) return new ForbidResult();

			var moves = game.Board.Controller.GetPossibleMoves(game.Players.IndexOf(player));
			Logger.Log($"<{player.Username}> GetMoves() => {string.Join(",", moves)}");
			return new OkObjectResult(moves);
		}

		public static bool GetPlayer(out Player player, HttpRequest request, GameContext context)
		{
			player = null;

			// Try to get the user from the cookie
			var user = StateHelper.GetUserFromCookie(request);
			if (user == null) return false;

			// Check if we have a player that matches the user in memory
			player = PlayerManager.GetOrCreatePlayer(user.Username);
			return true;
		}

		public static bool GetPlayerAndGame(out Player player, out Game game, HttpRequest request, GameContext context)
		{
			if (!GetPlayer(out player, request, context))
			{
				game = null;
				return false;
			}

			game = GameManager.GetGame(player, context);
			return game != null;
		}
	}
}