using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Hotels;
using Realta.Contract.Models.v1.Reviews;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers.v1
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
            var hotels = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotelReviews = await _repositoryManager.HotelReviewsRepository.FindAllHotelReviewsAsync(hotelId);


            var hotelDto = new HotelsDto
            {
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
            };

            if (hotelReviews.Count() == 0)
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    reviews = "there aren\'t any reviews for this hotel yet"
                });
            }
            else
            {
                var hotelReviewsDto = hotelReviews.Select(hr => new HotelReviewsDto
                {
                    HoreId = hr.HoreId,
                    HoreUserReview = hr.HoreUserReview,
                    // HoreUserId = hr.HoreUserId,
                    HoreRating = hr.HoreRating,
                    HoreCreatedOn = hr.HoreCreatedOn,
                    // HoreHotelId = hr.HoreHotelId
                });

                //var result = new HotelReviewsAllDto()
                //{
                //    Hotels = hotelDto,
                //    Reviews = hotelReviewsDto
                //};
                return Ok(hotelReviewsDto);
            }


        }

        // GET api/<HotelReviewsController>/5
        [HttpGet("{hotelId}/reviews/{hotelReviewsId}", Name = "GetHotelReviewsById")]
        public async Task<IActionResult> GetHotelReviewsAsync(int hotelId, int hotelReviewsId)
        {
            var hotels = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            var hotelDto = new HotelsDto
            {
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
            };

            var hotelReviews = await _repositoryManager.HotelReviewsRepository.FindHotelReviewsByIdAsync(hotelId, hotelReviewsId);

            if (hotelReviews != null)
            {
                var hotelReviewsDto = new HotelReviewsDto()
                {
                    HoreId = hotelReviews.HoreId,
                    HoreUserReview = hotelReviews.HoreUserReview,
                    HoreUserId = hotelReviews.HoreUserId,
                    HoreRating = hotelReviews.HoreRating,
                    HoreCreatedOn = hotelReviews.HoreCreatedOn,
                    HoreHotelId = hotelReviews.HoreHotelId
                };

                var result = new HotelReviewsByIdDto()
                {
                    Hotels = hotelDto,
                    Reviews = hotelReviewsDto
                };
                return Ok(result);
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
            var hotels = _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
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

            var hotelReviews = new HotelReviews()
            {
                HoreHotelId = hotelId,
                HoreUserReview = dto.HoreUserReview,
                HoreRating = dto.HoreRating,
                HoreUserId = dto.HoreUserId
            };

            //post data to db
            _repositoryManager.HotelReviewsRepository.Insert(hotelReviews);

            var result = await _repositoryManager.HotelReviewsRepository.FindHotelReviewsByIdAsync(hotelId, hotelReviews.HoreId);


            var resDto = new HotelReviewsDto()
            {
                HoreId = result.HoreId,
                HoreUserReview = result.HoreUserReview,
                HoreUserId = result.HoreUserId,
                HoreRating = result.HoreRating,
                HoreCreatedOn = result.HoreCreatedOn,
                HoreHotelId = result.HoreHotelId
            };

            //forward 
            return Ok(resDto);
        }

        // PUT api/<HotelReviewsController>/5
        [HttpPut("{hotelId}/reviews/{hotelReviewsId}")]
        public async Task<IActionResult> Put(int hotelId, int hotelReviewsId, [FromBody] HotelReviewsDto dto)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
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

            var hotelReviews = new HotelReviews()
            {
                HoreId = hotelReviewsId,
                HoreHotelId = hotelId,
                HoreUserReview = dto.HoreUserReview,
                HoreRating = dto.HoreRating,
                HoreUserId = dto.HoreUserId
            };

            _repositoryManager.HotelReviewsRepository.Edit(hotelReviews);

            var dataResult = await _repositoryManager.HotelReviewsRepository.FindHotelReviewsByIdAsync(hotelId, hotelReviewsId);

            var resDto = new HotelReviewsDto()
            {
                HoreId = dataResult.HoreId,
                HoreUserReview = dataResult.HoreUserReview,
                HoreUserId = dataResult.HoreUserId,
                HoreRating = dataResult.HoreRating,
                HoreCreatedOn = dataResult.HoreCreatedOn,
                HoreHotelId = dataResult.HoreHotelId
            };

            return Ok(resDto);
        }

        [HttpGet("{hotelId}/reviews/pageList/")]

        public async Task<IActionResult> GetFacilitiesPageList([FromQuery] ReviewsParameters reviewsParameters, int hotelId)
        {

            var reviews = await _repositoryManager.HotelReviewsRepository.GetReviewsPageList(reviewsParameters, hotelId);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(reviews.MetaData));

            return Ok(reviews);
        }


        // DELETE api/<HotelReviewsController>/5
        [HttpDelete("{hotelId}/reviews/{hotelReviewsId}")]
        public async Task<IActionResult> DeleteAsync(int hotelId, int hotelReviewsId)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
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
