using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    public class SoldPositionsRepository : ISoldPositionsRepository
    {
        private readonly StockInvestmentsContext _stockInvestmentsContext;

        public SoldPositionsRepository(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<SoldPosition> GetSoldPositions(string ticker)
        {
            return _stockInvestmentsContext.SoldPositions.Where(sp 
                => string.Equals(sp.Ticker, ticker, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public SoldPosition GetSoldPosition(string ticker, long number)
        {
            return _stockInvestmentsContext.SoldPositions.FirstOrDefault(sp =>
                string.Equals(sp.Ticker, ticker, StringComparison.CurrentCultureIgnoreCase) && sp.Number == number);
        }

        public void Add(SoldPosition soldPosition)
        {
            _stockInvestmentsContext.SoldPositions.Add(soldPosition);
            _stockInvestmentsContext.SaveChanges();
        }

        public void Update(SoldPosition dbSoldPosition, SoldPosition soldPosition)
        {
            dbSoldPosition.SellingPrice = soldPosition.SellingPrice;
            dbSoldPosition.TotalShares = soldPosition.TotalShares;

            _stockInvestmentsContext.SaveChanges();
        }

        public void Delete(SoldPosition soldPosition)
        {
            _stockInvestmentsContext.SoldPositions.Remove(soldPosition);
            _stockInvestmentsContext.SaveChanges();
        }
    }
}
