using Microsoft.EntityFrameworkCore;
using Reversi.Model;

namespace WebApplication.DAL
{
	public class GameContext : DbContext
	{
		public GameContext(DbContextOptions options) : base(options) { }

		public DbSet<Game> Games { get; set; }
	}
}