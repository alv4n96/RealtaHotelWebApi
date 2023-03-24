using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;
using Realta.Domain.RequestFeatures.HotelParameters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;

        public HotelsController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        // create class for middleware auth

        // GET: api/<HotelsController>
        [HttpGet]
        public async Task<IActionResult> GetAllHotelAsync()
        {
            var hotels = await _repositoryManager.HotelsRepository.FindAllHotelsAsync();
            if (hotels.ToList().Count() == 0)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotelDto = hotels.Select(h => new HotelsDto
            {
                HotelId = h.HotelId,
                HotelName = h.HotelName,
                HotelRatingStar = h.HotelRatingStar,
                HotelPhonenumber = h.HotelPhonenumber,
                HotelReasonStatus = h.HotelReasonStatus,
                // HotelStatus = h.HotelStatus ? "available" : "unavailable",
                HotelStatus = h.HotelStatus,
                HotelModifiedDate = h.HotelModifiedDate,
            });


            return Ok(hotelDto);
        }

        // GET api/<HotelsController>/search/{name}
        [HttpGet("search/{name}", Name = "GetHotelsByName")]
        public IActionResult GetHotelByName(string name)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsByName(name);
            if (hotels.ToList().Count() == 0)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotelDto = hotels.Select(h => new HotelsDto
            {
                HotelId = h.HotelId,
                HotelName = h.HotelName,
                HotelAddrDescription = h.HotelAddrDescription,
                HotelRatingStar = h.HotelRatingStar,
                HotelPhonenumber = h.HotelPhonenumber,
                // HotelStatus = h.HotelStatus ? "available" : "unavailable",
                HotelStatus = h.HotelStatus,
                HotelModifiedDate = h.HotelModifiedDate,
            });

            return Ok(hotelDto);
        }

        //GET api/<HotelsController>/5
        [HttpGet("{id}", Name = "GetHotelsById")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotels = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(id);

            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotelDto = new HotelsDto
            {
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelDescription = hotels.HotelDescription,
                HotelAddrDescription = hotels.HotelAddrDescription,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
                // HotelStatus = hotels.HotelStatus ? "available" : "unavailable",
                HotelStatus = hotels.HotelStatus,
                HotelReasonStatus = hotels.HotelReasonStatus,
                HotelModifiedDate = hotels.HotelModifiedDate,
            };

            return Ok(hotelDto);
        }

        // POST api/<HotelsController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] HotelsDto dto)
        {

            if (dto == null)
            {
                _logger.LogError("Hotel region object sent from client is null");
                return BadRequest("Some parameters are missing");
            }

            var hotel = new Hotels()
            {
                HotelName = dto.HotelName,
                HotelPhonenumber = dto.HotelPhonenumber,
                HotelStatus = dto.HotelStatus,
                // HotelStatus = dto.HotelStatus == "available" ? true : false,
                HotelAddrDescription = dto.HotelAddrDescription,
                HotelDescription = dto.HotelDescription,
            };

            //post data to db
            _repositoryManager.HotelsRepository.Insert(hotel);

            var result = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotel.HotelId);


            var resDto = new HotelsDto
            {
                HotelId = result.HotelId,
                HotelName = result.HotelName,
                HotelDescription = result.HotelDescription,
                // HotelStatus = result.HotelStatus ? "available" : "unavailable",
                HotelStatus = result.HotelStatus,
                HotelReasonStatus = result.HotelReasonStatus,
                HotelRatingStar = result.HotelRatingStar,
                HotelPhonenumber = result.HotelPhonenumber,
                HotelModifiedDate = result.HotelModifiedDate,
                HotelAddrId = result.HotelAddrId
            };
            //forward 
            return Ok(resDto);
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditHotel(int id, [FromBody] HotelsDto dto)
        {

            if (dto == null)
            {
                _logger.LogError("Hotels when update object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotel = new Hotels()
            {
                HotelId = id,
                HotelName = dto.HotelName,
                HotelDescription = dto.HotelDescription,
                // HotelStatus = dto.HotelStatus == "available" ? true : false,
                HotelPhonenumber = dto.HotelPhonenumber,
                HotelAddrDescription = dto.HotelAddrDescription
            };

            _repositoryManager.HotelsRepository.Edit(hotel);
            if (hotel == null)
            {
                _logger.LogError("Hotels when update object sent from client is null");
                return BadRequest("Internal edit record hotel error");
            }

            var checker = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(id);

            var result = new HotelsDto
            {
                HotelId = id,
                HotelName = checker.HotelName,
                HotelDescription = checker.HotelDescription,
                // HotelStatus = checker.HotelStatus ? "available" : "unavailable",
                
                HotelReasonStatus = checker.HotelReasonStatus,
                HotelRatingStar = checker.HotelRatingStar,
                HotelPhonenumber = checker.HotelPhonenumber,
                HotelModifiedDate = checker.HotelModifiedDate,
                HotelAddrId = checker.HotelAddrId
            };

            return Ok(result);
        }
        
        [HttpGet("paging")]
        public async Task<IActionResult> GetHotelPaging([FromQuery] HotelsParameters hotelParam)
        {
            var hotels = await _repositoryManager.HotelsRepository.GetHotelPaging(hotelParam);
            return Ok(hotels);
        }

        [HttpGet("pageList")]
        public async Task<IActionResult> GetProductPageList([FromQuery] HotelsParameters hotelParam)
        {
            if (!hotelParam.ValidateStatus)
            {
                return BadRequest("Available must be 1 And Unavailable must be 0");

            }

            var hotels = await _repositoryManager.HotelsRepository.GetHotelPageList(hotelParam);


            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(hotels.MetaData));

            return Ok(hotels);
        }


        // PUT api/<HotelsController>/switch/5
        [HttpPut("switch/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] HotelSwitchDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Hotels when update object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotel = new Hotels()
            {
                HotelId = id,
                HotelStatus = dto.HotelStatus,
                HotelReasonStatus = string.IsNullOrEmpty(dto.HotelReasonStatus) ? string.Empty : dto.HotelReasonStatus,
            };

            _repositoryManager.HotelsRepository.EditStatus(hotel);

            var dataResult = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(id);

            var result = new HotelsDto
            {
                HotelId = hotel.HotelId,
                HotelName = dataResult.HotelName,
                // HotelStatus = hotel.HotelStatus ? "available" : "unavailable",
                HotelStatus = hotel.HotelStatus,
                HotelReasonStatus = hotel.HotelReasonStatus,
                HotelRatingStar = dataResult.HotelRatingStar,
                HotelPhonenumber = dataResult.HotelPhonenumber,
                HotelModifiedDate = dataResult.HotelModifiedDate
            };

            return Ok(result);
        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //1. Id cannot be null
            if (id == null)
            {
                _logger.LogError("Id Hotels object sent from client is null");
                return BadRequest("Id Record doesn't exist or wrong parameter");
            }

            //2. Find id first
            var hotel = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(id);
            if (hotel == null)
            {
                _logger.LogError($"Hotel with id {id} Record doesn't exist or wrong parameter");
                return NotFound();
            }

            _repositoryManager.HotelsRepository.Remove(hotel);
            return Ok("Data Has Been Remove.");
        }
    }
}
