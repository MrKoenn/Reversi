using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reversi.Model;
using WebApplication.DAL;

namespace Reversi
{
	public static class GameManager
	{
		public static readonly List<Game> Games = new List<Game>();
		private static bool _loaded;

		public static Game GetGame(Player player, GameContext context)
		{
			if (_loaded || context == null) return Games.Find(game => Array.IndexOf(game.Players, player) != -1);

			foreach (var game in context.Games)
			{
				Games.Add(game);
			}
			_loaded = true;

			return Games.Find(game => Array.IndexOf(game.Players, player) != -1);
		}

		public static Game GetGame(int id)
		{
			return Games.Find(game => game.Id == id);
		}

		public static void RememberGame(Game game)
		{
			Games.Add(game);
		}

		public static async Task SaveGame(Game game, GameContext context)
		{
			await context.Games.AddAsync(game);
			await context.SaveChangesAsync();
		}

		public static async Task UpdateGame(Game game, GameContext context)
		{
			context.Games.Update(game);
			await context.SaveChangesAsync();
		}
	}
}
