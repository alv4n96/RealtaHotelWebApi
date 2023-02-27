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


            var hotelDto = new HotelsDto
            {
                hotel_id = hotels.hotel_id,
                hotel_name = hotels.hotel_name,
                hotel_rating_star = hotels.hotel_rating_star,
                hotel_phonenumber = hotels.hotel_phonenumber,
            };

            if (hotelReviews.Count() == 0)
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    reviews = "there aren\'t any reviews for this hotel yet"
                }) ;
            } 
            else
            {
                var hotelReviewsDto = hotelReviews.Select(hr => new HotelReviewsDto
                {
                    hore_id = hr.hore_id,
                    hore_user_review = hr.hore_user_review,
                    hore_user_id = hr.hore_user_id,
                    hore_rating = hr.hore_rating,
                    hore_created_on = hr.hore_created_on,
                    hore_hotel_id = hr.hore_hotel_id
                });
                return Ok(new
                {
                    hotel = hotelDto,
                    reviews = hotelReviewsDto
                });
            }


        }

        // GET api/<HotelReviewsController>/5
        [HttpGet("{hotelId}/reviews/{hotelReviewsId}", Name = "GetHotelReviewsById")]
        public async Task<IActionResult> GetHotelReviewsAsync(int hotelId, int hotelReviewsId)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
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
            };

            var hotelReviews = await _repositoryManager.HotelReviewsRepository.FindHotelReviewsByIdAsync(hotelId, hotelReviewsId);

            if (hotelReviews != null)
            {
                var hotelReviewsDto = new HotelReviewsDto()
                {
                    hore_id = hotelReviews.hore_id,
                    hore_user_review = hotelReviews.hore_user_review,
                    hore_user_id = hotelReviews.hore_user_id,
                    hore_rating = hotelReviews.hore_rating,
                    hore_created_on = hotelReviews.hore_created_on,
                    hore_hotel_id = hotelReviews.hore_hotel_id
                };
                return Ok(new
                {
                    hotel = hotelDto,
                    reviews = hotelReviewsDto
                });
            }
            else
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    reviews = "Data Not Found!"
                });
            }
        }

        // POST api/<HotelReviewsController>
        [HttpPost("{hotelId}/reviews/")]
        public async Task<IActionResult> PostAsync(int hotelId, [FromBody] HotelReviewsDto dto)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            if (dto == null)
            {
                _logger.LogError("Hotel region object sent from client is null");
                return BadRequest("Some parameters are missing");
            }

            var hotelReviews = new Hotel_Reviews()
            {
                hore_hotel_id = hotelId,
                hore_user_review = dto.hore_user_review,
                hore_rating = dto.hore_rating,
                hore_user_id = dto.hore_user_id
            };

            //post data to db
            _repositoryManager.HotelReviewsRepository.Insert(hotelReviews);

            var result = await _repositoryManager.HotelReviewsRepository.FindHotelReviewsByIdAsync(hotelId, hotelReviews.hore_id);


            var resDto = new HotelReviewsDto()
            {   
                hore_id = result.hore_id,
                hore_user_review = result.hore_user_review,
                hore_user_id = result.hore_user_id,
                hore_rating = result.hore_rating,
                hore_created_on = result.hore_created_on,
                hore_hotel_id = result.hore_hotel_id
            };

            //forward 
            return Ok(resDto);
        }

        // PUT api/<HotelReviewsController>/5
        [HttpPut("{hotelId}/reviews/{hotelReviewsId}")]
        public async Task<IActionResult> Put(int hotelId, int hotelReviewsId, [FromBody] HotelReviewsDto dto)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            if (dto == null)
            {
                _logger.LogError("Hotel region object sent from client is null");
                return BadRequest("Some parameters are missing");
            }

            var hotelReviews = new Hotel_Reviews()
            {
                hore_id = hotelReviewsId,
                hore_hotel_id = hotelId,
                hore_user_review = dto.hore_user_review,
                hore_rating = dto.hore_rating,
                hore_user_id = dto.hore_user_id
            };

            _repositoryManager.HotelReviewsRepository.Edit(hotelReviews);

            var dataResult = await _repositoryManager.HotelReviewsRepository.FindHotelReviewsByIdAsync(hotelId, hotelReviewsId);

            var resDto = new HotelReviewsDto()
            {
                hore_id = dataResult.hore_id,
                hore_user_review = dataResult.hore_user_review,
                hore_user_id = dataResult.hore_user_id,
                hore_rating = dataResult.hore_rating,
                hore_created_on = dataResult.hore_created_on,
                hore_hotel_id = dataResult.hore_hotel_id
            };

            return Ok(resDto);
        }

        // DELETE api/<HotelReviewsController>/5
        [HttpDelete("{hotelId}/reviews/{hotelReviewsId}")]
        public async Task<IActionResult> DeleteAsync(int hotelId, int hotelReviewsId)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            //2. Find id first
            var hotelReviews = await _repositoryManager.HotelReviewsRepository.FindHotelReviewsByIdAsync(hotelId, hotelReviewsId);
            if (hotelReviews == null)
            {
                _logger.LogError($"Reviews with id {hotelReviewsId} Record doesn't exist or wrong parameter");
                return NotFound();
            }

            _repositoryManager.HotelReviewsRepository.Remove(hotelReviews);
            return Ok("Data Has Been Remove.");
        }
    }
}
