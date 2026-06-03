using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Data.Configuration
{
    public class UserConfiguration<T> : IEntityTypeConfiguration<T> where T : User
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {


            builder.Property(u => u.Name)
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .HasMaxLength(100);

            builder.Property(u => u.Phone)
                .HasMaxLength(11);

            builder.HasIndex(u => u.Phone)
                .IsUnique();

            builder.OwnsOne(u => u.Address, a =>
            {
                a.Property(ad => ad.City)
                .HasColumnName("City")// instead of Address_City
                .HasMaxLength(50);
                a.Property(ad => ad.Street)
                .HasColumnName("Street")
                .HasMaxLength(50);
                a.Property(ad => ad.BuildingNumber)
                .HasColumnName("BuildingNumber")
                .HasMaxLength(50);
            });

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            builder.ToTable(tb => tb.HasCheckConstraint("User_Phone_Ck", "[Phone] Like '01[0125]%'"));


            builder.Property(u => u.Gender)
                .HasConversion<string>();
            //builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
