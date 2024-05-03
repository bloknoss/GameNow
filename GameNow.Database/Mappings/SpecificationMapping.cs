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
	public class SpecificationMapping : IEntityTypeConfiguration<Specifications>
	{
		public void Configure(EntityTypeBuilder<Specifications> builder)
		{
			builder.ToTable("Specification");
		}
	}
}
