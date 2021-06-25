﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace StockInvestments.API.Profiles
{
    public class SoldPositionsProfile : Profile
    {
        public SoldPositionsProfile()
        {
            CreateMap<Entities.SoldPosition, Models.SoldPositionDto>();
        }
    }
}
