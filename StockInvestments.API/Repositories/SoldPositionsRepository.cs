using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SoldPositionsRepository : ISoldPositionsRepository
    {
        private readonly StockInvestmentsContext _stockInvestmentsContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SoldPositionsRepository(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<SoldPosition> GetSoldPositions()
        {
            return _stockInvestmentsContext.SoldPositions.ToList();
        }

        public IEnumerable<SoldPosition> GetSoldPositions(string ticker)
        {
            return _stockInvestmentsContext.SoldPositions.Where(sp => sp.Ticker == ticker).ToList();
        }

        public IEnumerable<string> GetPositionsInProfit(List<CurrentPosition> currentPositions)
        {
            return (from currentPosition in currentPositions 
                let soldPositions = _stockInvestmentsContext.SoldPositions.
                    Where(sp => sp.Ticker == currentPosition.Ticker).ToList() 
                let totalAmountSum = soldPositions.Sum(soldPosition => soldPosition.TotalAmount) 
                where currentPosition.TotalAmount < totalAmountSum select currentPosition.Ticker).ToList();
        }

        public double GetSharesRemaining(CurrentPosition currentPosition)
        {
            var soldPositions = _stockInvestmentsContext.SoldPositions.Where(sp => sp.Ticker == currentPosition.Ticker).ToList();

            double totalShares = soldPositions.Sum(soldPosition => soldPosition.TotalShares);
            return currentPosition.TotalShares - totalShares;
        }

        public IEnumerable<SoldPosition> GetSoldPositionsFilteredByTotalAmount(double amount)
        {
            return _stockInvestmentsContext.SoldPositions.Where(sp => sp.TotalAmount >= amount).ToList();
        }

        public SoldPosition GetSoldPosition(string ticker, long number)
        {
            return _stockInvestmentsContext.SoldPositions.FirstOrDefault(sp => sp.Ticker == ticker && sp.Number == number);
        }

        public void Add(string ticker, SoldPosition soldPosition)
        {
            if (soldPosition == null)
            {
                throw new ArgumentNullException(nameof(soldPosition));
            }
            soldPosition.Ticker = ticker;
            _stockInvestmentsContext.SoldPositions.Add(soldPosition);
            _stockInvestmentsContext.SaveChanges();
        }

        public void Update(SoldPosition dbSoldPosition, SoldPosition soldPosition)
        {
            dbSoldPosition.SellingPrice = soldPosition.SellingPrice;
            dbSoldPosition.TotalShares = soldPosition.TotalShares;
            dbSoldPosition.TotalAmount = soldPosition.TotalAmount;

            _stockInvestmentsContext.SaveChanges();
        }

        public void Delete(SoldPosition soldPosition)
        {
            _stockInvestmentsContext.SoldPositions.Remove(soldPosition);
            _stockInvestmentsContext.SaveChanges();
        }
    }
}
