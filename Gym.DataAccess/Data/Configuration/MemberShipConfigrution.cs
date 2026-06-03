using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Data.Configuration
{
    internal class MemberShipConfigrution : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.ToTable(ms =>
            {
                ms.HasCheckConstraint("MemberShip_DateRange_CK", "[StartDate] < [EndDate]");
            });

            builder.HasOne(ms => ms.Member)
                .WithMany(m => m.MemberShips)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ms => ms.Plan)
                .WithMany(p => p.MemberShips)
                .HasForeignKey(ms => ms.PlanId)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
