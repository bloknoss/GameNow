using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Bundle
	{
		public int Id { get; set; }
		public int GameId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public int FinalPrice { get; set; }
		public int BundlePrice { get; set; }
	}
}
