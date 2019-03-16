using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Reversi.Model
{
	public class Tile : ILocatable
	{
		public Vector Location { get; set; }
		
		public int Owner { get; set; } = -1;
		
		public string Color => Owner == -1 ? "hidden" : Owner == 0 ? "white" : "black";

		public Tile()
		{
		}

		public Tile(Vector location)
		{
			Location = location;
		}
	}
}
