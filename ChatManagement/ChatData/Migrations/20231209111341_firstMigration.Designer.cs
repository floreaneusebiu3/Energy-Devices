﻿// <auto-generated />
using System;
using ChatData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatManagementData.Migrations
{
    [DbContext(typeof(ChatManagementContext))]
    [Migration("20231209111341_firstMigration")]
    partial class firstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatDomain.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ChatDomain.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DestionationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Seen")
                        .HasColumnType("bit");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DestionationUserId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatDomain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatDomain.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("ChatDomain.Group", b =>
                {
                    b.HasOne("ChatDomain.User", "Admin")
                        .WithMany("GroupsAsAdmin")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("ChatDomain.Message", b =>
                {
                    b.HasOne("ChatDomain.User", "DestionationUser")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("DestionationUserId");

                    b.HasOne("ChatDomain.Group", "Group")
                        .WithMany("Messages")
                        .HasForeignKey("GroupId");

                    b.HasOne("ChatDomain.User", "SenderUser")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestionationUser");

                    b.Navigation("Group");

                    b.Navigation("SenderUser");
                });

            modelBuilder.Entity("ChatDomain.UserGroup", b =>
                {
                    b.HasOne("ChatDomain.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ChatDomain.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatDomain.Group", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("ChatDomain.User", b =>
                {
                    b.Navigation("GroupsAsAdmin");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("SentMessages");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
