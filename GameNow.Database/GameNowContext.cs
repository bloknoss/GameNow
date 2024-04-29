using Microsoft.EntityFrameworkCore;

namespace GameNow.Database
{
	public class GameNowContext : DbContext
	{
        public GameNowContext(DbContextOptions<GameNowContext> options) : base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}

	
}