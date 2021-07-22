using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    public class ClosedPositionsRepository : IClosedPositionsRepository
    {
        private readonly StockInvestmentsContext _stockInvestmentsContext;

        public ClosedPositionsRepository(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<ClosedPosition> GetClosedPositions()
        {
            return _stockInvestmentsContext.ClosedPositions.ToList();
        }

        public IEnumerable<ClosedPosition> GetClosedPositionsFilteredByFinalValue(double value)
        {
            return _stockInvestmentsContext.ClosedPositions.Where(cp => cp.FinalValue >= value).ToList();
        }

        public ClosedPosition GetClosedPosition(string ticker)
        {
            return _stockInvestmentsContext.ClosedPositions.FirstOrDefault(cp => cp.Ticker == ticker);
        }

        public void Add(ClosedPosition closedPosition)
        {
            if (closedPosition == null)
            {
                throw new ArgumentNullException(nameof(closedPosition));
            }
            _stockInvestmentsContext.ClosedPositions.Add(closedPosition);
            _stockInvestmentsContext.SaveChanges();
        }

        public void Update(ClosedPosition dbClosedPosition, ClosedPosition closedPosition)
        {
            dbClosedPosition.Ticker = closedPosition.Ticker;
            dbClosedPosition.Company = closedPosition.Company;
            dbClosedPosition.FinalValue = closedPosition.FinalValue;

            _stockInvestmentsContext.SaveChanges();
        }

        public void Delete(ClosedPosition closedPosition)
        {
            _stockInvestmentsContext.ClosedPositions.Remove(closedPosition);
            _stockInvestmentsContext.SaveChanges();
        }
    }
}
