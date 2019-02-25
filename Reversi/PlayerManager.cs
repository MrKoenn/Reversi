using System;
using System.Collections.Generic;
using System.Text;
using Reversi.Model;

namespace Reversi
{
	public class PlayerManager
	{
		private static readonly List<Player> Players = new List<Player>();

		public static Player GetPlayer(int id)
		{
			return Players.Find(player => player.Id == id);
		}

		public static Player CreatePlayer(Player player)
		{
			Players.Add(player);
			return player;
		}
	}
}
