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
    [Route("api/currentPositions")]
    [ApiController]
    public class CurrentPositionsController : ControllerBase
    {
        private readonly ICurrentPositionsRepository _currentPositionsRepository;
        private readonly IMapper _mapper;
        public CurrentPositionsController(ICurrentPositionsRepository currentPositionsRepository, IMapper mapper)
        {
            _currentPositionsRepository = currentPositionsRepository ??
                                          throw new ArgumentNullException(nameof(currentPositionsRepository));
            _mapper = mapper ?? 
                      throw new ArgumentNullException(nameof(mapper));
        }

        //Get api/currentPositions
        [HttpGet]
        public ActionResult<IEnumerable<CurrentPositionDto>> GetCurrentPositions()
        {
           var currentPositionsFromRepo =  _currentPositionsRepository.GetCurrentPositions();
           return Ok(_mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsFromRepo));
        }

        //Get api/currentPositions/xxx
        [HttpGet("{ticker}")]
        public ActionResult<CurrentPositionDto> GetCurrentPosition(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var currentPositionFromRepo = _currentPositionsRepository.GetCurrentPosition(ticker);
            if (currentPositionFromRepo == null)
                return NotFound("Current Position couldn't be found.");

            return Ok(_mapper.Map<CurrentPositionDto>(currentPositionFromRepo));
        }
    }
}
