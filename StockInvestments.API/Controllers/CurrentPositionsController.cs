using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using StockInvestments.API.Entities;
using StockInvestments.API.Helpers;
using StockInvestments.API.Models;
using StockInvestments.API.Repositories;

namespace StockInvestments.API.Controllers
{
    /// <summary>
    /// CurrentPositionsController
    /// </summary>
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
        [HttpHead]
        public ActionResult<IEnumerable<CurrentPositionDto>> GetCurrentPositions()
        {
           var currentPositionsFromRepo =  _currentPositionsRepository.GetCurrentPositions();
           return Ok(_mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsFromRepo));
        }

        //Get api/currentPositions/filterByTotalAmount?totalAmountGreaterThan=100
        [Route("filterByTotalAmount")]
        [HttpGet]
        public ActionResult<IEnumerable<CurrentPositionDto>> GetCurrentPositionsFilteredByTotalAmount([FromQuery] double totalAmountGreaterThan)
        {
            var currentPositionsFromRepo = _currentPositionsRepository.GetCurrentPositionsFilteredByTotalAmount(totalAmountGreaterThan);
            return Ok(_mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsFromRepo));
        }

        //Get api/currentPositions/xxx
        [HttpGet("{ticker}", Name = "GetCurrentPosition")]
        public ActionResult<CurrentPositionDto> GetCurrentPosition(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var currentPositionFromRepo = _currentPositionsRepository.GetCurrentPosition(ticker);
            if (currentPositionFromRepo == null)
                return NotFound("Current Position couldn't be found.");

            return Ok(_mapper.Map<CurrentPositionDto>(currentPositionFromRepo));
        }

        //Get api/currentPositions/xxx,yyy
        [HttpGet("({tickers})", Name = "GetCurrentPositionCollection")]
        public ActionResult<IEnumerable<CurrentPositionDto>> GetCurrentPositionCollection([FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> tickers)
        {
            if (tickers == null)
                return BadRequest();

            var currentPositionsFromRepo = _currentPositionsRepository.GetCurrentPositions(tickers.ToList());
            if (tickers.Count() != currentPositionsFromRepo.Count())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsFromRepo));
        }


        /// <summary>
        /// Creates a CurrentPosition
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns>A newly created CurrentPosition</returns>
        /// <response code="201">Returns the newly created CurrentPosition</response>
        /// <response code="400">If the CurrentPosition is null</response>
        //Post api/currentPositions
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CurrentPositionDto> CreateCurrentPosition(CurrentPositionForCreationDto currentPosition)
        {
            var currentPositionEntity = _mapper.Map<CurrentPosition>(currentPosition);
            _currentPositionsRepository.Add(currentPositionEntity);

            var currentPositionToReturn = _mapper.Map<CurrentPositionDto>(currentPositionEntity);
            return CreatedAtRoute("GetCurrentPosition", new {ticker = currentPositionToReturn.Ticker},
                currentPositionToReturn);
        }

        //Post api/currentPositionsCollection
        [Route("/api/currentPositionsCollection")]
        [HttpPost]
        public ActionResult<IEnumerable<CurrentPositionDto>> CreateCurrentPositionCollection(IEnumerable<CurrentPositionForCreationDto> currentPositionCollection)
        {
            var currentPositionsEntity = _mapper.Map<IEnumerable<CurrentPosition>>(currentPositionCollection);
            foreach (var currentPositionEntity in currentPositionsEntity)
            {
                _currentPositionsRepository.Add(currentPositionEntity);
            }
            
            var currentPositionsToReturn = _mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsEntity);
            var tickers = string.Join(",", currentPositionsToReturn.Select(cp => cp.Ticker));
            return CreatedAtRoute("GetCurrentPositionCollection", new { tickers = tickers },
                currentPositionsToReturn);
        }

        //Options api/currentPositions
        [HttpOptions]
        public IActionResult GetCurrentPositionsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT,DELETE");
            return Ok();
        }

        //Delete api/currentPositions/xxx
        [HttpDelete("{ticker}")]
        public ActionResult DeleteSoldPositionsForCurrentPosition(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var currentPositionFromRepo = _currentPositionsRepository.GetCurrentPosition(ticker);
            if (currentPositionFromRepo == null)
                return NotFound("Current Position couldn't be found.");

            _currentPositionsRepository.Delete(currentPositionFromRepo);

            return NoContent();
        }
    }
}
