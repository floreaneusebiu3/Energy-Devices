﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserManagementData;

#nullable disable

namespace UserManagementData.Migrations
{
    [DbContext(typeof(UserManagementContext))]
    partial class UserManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserManagementDomain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d81aca9f-4dd3-4750-8e3a-3d7df37f5442"),
                            Email = "floreaneusebiu@gmail.com",
                            Name = "Eusebiu",
                            Password = "pass",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = new Guid("7fd0b951-6dde-44c7-a9ae-27d59e199b32"),
                            Email = "razvanb@yahoo.com",
                            Name = "Razvan",
                            Password = "pass",
                            Role = "Client"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
