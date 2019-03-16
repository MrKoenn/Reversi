using System.Collections.Generic;
using Newtonsoft.Json;
using Reversi.Model;

namespace Reversi.Controller
{
	public class BoardController
	{
		public Board Board { get; }

		private int _turn;
		public Player Turn => Players[_turn];

		[JsonIgnore]
		public Player[] Players { get; }

		public BoardController(Player player1, Player player2, Board board)
		{
			Board = board;
			Players = new[] {player1, player2};

			for (var x = 0; x < 8; x++)
			{
				for (var y = 0; y < 8; y++)
				{
					Board.Tiles.Add(new Tile(new Vector(x, y)));
				}
			}

			GetTile(new Vector(3, 3)).Owner = player1;
			GetTile(new Vector(4, 4)).Owner = player1;
			GetTile(new Vector(4, 3)).Owner = player2;
			GetTile(new Vector(3, 4)).Owner = player2;
		}

		public Tile GetTile(Vector location)
		{
			return Board.Tiles.Find(tile => tile.Location.Equals(location));
		}

		public bool MakeMove(Player player, Vector location)
		{
			if (!Turn.Equals(player))
			{
				return false;
			}

			if (!IsInBounds(location) || GetTile(location).Owner != null)
			{
				return false;
			}

			var possible = false;
			foreach (var direction in Vector.Directions)
			{
				var line = RayCast(location, direction, player);
				if (line == null) continue;
				possible = true;
				foreach (var tile in line)
				{
					tile.Owner = player;
				}
			}

			if (!possible) return false;

			GetTile(location).Owner = player;
			_turn = _turn == 0 ? 1 : 0;

			foreach (var p in Players)
			{
				p.Refresh = true;
			}

			return true;
		}

		public int[] GetScore()
		{
			var score = new int[Players.Length];
			foreach (var tile in Board.Tiles)
			{
				if (tile.Owner == null) continue;

				for (var i = 0; i < Players.Length; i++)
				{
					if (tile.Owner.Equals(Players[i]))
					{
						score[i]++;
					}
				}
			}

			return score;
		}

		public ICollection<Tile> RayCast(Vector origin, Vector direction, Player caster)
		{
			var hit = new List<Tile>();
			var location = origin.Clone();
			while (IsInBounds(location))
			{
				location.Add(direction);
				if (!IsInBounds(location))
				{
					return null;
				}

				var tile = GetTile(location);
				if (tile.Owner == null)
				{
					return null;
				}

				if (tile.Owner.Equals(caster))
				{
					return hit.Count > 0 ? hit : null;
				}

				hit.Add(tile);
			}

			return null;
		}

		public bool IsInBounds(Vector location)
		{
			return location.X >= 0 && location.Y >= 0 && location.X < 8 && location.Y < 8;
		}
	}
}