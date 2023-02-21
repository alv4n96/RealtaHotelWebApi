using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
        [HttpPost("{hotelId}/facilities/")]
        public async Task<IActionResult> Post(int hotelId, [FromBody] FacilitiesDto dto)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            if (String.IsNullOrEmpty(dto.faci_measure_unit) || (dto.faci_measure_unit != "people" && dto.faci_measure_unit != "beds"))
            {
                _logger.LogError("facility measure unit got wrong parameter");
                return BadRequest("facility measure unit should fill : people or beds");
            }

            if (dto == null)
            {
                _logger.LogError("Hotel region object sent from client is null");
                return BadRequest("Some parameters are missing");
            }

            var facilities = new Facilities()
            {
                faci_name= dto.faci_name,
                faci_description= string.IsNullOrEmpty(dto.faci_description) ? string.Empty : dto.faci_description,
                faci_max_number = dto.faci_max_number == null ? 0 : dto.faci_max_number,
                faci_measure_unit = string.IsNullOrEmpty(dto.faci_measure_unit) ? string.Empty : dto.faci_measure_unit,
                faci_room_number = dto.faci_room_number,
                faci_startdate = dto.faci_startdate,
                faci_endate = dto.faci_endate,
                faci_low_price = dto.faci_low_price,
                faci_high_price = dto.faci_high_price,
                faci_discount = (decimal)(dto.faci_discount == null ? 0 : dto.faci_discount),
                faci_tax_rate = (decimal)(dto.faci_tax_rate == null ? 0 : dto.faci_tax_rate),
                faci_cagro_id = dto.faci_cagro_id,
                faci_hotel_id = hotelId,
                faci_user_id = dto.faci_user_id,
            };

            //post data to db
            _repositoryManager.FacilitiesRepository.Insert(facilities);

            var result = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, facilities.faci_id);


            var resDto = new FacilitiesDto()
            {
                faci_id = result.faci_id,
                faci_name = result.faci_name,
                faci_room_number = result.faci_room_number,
                faci_max_number = result.faci_max_number,
                faci_measure_unit = result.faci_measure_unit,
                faci_startdate = result.faci_startdate,
                faci_endate = result.faci_endate,
                faci_low_price = result.faci_low_price,
                faci_high_price = result.faci_high_price,
                faci_discount = result.faci_discount,
                faci_rate_price = result.faci_rate_price,
                faci_tax_rate = result.faci_tax_rate,
                faci_cagro_id = result.faci_cagro_id,
                faci_user_id = result.faci_user_id,
            };

            //forward 
            return CreatedAtRoute("GetHotelFaciById", new { hotelId = result.faci_hotel_id, faciId = result.faci_id }, resDto);
        }

        // PUT api/<FacilitiesController>/5
        [HttpPut("{hotelId}/facilities/{faciId}")]
        public async Task<IActionResult> Put(int hotelId,int faciId, [FromBody] FacilitiesDto dto)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            if (String.IsNullOrEmpty(dto.faci_measure_unit) || (dto.faci_measure_unit != "people" && dto.faci_measure_unit != "beds"))
            {
                _logger.LogError("facility measure unit got wrong parameter");
                return BadRequest("facility measure unit should fill : people or beds");
            }

            if (dto == null)
            {
                _logger.LogError("Hotel region object sent from client is null");
                return BadRequest("Some parameters are missing");
            }

            var facilities = new Facilities()
            {
                faci_id = faciId,
                faci_name = dto.faci_name,
                faci_description = string.IsNullOrEmpty(dto.faci_description) ? string.Empty : dto.faci_description,
                faci_max_number = dto.faci_max_number == null ? 0 : dto.faci_max_number,
                faci_measure_unit = string.IsNullOrEmpty(dto.faci_measure_unit) ? string.Empty : dto.faci_measure_unit,
                faci_room_number = dto.faci_room_number,
                faci_startdate = dto.faci_startdate,
                faci_endate = dto.faci_endate,
                faci_low_price = dto.faci_low_price,
                faci_high_price = dto.faci_high_price,
                faci_discount = (decimal)(dto.faci_discount == null ? 0 : dto.faci_discount),
                faci_tax_rate = (decimal)(dto.faci_tax_rate == null ? 0 : dto.faci_tax_rate),
                faci_cagro_id = dto.faci_cagro_id,
                faci_hotel_id = hotelId,
                faci_user_id = dto.faci_user_id,
            };

            _repositoryManager.FacilitiesRepository.Edit(facilities);

            var result = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, facilities.faci_id);


            var resDto = new FacilitiesDto()
            {
                faci_id = result.faci_id,
                faci_name = result.faci_name,
                faci_room_number = result.faci_room_number,
                faci_max_number = result.faci_max_number,
                faci_measure_unit = result.faci_measure_unit,
                faci_startdate = result.faci_startdate,
                faci_endate = result.faci_endate,
                faci_low_price = result.faci_low_price,
                faci_high_price = result.faci_high_price,
                faci_discount = result.faci_discount,
                faci_rate_price = result.faci_rate_price,
                faci_tax_rate = result.faci_tax_rate,
                faci_cagro_id = result.faci_cagro_id,
                faci_user_id = result.faci_user_id,
            };

            //forward 
            return CreatedAtRoute("GetHotelFaciById", new { hotelId = result.faci_hotel_id, faciId = result.faci_id }, resDto);
        }

        // DELETE api/<FacilitiesController>/5
        [HttpDelete("{hotelId}/facilities/{faciId}")]
        public async Task<IActionResult> Delete(int hotelId, int faciId)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            //2. Find id first
            var facilities = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, faciId);
            if (facilities == null)
            {
                _logger.LogError($"Reviews with id {faciId} Record doesn't exist or wrong parameter");
                return NotFound();
            }

            _repositoryManager.FacilitiesRepository.Remove(facilities);
            return Ok("Data Has Been Remove.");
        }
    }
}
