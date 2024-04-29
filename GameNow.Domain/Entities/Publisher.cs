using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Publisher
	{
		public int Id { get; set; }
		public int AllGames { get; set; }
		public int GamesReleased { get; set; }
		public int DLCsReleased { get; set; }
		public string About { get; set; }

	}
}
