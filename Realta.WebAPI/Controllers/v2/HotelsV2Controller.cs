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
                HotelReasonStatus = h.HotelReasonStatus
            });

            return Ok(hotelDto);

        }

        [HttpGet("{id}", Name = "GetHotelsById")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(id);

            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotelDto = new HotelsDto
            {
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
                HotelStatus = hotels.HotelStatus ? "available" : "unavailable",
                HotelModifiedDate = hotels.HotelModifiedDate
            };

            return Ok(hotelDto);
        }

        // POST api/<HotelsController>
        [HttpPost]
        public IActionResult Post([FromBody] HotelsDto dto)
        {

            if (dto == null)
            {
                _logger.LogError("Hotel region object sent from client is null");
                return BadRequest("Some parameters are missing");
            }

            var hotel = new Hotels()
            {
                HotelName = dto.HotelName,
                HotelDescription = dto.HotelDescription,
                HotelStatus = dto.HotelStatus == "available" ? true : false,
                HotelRatingStar = dto.HotelRatingStar,
                HotelPhonenumber = dto.HotelPhonenumber,
                HotelAddrId = (int)dto.HotelAddrId
            };

            //post data to db
            _repositoryManager.HotelsRepository.Insert(hotel);

            var result = _repositoryManager.HotelsRepository.FindHotelsById(hotel.HotelId);


            var resDto = new HotelsDto
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                HotelDescription = hotel.HotelDescription,
                HotelStatus = hotel.HotelStatus ? "available" : "unavailable",
                HotelReasonStatus = hotel.HotelReasonStatus,
                HotelRatingStar = hotel.HotelRatingStar,
                HotelPhonenumber = hotel.HotelPhonenumber,
                HotelModifiedDate = hotel.HotelModifiedDate,
                HotelAddrId = hotel.HotelAddrId
            };
            //forward 
            return Ok(resDto);
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
