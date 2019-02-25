using Newtonsoft.Json;

namespace Reversi.Model
{
	public class Tile : ILocatable
	{
		public Vector Location { get; }

		[JsonIgnore]
		public Player Owner { get; set; }

		public int OwnerId => Owner?.Id ?? -1;

		public Tile(Vector location)
		{
			Location = location;
		}
	}
}
