using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Review
	{
		public int Id { get; set; }
		public string GameId { get; set; }
		public string Content { get; set; }
		public int Upvotes { get; set; }
		public int Downvotes { get; set; }
		public DateTime PostedTime { get; set; }
		public DateTime LastEdit { get; set; }
	}
}
