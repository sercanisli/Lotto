﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories.EntityFrameworkCore;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.SuperLoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Numbers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Numbers");

                    b.HasKey("Id");

                    b.ToTable("SuperLotos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2023, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Numbers = "45,26,17,6,27,60"
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2023, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Numbers = "25,7,9,17,27,42"
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2023, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Numbers = "12,4,17,6,27,60"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
