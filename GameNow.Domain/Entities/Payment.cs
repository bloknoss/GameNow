using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Payment
	{
		public int Id { get; set; }
		public string Method { get; set; }
		public int Amount { get; set; }
		public DateTime Date { get; set; }
		public int UserId { get; set; }
		public int StatusId { get; set; }

	}
}
