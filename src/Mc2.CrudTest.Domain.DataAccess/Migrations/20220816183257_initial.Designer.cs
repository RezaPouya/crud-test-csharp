﻿// <auto-generated />
using System;
using Mc2.CrudTest.Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mc2.CrudTest.Domain.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220816183257_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mc2.CrudTest.Domain.Customers.Customer", b =>
                {
                    b.Property<string>("Email")
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)");

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varchar(34)");

                    b.HasKey("Email");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Mc2.CrudTest.Domain.Customers.Customer", b =>
                {
                    b.OwnsOne("Mc2.CrudTest.Domain.Customers.ValueObjects.CustomerPersonalInfo", "PersonalInfo", b1 =>
                        {
                            b1.Property<string>("CustomerEmail")
                                .HasColumnType("varchar(254)");

                            b1.Property<DateTime>("DateOfBirth")
                                .HasColumnType("datetime2")
                                .HasColumnName("DateOfBirth");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)")
                                .HasColumnName("LastName");

                            b1.HasKey("CustomerEmail");

                            b1.HasIndex("FirstName", "LastName", "DateOfBirth")
                                .IsUnique();

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerEmail");
                        });

                    b.OwnsOne("Mc2.CrudTest.Domain.Customers.ValueObjects.CustomerPhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<string>("CustomerEmail")
                                .HasColumnType("varchar(254)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(31)
                                .HasColumnType("varchar(31)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("CustomerEmail");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerEmail");
                        });

                    b.Navigation("PersonalInfo")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
