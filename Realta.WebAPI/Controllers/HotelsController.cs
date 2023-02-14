using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{id}")]
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
                hotel_id = dto.hotel_id,
                hotel_name = dto.hotel_name,
                hotel_description = dto.hotel_description,
                hotel_rating_star = dto.hotel_rating_star,
                hotel_phonenumber = dto.hotel_phonenumber,
                hotel_modified_date = dto.hotel_modified_date,
                hotel_addr_id = dto.hotel_addr_id
            }
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
