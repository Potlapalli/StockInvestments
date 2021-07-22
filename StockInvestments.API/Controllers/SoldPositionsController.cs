using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockInvestments.API.Entities;
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

        //Get api/soldPositions
        [Route("/api/soldPositions")]
        [HttpGet]
        public ActionResult<IEnumerable<SoldPositionDto>> GetSoldPositions()
        {
            var soldPositionsFromRepo = _soldPositionsRepository.GetSoldPositions();
            return Ok(_mapper.Map<IEnumerable<SoldPositionDto>>(soldPositionsFromRepo));
        }

        //Get api/currentPositions/xxx/soldPositions/sharesRemaining
        [Route("sharesRemaining")]
        [HttpGet]
        public ActionResult<double> GetSharesRemaining(string ticker)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var currentPosition = _currentPositionsRepository.GetCurrentPosition(ticker);
            var sharesRemaining = _soldPositionsRepository.GetSharesRemaining(currentPosition);
            return Ok(sharesRemaining);
        }

        //Get api/positionsInProfit
        [Route("/api/positionsInProfit")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetPositionsInProfit()
        {
            var currentPositions = _currentPositionsRepository.GetCurrentPositions().ToList();
            var positionsInProfit = _soldPositionsRepository.GetPositionsInProfit(currentPositions);
            return Ok(positionsInProfit);
        }

        //Get api/soldPositions/filterByTotalAmount?totalAmountGreaterThan=100
        [Route("/api/soldPositions/filterByTotalAmount")]
        [HttpGet]
        public ActionResult<IEnumerable<SoldPositionDto>> GetSoldPositionsFilteredByTotalAmount([FromQuery] double totalAmountGreaterThan)
        {
            var soldPositionsFromRepo = _soldPositionsRepository.GetSoldPositionsFilteredByTotalAmount(totalAmountGreaterThan);
            return Ok(_mapper.Map<IEnumerable<SoldPositionDto>>(soldPositionsFromRepo));
        }

        //Get api/currentPositions/xxx/soldPositions/1
        [HttpGet("{number}", Name = "GetSoldPositionForCurrentPosition")]
        public ActionResult<SoldPositionDto> GetSoldPositionForCurrentPosition(string ticker, long number)
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

        //Get api/currentPositions/xxx/soldPositions
        [HttpPost]
        public ActionResult<SoldPositionDto> CreateSoldPosition(string ticker, SoldPositionForCreationDto soldPosition)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var soldPositionEntity = _mapper.Map<SoldPosition>(soldPosition);
            _soldPositionsRepository.Add(ticker, soldPositionEntity);

            var soldPositionToReturn = _mapper.Map<SoldPositionDto>(soldPositionEntity);
            return CreatedAtRoute("GetSoldPositionForCurrentPosition", new { ticker = ticker, number = soldPositionToReturn.Number },
                soldPositionToReturn);
        }
    }
}
