using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Game
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public bool EarlyAcess { get; set; }
		public int RecentReviews { get; set; }
		public int RecentRating { get; set; }
		public int AllReviews { get; set; }
		public int HistoricalRating { get; set; }
		public int Achievements { get; set; }
		public DateTime ReleaseDate { get; set; }

	}
}
