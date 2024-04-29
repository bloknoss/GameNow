using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Specifications
	{
		public int Id { get; set; }
		public string MinimumCPU { get; set; }
		public string RecommendedCPU { get; set; }
		public string MinimumGPU { get; set; }
		public string RecommendedGPU { get; set; }
		public string MinimumRAM { get; set; }
		public string RecommendedRAM { get; set; }

	}
}
