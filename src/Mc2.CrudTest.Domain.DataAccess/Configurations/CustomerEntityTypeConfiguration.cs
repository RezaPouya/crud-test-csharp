﻿using Mc2.CrudTest.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Domain.DataAccess.Configurations
{
    internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Email)
                .HasMaxLength(254)
                .IsRequired()
                .HasColumnType("varchar(254)")
                .ValueGeneratedNever();

            builder.Property(p => p.BankAccountNumber)
                .HasMaxLength(34)
                .HasColumnType("varchar(34)").IsRequired();

            // Customers must be unique in database: By Firstname, Lastname and DateOfBirth.

            builder.OwnsOne(pi => pi.PersonalInfo, builder =>
            {
                builder.Property(p => p.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(64)
                .IsRequired();

                builder.Property(p => p.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(64)
                .IsRequired();

                builder.Property(p => p.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .HasColumnType("date")
                .IsRequired();

                builder.HasIndex(e => new { e.FirstName, e.LastName, e.DateOfBirth }).IsUnique();
            });

            builder.OwnsOne(pi => pi.PhoneNumber, builder =>
            {
                builder.Property(p => p.Number)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(31)
                .HasColumnType("numeric(20,0)")
                .IsRequired();
            });
        }
    }
}