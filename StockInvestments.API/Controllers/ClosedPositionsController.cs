using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockInvestments.API.Entities;
using StockInvestments.API.Helpers;
using StockInvestments.API.Models;
using StockInvestments.API.Repositories;

namespace StockInvestments.API.Controllers
{
    [Route("api/closedPositions")]
    [ApiController]
    public class ClosedPositionsController : ControllerBase
    {
        private readonly IClosedPositionsRepository _closedPositionsRepository;
        private readonly IMapper _mapper;
        public ClosedPositionsController(IClosedPositionsRepository closedPositionsRepository, IMapper mapper)
        {
            _closedPositionsRepository = closedPositionsRepository ??
                                       throw new ArgumentNullException(nameof(closedPositionsRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        //Get api/ClosedPositions
        [HttpGet]
        public ActionResult<IEnumerable<ClosedPositionDto>> GetClosedPositions()
        {
            var closedPositionsFromRepo = _closedPositionsRepository.GetClosedPositions();
            return Ok(_mapper.Map<IEnumerable<ClosedPositionDto>>(closedPositionsFromRepo));
        }

        //Get api/ClosedPositions/filterByFinalValue?Value=100
        [Route("filterByFinalValue")]
        [HttpGet]
        public ActionResult<IEnumerable<ClosedPositionDto>> GetCurrentPositionsFilteredByTotalAmount([FromQuery] double value)
        {
            var closedPositionsFromRepo = _closedPositionsRepository.GetClosedPositionsFilteredByFinalValue(value);
            return Ok(_mapper.Map<IEnumerable<ClosedPositionDto>>(closedPositionsFromRepo));
        }

        //Get api/ClosedPositions/xxx
        [HttpGet("{ticker}", Name = "GetClosedPosition")]
        public ActionResult<ClosedPositionDto> GetClosedPosition(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var closedPositionFromRepo = _closedPositionsRepository.GetClosedPosition(ticker);
            if (closedPositionFromRepo == null)
                return NotFound("Closed position couldn't be found.");

            return Ok(_mapper.Map<ClosedPositionDto>(closedPositionFromRepo));
        }

        //Post api/ClosedPositions
        [HttpPost]
        public ActionResult<ClosedPositionDto> CreateClosedPosition(ClosedPositionForCreationDto closedPosition)
        {
            var closedPositionEntity = _mapper.Map<ClosedPosition>(closedPosition);
            _closedPositionsRepository.Add(closedPositionEntity);

            var closedPositionToReturn = _mapper.Map<ClosedPositionDto>(closedPositionEntity);
            return CreatedAtRoute("GetClosedPosition", new { ticker = closedPositionToReturn.Ticker },
                closedPositionToReturn);
        }
    }
}
