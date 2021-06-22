using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockInvestments.API.Entities;
using StockInvestments.API.Repositories;

namespace StockInvestments.API.Controllers
{
    [Route("api/currentPositions")]
    [ApiController]
    public class CurrentPositionsController : ControllerBase
    {
        private readonly ICurrentPositionsRepository _currentPositionsRepository;
        public CurrentPositionsController(ICurrentPositionsRepository currentPositionsRepository)
        {
            _currentPositionsRepository = currentPositionsRepository ??
                                          throw new ArgumentNullException(nameof(currentPositionsRepository));
        }

        //Get api/currentPositions
        [HttpGet]
        public IActionResult GetCurrentPositions()
        {
           var currentPositions =  _currentPositionsRepository.GetCurrentPositions();
           return Ok(currentPositions);
        }

        //Get api/currentPositions/5
        [HttpGet("{ticker}")]
        public IActionResult GetCurrentPosition(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");
            var currentPosition = _currentPositionsRepository.GetCurrentPosition(ticker);
            if (currentPosition == null)
                return NotFound("Current Position couldn't be found.");
            return Ok(currentPosition);
        }
    }
}
