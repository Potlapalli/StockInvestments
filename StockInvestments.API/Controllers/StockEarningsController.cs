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
    [Route("api/stockEarnings")]
    [ApiController]
    public class StockEarningsController : ControllerBase
    {
        private readonly IStockEarningsRepository _stockEarningsRepository;
        private readonly IMapper _mapper;
        public StockEarningsController(IStockEarningsRepository stockEarningsRepository, IMapper mapper)
        {
            _stockEarningsRepository = stockEarningsRepository ??
                                       throw new ArgumentNullException(nameof(stockEarningsRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        //Get api/stockEarnings
        [HttpGet]
        public ActionResult<IEnumerable<StockEarningDto>> GetStockEarnings()
        {
            var stockEarningsFromRepo = _stockEarningsRepository.GetStockEarnings();
            return Ok(_mapper.Map<IEnumerable<StockEarningDto>>(stockEarningsFromRepo));
        }

        //Get api/stockEarnings/filterByDate?Date=xxx
        [Route("filterByDate")]
        [HttpGet]
        public ActionResult<IEnumerable<StockEarningDto>> GetCurrentPositionsFilteredByTotalAmount([FromQuery] DateTimeOffset date)
        {
            var stockEarningsFromRepo = _stockEarningsRepository.GetStockEarningsFilteredByDate(date);
            return Ok(_mapper.Map<IEnumerable<StockEarningDto>>(stockEarningsFromRepo));
        }

        //Get api/stockEarnings/xxx
        [HttpGet("{ticker}", Name = "GetStockEarning")]
        public ActionResult<StockEarningDto> GetStockEarning(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var stockEarningFromRepo = _stockEarningsRepository.GetStockEarning(ticker);
            if (stockEarningFromRepo == null)
                return NotFound("Stock Earning couldn't be found.");

            return Ok(_mapper.Map<StockEarningDto>(stockEarningFromRepo));
        }

        //Post api/stockEarnings
        [HttpPost]
        public ActionResult<StockEarningDto> CreateStockEarning(StockEarningForCreationDto stockEarning)
        {
            var stockEarningEntity = _mapper.Map<StockEarning>(stockEarning);
            if (!string.IsNullOrEmpty(stockEarningEntity.EarningsCallTime) &&
                stockEarningEntity.EarningsCallTime != nameof(EarningsCallTime.AM) &&
                stockEarningEntity.EarningsCallTime != nameof(EarningsCallTime.PM)) 
                return BadRequest("Invalid Earnings call time provided");
            
            _stockEarningsRepository.Add(stockEarningEntity);

            var stockEarningToReturn = _mapper.Map<StockEarningDto>(stockEarningEntity);
            return CreatedAtRoute("GetStockEarning", new { ticker = stockEarningToReturn.Ticker },
                stockEarningToReturn);
        }
    }
}
