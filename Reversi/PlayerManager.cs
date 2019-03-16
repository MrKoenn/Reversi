using System.Collections.Generic;
using Reversi.Model;

namespace Reversi
{
	public static class PlayerManager
	{
		private static readonly List<Player> Players = new List<Player>();

		public static Player GetPlayer(string token)
		{
			return Players.Find(player => player.Token == token);
		}

		public static Player GetPlayerByUsername(string username)
		{
			return Players.Find(player => player.Username == username);
		}

		public static void RememberPlayer(Player player)
		{
			Players.Add(player);
		}
	}
}
