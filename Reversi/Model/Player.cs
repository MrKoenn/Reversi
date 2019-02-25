using Newtonsoft.Json;

namespace Reversi.Model
{
	public class Player
	{
		private static int _idCounter;

		public int Id { get; }
		public string Name { get; }

		[JsonIgnore]
		public bool Refresh { get; set; }

		public Player(string name)
		{
			Id = _idCounter++;
			Name = name;
		}

		public bool Equals(Player player)
		{
			return player.Id == Id;
		}
	}
}
