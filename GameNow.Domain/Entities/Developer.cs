using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Developer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Country { get; set; }
		public string Website { get; set; }
		public int GameId { get; set; }
	}
}
