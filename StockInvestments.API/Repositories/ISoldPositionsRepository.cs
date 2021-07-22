﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Repositories
{
    public interface ISoldPositionsRepository
    {
        IEnumerable<SoldPosition> GetSoldPositions();
        IEnumerable<SoldPosition> GetSoldPositions(string ticker);
        IEnumerable<string> GetPositionsInProfit(List<CurrentPosition> currentPositions);
        double GetSharesRemaining(CurrentPosition currentPosition);
        IEnumerable<SoldPosition> GetSoldPositionsFilteredByTotalAmount(double amount);
        SoldPosition GetSoldPosition(string ticker, long number);
        void Add(string ticker, SoldPosition soldPosition);
        void Update(SoldPosition dbSoldPosition, SoldPosition soldPosition);
        void Delete(SoldPosition soldPosition);
    }
}
