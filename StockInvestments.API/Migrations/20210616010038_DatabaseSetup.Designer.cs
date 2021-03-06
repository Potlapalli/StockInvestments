// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockInvestments.API.DbContexts;

namespace StockInvestments.API.Migrations
{
    [DbContext(typeof(StockInvestmentsContext))]
    [Migration("20210616010038_DatabaseSetup")]
    partial class DatabaseSetup
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

                    b.Property<float>("FinalValue")
                        .HasColumnType("real");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Number");

                    b.ToTable("ClosedPositions");
                });

            modelBuilder.Entity("StockInvestments.API.Entities.CurrentPosition", b =>
                {
                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PurchasePrice")
                        .HasColumnType("real");

                    b.Property<float>("TotalShares")
                        .HasColumnType("real");

                    b.HasKey("Ticker");

                    b.ToTable("CurrentPositions");
                });

            modelBuilder.Entity("StockInvestments.API.Entities.SoldPosition", b =>
                {
                    b.Property<long>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrentPositionTicker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("SellingPrice")
                        .HasColumnType("real");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalShares")
                        .HasColumnType("real");

                    b.HasKey("Number");

                    b.HasIndex("CurrentPositionTicker");

                    b.ToTable("SoldPositions");
                });

            modelBuilder.Entity("StockInvestments.API.Entities.SoldPosition", b =>
                {
                    b.HasOne("StockInvestments.API.Entities.CurrentPosition", "CurrentPosition")
                        .WithMany("SoldPositions")
                        .HasForeignKey("CurrentPositionTicker");

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
