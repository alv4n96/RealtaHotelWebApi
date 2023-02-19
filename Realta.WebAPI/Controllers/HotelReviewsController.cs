using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Contract.Models.Hotels;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelReviewsController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;

        public HotelReviewsController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }




        // GET: api/<HotelReviewsController>
        [HttpGet("{hotelId}/reviews")]
        public async Task<IActionResult> GetAsync(int hotelId)
        {

            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotelReviews = await _repositoryManager.HotelReviewsRepository.FindAllHotelReviewsAsync(hotelId);
            if (hotelReviews == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Data Not Found!");
            }

            var hotelReviewsDto = hotelReviews.Select(hr => new HotelReviewsDto
            {
                hore_id = hr.hore_id,
                hore_user_review = hr.hore_user_review,
                hore_user_id = hr.hore_user_id,
                hore_rating = hr.hore_rating,
                hore_created_on = hr.hore_created_on,
                hore_hotel_id = hr.hore_hotel_id
            });

            var hotelDto = new HotelsDto
            {
                hotel_id = hotels.hotel_id,
                hotel_name = hotels.hotel_name,
                hotel_rating_star = hotels.hotel_rating_star,
                hotel_phonenumber = hotels.hotel_phonenumber,
                hotel_status = hotels.hotel_status ? "available" : "unavailable",
                hotel_modified_date = hotels.hotel_modified_date
            };

            var result = new {
                //hotel = hotelDto,
                reviews = hotelReviewsDto
            };


            return Ok(result);

        }

        // GET api/<HotelReviewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HotelReviewsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HotelReviewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HotelReviewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
