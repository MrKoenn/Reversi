using System;
using System.Collections.Generic;
using Reversi.Model;

namespace Reversi
{
	public static class GameManager
	{
		public static readonly List<Game> Games = new List<Game>();

		public static Game GetGame(Player player)
		{
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

		public static void ForgetGame(Game game)
		{
			Games.Remove(game);
		}
	}
}
