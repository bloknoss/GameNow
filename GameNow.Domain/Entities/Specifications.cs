using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class Specifications
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string MinimumCPU { get; set; }
		public string RecommendedCPU { get; set; }
		public string MinimumGPU { get; set; }
		public string RecommendedGPU { get; set; }
		public string MinimumRAM { get; set; }
		public string RecommendedRAM { get; set; }
		public string RequiredOS { get; set; }

	}
}
