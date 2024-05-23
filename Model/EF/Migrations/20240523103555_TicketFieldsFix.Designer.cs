﻿// <auto-generated />
using System;
using EventSeller.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventSeller.Migrations
{
    [DbContext(typeof(SellerContext))]
    [Migration("20240523103555_TicketFieldsFix")]
    partial class TicketFieldsFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventSeller.Model.Event", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndEventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartEventDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventSeller.Model.HallSector", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<long>("PlaceHallID")
                        .HasColumnType("bigint");

                    b.Property<string>("SectorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("PlaceHallID");

                    b.ToTable("HallSectors");
                });

            modelBuilder.Entity("EventSeller.Model.PlaceAddress", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PlaceAddresses");
                });

            modelBuilder.Entity("EventSeller.Model.PlaceHall", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<long>("EventAddressID")
                        .HasColumnType("bigint");

                    b.Property<string>("HallName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EventAddressID");

                    b.ToTable("PlaceHalls");
                });

            modelBuilder.Entity("EventSeller.Model.Ticket", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EventID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SeatID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TicketEndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TicketStartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("EventID");

                    b.HasIndex("SeatID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("EventSeller.Model.TicketSeat", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("HallSectorID")
                        .HasColumnType("bigint");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlaceRow")
                        .HasColumnType("int");

                    b.Property<int?>("PlaceSeat")
                        .HasColumnType("int");

                    b.Property<string>("PlaceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("HallSectorID");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("EventSeller.Model.HallSector", b =>
                {
                    b.HasOne("EventSeller.Model.PlaceHall", "PlaceHall")
                        .WithMany("HallSectors")
                        .HasForeignKey("PlaceHallID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlaceHall");
                });

            modelBuilder.Entity("EventSeller.Model.PlaceHall", b =>
                {
                    b.HasOne("EventSeller.Model.PlaceAddress", "EventAddress")
                        .WithMany("PlaceHall")
                        .HasForeignKey("EventAddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventAddress");
                });

            modelBuilder.Entity("EventSeller.Model.Ticket", b =>
                {
                    b.HasOne("EventSeller.Model.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventSeller.Model.TicketSeat", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("EventSeller.Model.TicketSeat", b =>
                {
                    b.HasOne("EventSeller.Model.HallSector", "HallSector")
                        .WithMany("TicketSeats")
                        .HasForeignKey("HallSectorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HallSector");
                });

            modelBuilder.Entity("EventSeller.Model.HallSector", b =>
                {
                    b.Navigation("TicketSeats");
                });

            modelBuilder.Entity("EventSeller.Model.PlaceAddress", b =>
                {
                    b.Navigation("PlaceHall");
                });

            modelBuilder.Entity("EventSeller.Model.PlaceHall", b =>
                {
                    b.Navigation("HallSectors");
                });
#pragma warning restore 612, 618
        }
    }
}