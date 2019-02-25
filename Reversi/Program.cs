using System;
using Reversi.Controller;
using Reversi.Model;

namespace Reversi
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var players = new[] {
				new Player("White"),
				new Player("Black")
			};

			var board = new BoardController(players[0], players[1], null);
			PrintBoard(board);

			var turn = 0;
			while (true)
			{
				Console.WriteLine($"{players[turn].Name}'s turn");
				var input = Vector.Parse(Console.ReadLine());
				if (input == null)
				{
					Console.WriteLine("Invalid input");
					continue;
				}

				if (board.MakeMove(players[turn], input))
				{
					PrintBoard(board);
				}
				else
				{
					Console.WriteLine("Invalid move");
					continue;
				}

				turn = turn == 0 ? 1 : 0;
			}
		}

		private static void PrintBoard(BoardController board)
		{
			Console.WriteLine(" 01234567");
			for (var y = 0; y < 8; y++)
			{
				Console.Write(y);
				for (var x = 0; x < 8; x++)
				{
					var tile = board.GetTile(new Vector(x, y));
					Console.Write(tile.Owner?.Name[0] ?? '#');
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
	}
}
