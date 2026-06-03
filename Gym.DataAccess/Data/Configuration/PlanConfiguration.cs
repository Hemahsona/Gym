using Gym.DataAccess.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(b => b.Name)
                .HasMaxLength(50);

            builder.Property(b => b.Description)
                .HasMaxLength(200);

            builder.Property(b => b.Price)
                .HasPrecision(18,2);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("PlanDurationDays", "DurationDays BETWEEN 1 AND 365");
            });

            builder.HasQueryFilter(u => !u.IsDeleted);

        }
    }
}
