using System;
using Newtonsoft.Json;

namespace Reversi.Model
{
	public class Tile : ILocatable
	{
		public Vector Location { get; }

		[JsonIgnore]
		public Player Owner { get; set; }

		public string Color {
			get
			{
				if (Owner == null) return "hidden";
				var game = GameManager.GetGame(Owner);
				if (game == null) return null;
				return Array.IndexOf(game.Players, Owner) == 0 ? "white" : "black";
			}
		}

		public Tile(Vector location)
		{
			Location = location;
		}
	}
}
