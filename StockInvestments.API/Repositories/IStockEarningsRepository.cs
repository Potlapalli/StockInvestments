using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    public interface IStockEarningsRepository
    {
        IEnumerable<StockEarning> GetStockEarnings();
        IEnumerable<StockEarning> GetStockEarningsFilteredByDate(DateTimeOffset date);
        StockEarning GetStockEarning(string ticker);
        void Add(StockEarning stockEarning);
        void Update(StockEarning dbStockEarning, StockEarning stockEarning);
        void Delete(StockEarning stockEarning);
    }
}
