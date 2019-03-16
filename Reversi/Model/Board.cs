using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Reversi.Controller;

namespace Reversi.Model
{
	public class Board
	{
		public List<Tile> Tiles { get; } = new List<Tile>();

		[JsonIgnore]
		public BoardController Controller { get; }

		public Board(Player player1, Player player2)
		{
			Controller = new BoardController(player1, player2, this);
		}
	}
}
