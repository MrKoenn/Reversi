using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Reversi.Controller;

namespace Reversi.Model
{
	public class Board
	{
		private List<Tile> _tiles = new List<Tile>();

		public List<Tile> Tiles
		{
			get
			{
				if (_tiles.Count > 64)
				{
					_tiles.RemoveRange(64, _tiles.Count - 64);
				}

				return _tiles;
			}
			set
			{
				_tiles = value;
				if (_tiles.Count > 64)
				{
					_tiles.RemoveRange(64, _tiles.Count - 64);
				}
			}
		}

		[JsonIgnore]
		public BoardController Controller { get; }

		public Board()
		{
			Controller = new BoardController(this);
		}
	}
}