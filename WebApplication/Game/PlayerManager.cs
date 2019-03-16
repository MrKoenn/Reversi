using System.Collections.Generic;
using Reversi.Model;
using WebApplication.DAL;

namespace Reversi
{
	public static class PlayerManager
	{
		private static readonly List<Player> Players = new List<Player>();

		public static Player GetOrCreatePlayer(string username)
		{
			if (username == null) return null;

			var result = Players.Find(player => player.Username == username);
			if (result != null) return result;

			result = new Player(username);
			Players.Add(result);
			return result;
		}
	}
}
