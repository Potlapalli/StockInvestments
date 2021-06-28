using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    public interface ICurrentPositionsRepository
    {
        IEnumerable<CurrentPosition> GetCurrentPositions();
        IEnumerable<CurrentPosition> GetCurrentPositionsFilteredByTotalAmount(double amount);
        CurrentPosition GetCurrentPosition(string ticker);
        void Add(CurrentPosition currentPosition);
        void Update(CurrentPosition dbCurrentPosition, CurrentPosition currentPosition);
        void Delete(CurrentPosition currentPosition);
        bool CurrentPositionExists(string ticker);
    }
}
