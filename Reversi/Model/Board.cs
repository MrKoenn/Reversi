using System;
using System.Collections.Generic;
using System.Text;
using Reversi.Controller;

namespace Reversi.Model
{
	public class Board
	{
		public List<Tile> Tiles { get; } = new List<Tile>();

		[NonSerialized]
		public BoardController Controller;

		public Board(Player player1, Player player2)
		{
			Controller = new BoardController(player1, player2, this);
		}
	}
}
