using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Context
{
    public class GymDBContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymProject1;Trusted_Connection=True;TrustServerCertificate=True");

        //}

        public GymDBContext(DbContextOptions<GymDBContext> options) : base(options)
        {
        }

        public GymDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDBContext).Assembly);
            modelBuilder.Entity<User>().HasDiscriminator<string>("UserType")
                .HasValue<Member>("Member")
                .HasValue<Trainer>("Trainer");

            modelBuilder.Entity<User>()
            .HasQueryFilter(u => !u.IsDeleted);
                

        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }

        //public override int SaveChanges()
        //{
        //    var entries = ChangeTracker
        //        .Entries<BaseEntity>();

        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //            entry.Entity.CreatedAt = DateTime.UtcNow;


        //        if (entry.State == EntityState.Modified)
        //            entry.Entity.UpdatedAt = DateTime.UtcNow;


        //        if (entry.State == EntityState.Deleted)
        //            entry.Entity.DeletedAt = DateTime.UtcNow;

        //        if (entry.State == EntityState.Deleted)
        //            entry.Entity.IsDeleted = true;

        //    }

        //    return base.SaveChanges();
        //}
    }
}
