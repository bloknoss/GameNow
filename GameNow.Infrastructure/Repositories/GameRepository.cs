using GameNow.Database;
using GameNow.Domain.Entities;
using GameNow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Infrastructure.Repositories
{
	public class GameRepository : IRepository<Game>
	{
		public GameNowContext _dbContext;

		public GameRepository(GameNowContext gameNowContext)
		{
			_dbContext = gameNowContext;
		}

		public async Task Add(Game entity)
		{
			await _dbContext.AddAsync(entity);
			_dbContext.SaveChanges();
		}

		public void Delete(int Id)
		{
			var game = _dbContext.Games.FirstOrDefault(x => x.Id == Id);
			_dbContext.Remove(Id);
			_dbContext.SaveChanges();
		}
		public void Update(Game entity)
		{
			_dbContext.Update(entity);
			_dbContext.SaveChanges();
		}

		public IEnumerable<Game> GetAll()
		{
			return _dbContext.Games;
		}

		public Game GetById(int Id)
		{
			return _dbContext.Games.FirstOrDefault(e => e.Id == Id);
		}

	}
}
