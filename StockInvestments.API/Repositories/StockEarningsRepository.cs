using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    public class StockEarningsRepository : IStockEarningsRepository
    {
        private readonly StockInvestmentsContext _stockInvestmentsContext;

        public StockEarningsRepository(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<StockEarning> GetStockEarnings()
        {
            return _stockInvestmentsContext.StockEarnings.ToList();
        }

        public IEnumerable<StockEarning> GetStockEarningsFilteredByDate(DateTimeOffset date)
        {
            return _stockInvestmentsContext.StockEarnings.Where(se => se.EarningsDate == date).ToList();
        }

        public StockEarning GetStockEarning(string ticker)
        {
            return _stockInvestmentsContext.StockEarnings.FirstOrDefault(se => se.Ticker == ticker);
        }

        public void Add(StockEarning stockEarning)
        {
            if (stockEarning == null)
            {
                throw new ArgumentNullException(nameof(stockEarning));
            }
            _stockInvestmentsContext.StockEarnings.Add(stockEarning);
            _stockInvestmentsContext.SaveChanges();
        }

        public void Update(StockEarning dbStockEarning, StockEarning stockEarning)
        {
            dbStockEarning.Company = stockEarning.Company;
            dbStockEarning.EarningsDate = stockEarning.EarningsDate;

            _stockInvestmentsContext.SaveChanges();
        }

        public void Delete(StockEarning stockEarning)
        {
            _stockInvestmentsContext.StockEarnings.Remove(stockEarning);
            _stockInvestmentsContext.SaveChanges();
        }
    }
}
