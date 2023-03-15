using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Persistence.Interface;
using Realta.Services.Abstraction;
using System.Runtime.InteropServices;

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
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelDescription = hotels.HotelDescription,
                HotelAddrDescription = hotels.HotelAddrDescription,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
                // HotelStatus = hotels.HotelStatus ? "available" : "unavailable",
                HotelStatus = hotels.HotelStatus,
                HotelModifiedDate = hotels.HotelModifiedDate,
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
                // HotelStatus = dto.HotelStatus == "available" ? true : false,
                HotelStatus = dto.HotelStatus,
                HotelRatingStar = dto.HotelRatingStar,
                HotelPhonenumber = dto.HotelPhonenumber,
                HotelAddrId = dto.HotelAddrId
            };

            //post data to db
            _repositoryManager.HotelsRepository.Insert(hotel);

            var result = _repositoryManager.HotelsRepository.FindHotelsById(hotel.HotelId);


            var resDto = new HotelsDto
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                HotelDescription = hotel.HotelDescription,
                // HotelStatus = hotel.HotelStatus ? "available" : "unavailable",
                HotelStatus = hotel.HotelStatus,
                HotelReasonStatus = hotel.HotelReasonStatus,
                HotelRatingStar = hotel.HotelRatingStar,
                HotelPhonenumber = hotel.HotelPhonenumber,
                HotelModifiedDate = hotel.HotelModifiedDate,
                HotelAddrId = hotel.HotelAddrId
            };
            //forward 
            return Ok(resDto);
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public IActionResult EditHotel(int id, [FromBody] HotelsDto dto)
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
                HotelStatus = dto.HotelStatus,
                HotelRatingStar = dto.HotelRatingStar,
                HotelPhonenumber = dto.HotelPhonenumber,
                HotelAddrId = dto.HotelAddrId
            };

            _repositoryManager.HotelsRepository.Edit(hotel);
            if (hotel == null)
            {
                _logger.LogError("Hotels when update object sent from client is null");
                return BadRequest("Internal edit record hotel error");
            }


            var result = new HotelsDto
            {
                HotelName = hotel.HotelName,
                HotelDescription = hotel.HotelDescription,
                // HotelStatus = hotel.HotelStatus ? "available" : "unavailable",
                
                HotelReasonStatus = hotel.HotelReasonStatus,
                HotelRatingStar = hotel.HotelRatingStar,
                HotelPhonenumber = hotel.HotelPhonenumber,
                HotelModifiedDate = hotel.HotelModifiedDate,
                HotelAddrId = hotel.HotelAddrId
            };

            return Ok(result);
        }


        // PUT api/<HotelsController>/switch/5
        [HttpPut("switch/{id}")]
        public IActionResult Put(int id, [FromBody] HotelSwitchDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Hotels when update object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotel = new Hotels()
            {
                HotelId = id,
                HotelStatus = dto.HotelStatus == "available" ? true : false,
                HotelReasonStatus = string.IsNullOrEmpty(dto.HotelReasonStatus) ? string.Empty : dto.HotelReasonStatus,
            };

            _repositoryManager.HotelsRepository.EditStatus(hotel);

            var dataResult = _repositoryManager.HotelsRepository.FindHotelsById(id);

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
        public IActionResult Delete(int id)
        {
            //1. Id cannot be null
            if (id == null)
            {
                _logger.LogError("Id Hotels object sent from client is null");
                return BadRequest("Id Record doesn't exist or wrong parameter");
            }

            //2. Find id first
            var hotel = _repositoryManager.HotelsRepository.FindHotelsById(id);
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
