using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Realta.Contract.Models.Hotels;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Persistence.Interface;
using Realta.Services.Abstraction;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
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

            var hotelDto = hotels.Select(h => new HotelsDto
            {
                hotel_id = h.hotel_id,
                hotel_name = h.hotel_name,
                hotel_rating_star = h.hotel_rating_star,
                hotel_phonenumber = h.hotel_phonenumber,
                hotel_status = h.hotel_status ? "available" : "unavailable",
                hotel_modified_date = h.hotel_modified_date,
            });

            return Ok(hotelDto);
        }

        // GET api/<HotelsController>/search/{name}
        [HttpGet("search/{name}", Name = "GetHotelsByName")]
        public IActionResult GetHotelByName(string name)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsByName(name);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }


            var hotelDto = hotels.Select(h => new HotelsDto
            {
                hotel_id = h.hotel_id,
                hotel_name = h.hotel_name,
                hotel_rating_star = h.hotel_rating_star,
                hotel_phonenumber = h.hotel_phonenumber,
                hotel_status = h.hotel_status ? "available" : "unavailable",
                hotel_modified_date = h.hotel_modified_date,
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
                hotel_id = hotels.hotel_id,
                hotel_name = hotels.hotel_name,
                hotel_rating_star = hotels.hotel_rating_star,
                hotel_phonenumber = hotels.hotel_phonenumber,
                hotel_status = hotels.hotel_status ? "available" : "unavailable",
                hotel_modified_date = hotels.hotel_modified_date
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
                hotel_name = dto.hotel_name,
                hotel_description = dto.hotel_description,
                hotel_status = dto.hotel_status == "available" ? true : false,
                hotel_rating_star = dto.hotel_rating_star,
                hotel_phonenumber = dto.hotel_phonenumber,
                hotel_addr_id = (int)dto.hotel_addr_id
            };

            //post data to db
            _repositoryManager.HotelsRepository.Insert(hotel);

            var result = _repositoryManager.HotelsRepository.FindHotelsById(hotel.hotel_id);


            var resDto = new HotelsDto
            {
                hotel_id = hotel.hotel_id,
                hotel_name = hotel.hotel_name,
                hotel_description = hotel.hotel_description,
                hotel_status = hotel.hotel_status ? "available" : "unavailable",
                hotel_reason_status = hotel.hotel_reason_status,
                hotel_rating_star = hotel.hotel_rating_star,
                hotel_phonenumber = hotel.hotel_phonenumber,
                hotel_modified_date = hotel.hotel_modified_date,
                hotel_addr_id = hotel.hotel_addr_id
            };
            //forward 
            return CreatedAtRoute("GetHotelsById", new { id = resDto.hotel_id }, resDto);
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public IActionResult EditHotel (int id, [FromBody] HotelsDto dto)
        {

            if (dto == null)
            {
                _logger.LogError("Hotels when update object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotel = new Hotels()
            {
                hotel_id = id,
                hotel_name = dto.hotel_name,
                hotel_description = dto.hotel_description,
                hotel_status = dto.hotel_status == "available" ? true : false,
                hotel_rating_star = dto.hotel_rating_star,
                hotel_phonenumber = dto.hotel_phonenumber,
                hotel_addr_id = (int)dto.hotel_addr_id
            };

            _repositoryManager.HotelsRepository.Edit(hotel);
            if (hotel == null)
            {
                _logger.LogError("Hotels when update object sent from client is null");
                return BadRequest("Internal edit record hotel error");
            }


            var result = new HotelsDto
            {
                hotel_id = hotel.hotel_id,
                hotel_name = hotel.hotel_name,
                hotel_description = hotel.hotel_description,
                hotel_status = hotel.hotel_status ? "available" : "unavailable",
                hotel_reason_status = hotel.hotel_reason_status,
                hotel_rating_star = hotel.hotel_rating_star,
                hotel_phonenumber = hotel.hotel_phonenumber,
                hotel_modified_date = hotel.hotel_modified_date,
                hotel_addr_id = hotel.hotel_addr_id
            };

            return CreatedAtRoute("GetHotelsById", new { id = result.hotel_id }, result);
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
                hotel_id = id,
                hotel_status = dto.hotel_status == "available" ? true : false,
                hotel_reason_status = string.IsNullOrEmpty(dto.hotel_reason_status) ? string.Empty : dto.hotel_reason_status,
            };

            _repositoryManager.HotelsRepository.EditStatus(hotel);

            var dataResult = _repositoryManager.HotelsRepository.FindHotelsById(id);

            var result = new HotelsDto
            {
                hotel_id = hotel.hotel_id,
                hotel_name = dataResult.hotel_name,
                hotel_status = hotel.hotel_status ? "available" : "unavailable",
                hotel_reason_status = hotel.hotel_reason_status,
                hotel_rating_star = dataResult.hotel_rating_star,
                hotel_phonenumber = dataResult.hotel_phonenumber,
                hotel_modified_date = dataResult.hotel_modified_date
            };

            return CreatedAtRoute("GetHotelsById", new { id = result.hotel_id }, result);
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
                _logger.LogError($"Region with id {id} Record doesn't exist or wrong parameter");
                return NotFound();
            }

            _repositoryManager.HotelsRepository.Remove(hotel);
            return Ok("Data Has Been Remove.");
        }
    }
}
