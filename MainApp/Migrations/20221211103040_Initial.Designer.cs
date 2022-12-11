﻿// <auto-generated />
using System;
using MainApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MainApp.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20221211103040_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("LabDB.Entity.Agent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Agents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "Test",
                            Password = "Test"
                        });
                });

            modelBuilder.Entity("LabDB.Entity.Computer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Computers");

                    b.HasData(
                        new
                        {
                            Id = 1
                        });
                });

            modelBuilder.Entity("LabDB.Entity.LoadedApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AgentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ComputerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("ComputerId");

                    b.ToTable("LoadedApps");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgentId = 1,
                            ComputerId = 1,
                            DateTime = new DateTime(2022, 12, 11, 13, 30, 40, 218, DateTimeKind.Local).AddTicks(8194),
                            Name = "Приложение 1"
                        },
                        new
                        {
                            Id = 2,
                            AgentId = 1,
                            ComputerId = 1,
                            DateTime = new DateTime(2022, 12, 11, 13, 30, 40, 218, DateTimeKind.Local).AddTicks(8210),
                            Name = "Приложение 2"
                        },
                        new
                        {
                            Id = 3,
                            AgentId = 1,
                            ComputerId = 1,
                            DateTime = new DateTime(2022, 12, 11, 13, 30, 40, 218, DateTimeKind.Local).AddTicks(8211),
                            Name = "Приложение 3"
                        },
                        new
                        {
                            Id = 4,
                            AgentId = 1,
                            ComputerId = 1,
                            DateTime = new DateTime(2022, 12, 11, 13, 30, 40, 218, DateTimeKind.Local).AddTicks(8212),
                            Name = "Приложение 4"
                        });
                });

            modelBuilder.Entity("LabDB.Entity.LoadedApp", b =>
                {
                    b.HasOne("LabDB.Entity.Agent", "Agent")
                        .WithMany("LoadedApps")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LabDB.Entity.Computer", "Computer")
                        .WithMany("LoadedApps")
                        .HasForeignKey("ComputerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");

                    b.Navigation("Computer");
                });

            modelBuilder.Entity("LabDB.Entity.Agent", b =>
                {
                    b.Navigation("LoadedApps");
                });

            modelBuilder.Entity("LabDB.Entity.Computer", b =>
                {
                    b.Navigation("LoadedApps");
                });
#pragma warning restore 612, 618
        }
    }
}
