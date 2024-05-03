using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Review
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int GameId { get; set; }
		public string Content { get; set; }
		public int Upvotes { get; set; }
		public int Downvotes { get; set; }
		public DateTime PostedTime { get; set; }
		public DateTime LastEdit { get; set; }

		[ForeignKey("GameId")]
		public virtual Game Game { get; set; }
	}
}