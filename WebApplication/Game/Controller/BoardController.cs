using System.Collections.Generic;
using Reversi.Model;

namespace Reversi.Controller
{
	public class BoardController
	{
		public Board Board { get; }

		public int Turn;

		public BoardController(Board board)
		{
			Board = board;
		}

		public void InitBoard()
		{
			for (var x = 0; x < 8; x++)
			{
				for (var y = 0; y < 8; y++)
				{
					Board.Tiles.Add(new Tile(new Vector(x, y)));
				}
			}

			GetTile(new Vector(3, 3)).Owner = 0;
			GetTile(new Vector(4, 4)).Owner = 0;
			GetTile(new Vector(4, 3)).Owner = 1;
			GetTile(new Vector(3, 4)).Owner = 1;
		}

		public Tile GetTile(Vector location)
		{
			return Board.Tiles.Find(tile => tile.Location.Equals(location));
		}

		public bool MakeMove(int player, Vector location)
		{
			if (!Turn.Equals(player))
			{
				return false;
			}

			if (!IsInBounds(location) || GetTile(location).Owner != -1)
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
			Turn = Turn == 0 ? 1 : 0;

			return true;
		}

		public int[] GetScore()
		{
			var score = new int[2];
			foreach (var tile in Board.Tiles)
			{
				if (tile.Owner == -1) continue;

				for (var i = 0; i < 2; i++)
				{
					if (tile.Owner.Equals(i))
					{
						score[i]++;
					}
				}
			}

			return score;
		}

		public ICollection<Tile> RayCast(Vector origin, Vector direction, int caster)
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
				if (tile.Owner == -1)
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

		public ICollection<Tile> GetPossibleMoves(int player)
		{
			var tiles = new List<Tile>();
			foreach (var tile in Board.Tiles)
			{
				if (tile.Owner != -1) continue;

				var location = tile.Location;
				var directions = new List<Vector>();
				foreach (var direction in Vector.Directions)
				{
					var neighborLocation = location.Clone();
					neighborLocation.Add(direction);
					if (!IsInBounds(neighborLocation)) continue;
					var neighbor = GetTile(neighborLocation);
					if (neighbor.Owner == -1) continue;
					directions.Add(direction);
				}

				foreach (var direction in directions)
				{
					var line = RayCast(location, direction, player);
					if (line == null) continue;
					tiles.Add(tile);
					break;
				}
			}

			return tiles;
		}

		public bool IsInBounds(Vector location)
		{
			return location.X >= 0 && location.Y >= 0 && location.X < 8 && location.Y < 8;
		}
	}
}