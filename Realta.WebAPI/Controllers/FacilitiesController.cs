using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Contract.Models.Hotels;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.Entities.Enum;
using Realta.Services.Abstraction;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;

        public FacilitiesController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }


        // GET: api/<FacilitiesController>
        [HttpGet("{hotelId}/facilities")]
        public async Task<IActionResult> GetAllFacilitiesAsync(int hotelId)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }
            var facilities = await _repositoryManager.FacilitiesRepository.FindAllFacilitiesAsync(hotelId);

            var hotelDto = new HotelsDto
            {
                hotel_id = hotels.hotel_id,
                hotel_name = hotels.hotel_name,
                hotel_rating_star = hotels.hotel_rating_star,
                hotel_phonenumber = hotels.hotel_phonenumber,
            };

            if (facilities.Count() == 0)
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    reviews = "there aren\'t any facility for this hotel yet"
                });
            }
            else
            {
                var hotelReviewsDto = facilities.Select(f => new FacilitiesDto
                {
                    faci_id = f.faci_id,
                    faci_name = f.faci_name,
                    faci_room_number = f.faci_room_number,
                    faci_max_number = f.faci_max_number,
                    faci_measure_unit = f.faci_measure_unit,
                    faci_startdate = f.faci_startdate,
                    faci_endate = f.faci_endate,
                    faci_low_price = f.faci_low_price,
                    faci_high_price = f.faci_high_price,
                    faci_discount = f.faci_discount,
                    faci_rate_price = f.faci_rate_price,
                    faci_tax_rate = f.faci_tax_rate
                });
                return Ok(new
                {
                    hotel = hotelDto,
                    reviews = hotelReviewsDto
                });
            }
        }

        // GET api/<FacilitiesController>/5
        [HttpGet("{hotelId}/facilities/{faciId}", Name = "GetHotelFaciById")]
        public async Task<IActionResult> GetFacilitiesByIdAsync(int hotelId, int faciId)
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

            var facilities = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, faciId);

            if (facilities != null)
            {
                var facilitiesDto = new FacilitiesDto()
                {
                    faci_id = facilities.faci_id,
                    faci_name = facilities.faci_name,
                    faci_room_number = facilities.faci_room_number,
                    faci_max_number = facilities.faci_max_number,
                    faci_measure_unit = facilities.faci_measure_unit,
                    faci_startdate = facilities.faci_startdate,
                    faci_endate = facilities.faci_endate,
                    faci_low_price = facilities.faci_low_price,
                    faci_high_price = facilities.faci_high_price,
                    faci_discount = facilities.faci_discount,
                    faci_rate_price = facilities.faci_rate_price,
                    faci_tax_rate = facilities.faci_tax_rate
                };
                return Ok(new
                {
                    hotel = hotelDto,
                    facilities = facilitiesDto
                });
            }
            else
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    facilities = "Data Not Found!"
                });
            }
        }

        // POST api/<FacilitiesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FacilitiesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacilitiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
