using System;
using Newtonsoft.Json;

namespace Reversi.Model
{
	public class Player
	{
		public string Username { get; set; }

		public string Color {
			get {
				var game = GameManager.GetGame(this);
				if (game == null) return null;
				return Array.IndexOf(game.Players, this) == 0 ? "white" : "black";
			}
		}

		[JsonIgnore]
		public string Token { get; set; }

		[JsonIgnore]
		public bool Refresh { get; set; }

		public Player(string username, string token)
		{
			Username = username;
			Token = token;
		}

		public bool Equals(Player player)
		{
			return player.Username == Username;
		}
	}
}
