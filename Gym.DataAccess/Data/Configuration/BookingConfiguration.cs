using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Data.Configuration
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasOne(m => m.Member)
                .WithMany(b => b.Bookings)
                .HasForeignKey(m => m.MemberId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(s => s.Session)
                .WithMany(b => b.Bookings)
                .HasForeignKey(s => s.SessionId)
                .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
