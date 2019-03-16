using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Reversi.Model
{
	public class Game
	{
		[Key]
		public int Id { get; set; }

		[NotMapped]
		public Board Board { get; set; } = new Board();

		public string BoardString
		{
			get => JsonConvert.SerializeObject(Board);
			set => Board = JsonConvert.DeserializeObject<Board>(value);
		}

		public int Turn
		{
			get => Board.Controller.Turn;
			set => Board.Controller.Turn = value;
		}

		[NotMapped]
		public Player[] Players => new[]
		{
			PlayerManager.GetOrCreatePlayer(Player1),
			PlayerManager.GetOrCreatePlayer(Player2)
		};

		public string Player1 { get; set; }
		public string Player2 { get; set; }

		[NotMapped]
		public bool Started { get; set; }

		public Game()
		{
			Started = true;
		}

		public Game(Player player1)
		{
			Player1 = player1.Username;
		}

		public void Start(Player player2)
		{
			Board.Controller.InitBoard();

			Player2 = player2.Username;
			Started = true;
		}
	}
}