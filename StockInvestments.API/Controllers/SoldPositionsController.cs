using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockInvestments.API.Models;
using StockInvestments.API.Repositories;

namespace StockInvestments.API.Controllers
{
    [Route("api/currentPositions/{ticker}/soldPositions")]
    [ApiController]
    public class SoldPositionsController : ControllerBase
    {
        private readonly ICurrentPositionsRepository _currentPositionsRepository;
        private readonly ISoldPositionsRepository _soldPositionsRepository;
        private readonly IMapper _mapper;
        public SoldPositionsController(ICurrentPositionsRepository currentPositionsRepository, ISoldPositionsRepository soldPositionsRepository, IMapper mapper)
        {
            _currentPositionsRepository = currentPositionsRepository ??
                                          throw new ArgumentNullException(nameof(currentPositionsRepository));

            _soldPositionsRepository = soldPositionsRepository ??
                                       throw new ArgumentNullException(nameof(soldPositionsRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        //Get api/currentPositions/xxx/soldPositions
        [HttpGet]
        public ActionResult<IEnumerable<SoldPositionDto>> GetSoldPositionsForCurrentPosition(string ticker)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }
            var soldPositionsFromRepo = _soldPositionsRepository.GetSoldPositions(ticker);
            return Ok(_mapper.Map<IEnumerable<SoldPositionDto>>(soldPositionsFromRepo));
        }

        //Get api/currentPositions/xxx/soldPositions/1
        [HttpGet("{number}")]
        public ActionResult<SoldPositionDto> GetCurrentPosition(string ticker, long number)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var soldPositionFromRepo = _soldPositionsRepository.GetSoldPosition(ticker, number);
            if (soldPositionFromRepo == null)
                return NotFound("Sold Position couldn't be found.");

            return Ok(_mapper.Map<SoldPositionDto>(soldPositionFromRepo));
        }
    }
}
