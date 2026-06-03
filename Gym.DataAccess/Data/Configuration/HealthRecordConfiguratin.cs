using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Data.Configuration
{
    public class HealthRecordConfiguratin : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.Property(h => h.Height)
                .HasMaxLength(10);

            builder.Property(h => h.Weight)
                .HasMaxLength(10);

            builder.Property(h => h.BloodType)
                .HasConversion<string>();

            builder.HasQueryFilter(u => !u.IsDeleted);

        }
    }
}
