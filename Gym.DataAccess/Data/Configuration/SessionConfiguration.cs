using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Data.Configuration
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(s => s.Capacity)
                .HasMaxLength(25);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.ToTable(st =>
            {
                st.HasCheckConstraint("Session_DateRange_CK", "[StartDate] < [EndDate]");
            });

            builder.HasQueryFilter(u => !u.IsDeleted);

            builder.HasOne(builder => builder.Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);
             builder.HasOne(builder => builder.Category)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
