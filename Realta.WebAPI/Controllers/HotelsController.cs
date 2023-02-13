using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Persistence.Interface;
using Realta.Services.Abstraction;

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

            /* -- Synchronous
            var hotels = _repositoryManager.HotelsRepository.FindAllHotels().ToList();

            var hotelsDto = hotels.Select(h => new HotelsDto
            {
                hotel_id = h.hotel_id,
                hotel_name = h.hotel_name,
                hotel_description = h.hotel_description,
                hotel_rating_star = h.hotel_rating_star,
                hotel_phonenumber = h.hotel_phonenumber,
                hotel_modified_date = h.hotel_modified_date,
                hotel_addr_id = h.hotel_addr_id
            });

            return Ok(hotelsDto);
            */
        }

        // GET api/<HotelsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HotelsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
