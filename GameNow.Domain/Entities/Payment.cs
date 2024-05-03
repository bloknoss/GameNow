using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Payment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Method { get; set; }
		public int Amount { get; set; }
		public DateTime Date { get; set; }
		public String UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }



	}
}
