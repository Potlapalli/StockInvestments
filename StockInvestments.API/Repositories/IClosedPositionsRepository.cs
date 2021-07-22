using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    public interface IClosedPositionsRepository 
    {
        IEnumerable<ClosedPosition> GetClosedPositions();
        IEnumerable<ClosedPosition> GetClosedPositionsFilteredByFinalValue(double value);
        ClosedPosition GetClosedPosition(string ticker);
        void Add(ClosedPosition closedPosition);
        void Update(ClosedPosition dbClosedPosition, ClosedPosition closedPosition);
        void Delete(ClosedPosition closedPosition);
    }
}
