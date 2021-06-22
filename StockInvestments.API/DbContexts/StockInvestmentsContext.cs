using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockInvestments.API.Entities;

namespace StockInvestments.API.DbContexts
{
    public class StockInvestmentsContext : DbContext
    {
        public StockInvestmentsContext(DbContextOptions<StockInvestmentsContext> options)
            : base(options)
        {

        }

        public DbSet<CurrentPosition> CurrentPositions { get; set; }
        public DbSet<SoldPosition> SoldPositions { get; set; }
        public DbSet<ClosedPosition> ClosedPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentPosition>().HasData(new CurrentPosition
                {
                    Ticker = "NKLA",
                    Company = "Nikola",
                    PurchasePrice = 81.625,
                    TotalShares = 2
                }, new CurrentPosition
                {
                    Ticker = "ETSY",
                    Company = "Etsy",
                    PurchasePrice = 204.79,
                    TotalShares = 2
                }, new CurrentPosition
                {
                    Ticker = "AAPL",
                    Company = "Apple",
                    PurchasePrice = 142.09,
                    TotalShares = 5
                }
            );

            modelBuilder.Entity<ClosedPosition>().HasData(new ClosedPosition
                {
                    Number = 1,
                    Ticker = "NVAX",
                    Company = "Novavax",
                    FinalValue = 185
                }, new ClosedPosition
                {
                    Number = 2,
                    Ticker = "W",
                    Company = "WayFair",
                    FinalValue = -3.8099
                }, new ClosedPosition
                {
                    Number = 3,
                    Ticker = "SHOP",
                    Company = "Shopify",
                    FinalValue = 10.54946
                }
            );
        }
    }
}
