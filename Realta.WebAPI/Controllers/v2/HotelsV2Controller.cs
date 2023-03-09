using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.Base;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers.v2
{
    [Route("api/v2/Hotel")]
    [ApiController]
    public class HotelsV2Controller : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;
        
        // GET: api/<HotelsV2Controller>
        public HotelsV2Controller(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _repositoryManager.HotelsRepository.FindAllHotelsAsync();

            var hotelDto = hotels.Select(h => new HotelsDto
            {
                HotelId = h.HotelId,
                HotelName = h.HotelName,
                HotelAddrId = h.HotelAddrId,
                HotelDescription = h.HotelDescription,
                HotelModifiedDate = h.HotelModifiedDate,
                HotelPhonenumber = h.HotelPhonenumber,
                HotelRatingStar = h.HotelRatingStar,
                HotelStatus = h.HotelStatus ? "available" : "unavailable",
            });

            return Ok(hotelDto);

        }

        // GET api/<HotelsV2Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HotelsV2Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HotelsV2Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HotelsV2Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
