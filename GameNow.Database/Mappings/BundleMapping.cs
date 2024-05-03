using GameNow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Database.Mappings
{
	public class BundleMapping : IEntityTypeConfiguration<Bundle>
	{
		public void Configure(EntityTypeBuilder<Bundle> builder)
		{
			builder.ToTable("Bundle");
		}
	}
}
