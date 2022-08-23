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
    [Migration("20220822173610_change-data-of-birth-type")]
    partial class changedataofbirthtype
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varchar(34)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)");

                    b.HasKey("Id");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Mc2.CrudTest.Domain.Customers.Customer", b =>
                {
                    b.OwnsOne("Mc2.CrudTest.Domain.Customers.ValueObjects.CustomerPersonalInfo", "PersonalInfo", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("DateOfBirth")
                                .HasColumnType("date")
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

                            b1.HasKey("CustomerId");

                            b1.HasIndex("FirstName", "LastName", "DateOfBirth")
                                .IsUnique();

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.OwnsOne("Mc2.CrudTest.Domain.Customers.ValueObjects.CustomerPhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .HasColumnType("int");

                            b1.Property<decimal>("Number")
                                .HasMaxLength(31)
                                .HasColumnType("numeric(20,0)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
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
