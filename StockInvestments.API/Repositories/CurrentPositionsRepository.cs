using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    public class CurrentPositionsRepository : ICurrentPositionsRepository
    {
        private readonly StockInvestmentsContext _stockInvestmentsContext;

        public CurrentPositionsRepository(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<CurrentPosition> GetCurrentPositions()
        {
            return _stockInvestmentsContext.CurrentPositions.ToList();
        }

        public IEnumerable<CurrentPosition> GetCurrentPositionsFilteredByTotalAmount(double amount)
        {
            return _stockInvestmentsContext.CurrentPositions.Where(cp => cp.TotalAmount >= amount).ToList();
        }

        public CurrentPosition GetCurrentPosition(string ticker)
        {
            return _stockInvestmentsContext.CurrentPositions.FirstOrDefault(cp => cp.Ticker == ticker);
        }

        public void Add(CurrentPosition currentPosition)
        {
            _stockInvestmentsContext.CurrentPositions.Add(currentPosition);
            _stockInvestmentsContext.SaveChanges();
        }

        public void Update(CurrentPosition dbCurrentPosition, CurrentPosition currentPosition)
        {
            dbCurrentPosition.Company = currentPosition.Company;
            dbCurrentPosition.PurchasePrice = currentPosition.PurchasePrice;
            dbCurrentPosition.TotalShares = currentPosition.TotalShares;
            dbCurrentPosition.TotalAmount = currentPosition.TotalAmount;

            _stockInvestmentsContext.SaveChanges();
        }

        public void Delete(CurrentPosition currentPosition)
        {
            _stockInvestmentsContext.CurrentPositions.Remove(currentPosition);
            _stockInvestmentsContext.SaveChanges();
        }

        public bool CurrentPositionExists(string ticker)
        {
            return _stockInvestmentsContext.CurrentPositions.Any(cp => cp.Ticker == ticker);
        }
    }
}
