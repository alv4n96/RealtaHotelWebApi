using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Realta.Contract.Models;
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



        // GET: api/<HotelsController>
        [HttpGet]
        public async Task<IActionResult> GetAllHotelAsync()
        {
            var hotels = await _repositoryManager.HotelsRepository.FindAllHotelsAsync();
            return Ok(hotels.ToList());
        }

        // GET api/<HotelsController>/5
        [HttpGet("{id}", Name = "GetHotels")]
        public IActionResult GetHotel(int id)
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
                hotel_description = hotels.hotel_description,
                hotel_rating_star = hotels.hotel_rating_star,
                hotel_phonenumber = hotels.hotel_phonenumber,
                hotel_modified_date = hotels.hotel_modified_date,
                hotel_addr_id = hotels.hotel_addr_id
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
                hotel_rating_star = dto.hotel_rating_star,
                hotel_phonenumber = dto.hotel_phonenumber,
                hotel_addr_id = dto.hotel_addr_id
            };

            //post data to db
            _repositoryManager.HotelsRepository.Insert(hotel);

            var result = _repositoryManager.HotelsRepository.FindHotelsById(hotel.hotel_id);

            //forward 
            return CreatedAtRoute("GetHotels", new { id = hotel.hotel_id }, result);
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] HotelsDto dto)
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
                hotel_rating_star = dto.hotel_rating_star,
                hotel_phonenumber = dto.hotel_phonenumber,
                hotel_addr_id = dto.hotel_addr_id
            };

            _repositoryManager.HotelsRepository.Edit(hotel);

            return CreatedAtRoute("GetHotels", new { id = hotel.hotel_id }, hotel);
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
            var region = _repositoryManager.HotelsRepository.FindHotelsById(id);
            if (region == null)
            {
                _logger.LogError($"Region with id {id} Record doesn't exist or wrong parameter");
                return NotFound();
            }

            _repositoryManager.HotelsRepository.Remove(region);
            return Ok("Data Has Been Remove.");
        }
    }
}
