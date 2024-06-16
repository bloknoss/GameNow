﻿using GameNow.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Database.Mappings
{
    public class ImageMapping : IEntityTypeConfiguration<Specifications>
    {
        public void Configure(EntityTypeBuilder<Specifications> builder)
        {
            builder.ToTable("Image");
        }
    }
}
