namespace Reversi.Model
{
	public class Game
	{
		private static int IdCounter;

		public int Id { get; }
		public Board Board { get; private set; }
		public Player[] Players { get; }
		public bool Started { get; private set; }

		public Game(Player player1)
		{
			Id = IdCounter++;
			Players = new[]{player1, null};
		}

		public void Start(Player player2)
		{
			Players[1] = player2;
			Board = new Board(Players[0], Players[1]);
			Started = true;
		}
	}
}
