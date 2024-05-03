using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Domain.Entities
{
	public class User : IdentityUser
	{
		public string? Address { get; set; }
		public string? PhoneNumber { get; set; }
		public int? GamesPlayed { get; set; }
		public int? PlaytimeForever { get; set; }
		public int? RecentPlaytime { get; set; }

	}
}
