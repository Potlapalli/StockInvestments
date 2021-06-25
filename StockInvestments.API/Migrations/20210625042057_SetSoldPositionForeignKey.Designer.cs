﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockInvestments.API.DbContexts;

namespace StockInvestments.API.Migrations
{
    [DbContext(typeof(StockInvestmentsContext))]
    [Migration("20210625042057_SetSoldPositionForeignKey")]
    partial class SetSoldPositionForeignKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StockInvestments.API.Entities.ClosedPosition", b =>
                {
                    b.Property<long>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FinalValue")
                        .HasColumnType("float");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Number");

                    b.ToTable("ClosedPositions");

                    b.HasData(
                        new
                        {
                            Number = 1L,
                            Company = "Novavax",
                            FinalValue = 185.0,
                            Ticker = "NVAX"
                        },
                        new
                        {
                            Number = 2L,
                            Company = "WayFair",
                            FinalValue = -3.8098999999999998,
                            Ticker = "W"
                        },
                        new
                        {
                            Number = 3L,
                            Company = "Shopify",
                            FinalValue = 10.54946,
                            Ticker = "SHOP"
                        });
                });

            modelBuilder.Entity("StockInvestments.API.Entities.CurrentPosition", b =>
                {
                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("float");

                    b.Property<double>("TotalShares")
                        .HasColumnType("float");

                    b.HasKey("Ticker");

                    b.ToTable("CurrentPositions");

                    b.HasData(
                        new
                        {
                            Ticker = "NKLA",
                            Company = "Nikola",
                            PurchasePrice = 81.625,
                            TotalShares = 2.0
                        },
                        new
                        {
                            Ticker = "ETSY",
                            Company = "Etsy",
                            PurchasePrice = 204.78999999999999,
                            TotalShares = 2.0
                        },
                        new
                        {
                            Ticker = "AAPL",
                            Company = "Apple",
                            PurchasePrice = 142.09,
                            TotalShares = 5.0
                        });
                });

            modelBuilder.Entity("StockInvestments.API.Entities.SoldPosition", b =>
                {
                    b.Property<long>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("SellingPrice")
                        .HasColumnType("float");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("TotalShares")
                        .HasColumnType("float");

                    b.HasKey("Number");

                    b.HasIndex("Ticker");

                    b.ToTable("SoldPositions");
                });

            modelBuilder.Entity("StockInvestments.API.Entities.SoldPosition", b =>
                {
                    b.HasOne("StockInvestments.API.Entities.CurrentPosition", "CurrentPosition")
                        .WithMany("SoldPositions")
                        .HasForeignKey("Ticker");

                    b.Navigation("CurrentPosition");
                });

            modelBuilder.Entity("StockInvestments.API.Entities.CurrentPosition", b =>
                {
                    b.Navigation("SoldPositions");
                });
#pragma warning restore 612, 618
        }
    }
}
