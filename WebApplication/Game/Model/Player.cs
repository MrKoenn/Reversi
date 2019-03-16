using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Reversi.Model
{
	public class Player
	{
		[Key]
		[JsonIgnore]
		public int Id { get; set; }

		public string Username { get; set; }

		[NotMapped]
		public string Color {
			get {
				var game = GameManager.GetGame(this, null);
				if (game == null) return null;
				return Array.IndexOf(game.Players, this) == 0 ? "white" : "black";
			}
		}

		[NotMapped]
		[JsonIgnore]
		public bool Refresh { get; set; }

		public Player()
		{
		}

		public Player(string username)
		{
			Username = username;
		}

		public bool Equals(Player player)
		{
			return player.Username == Username;
		}
	}
}
