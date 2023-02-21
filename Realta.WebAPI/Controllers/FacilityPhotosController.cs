using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models.Hotels;
using Realta.Contract.Models;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;
using Realta.Domain.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityPhotosController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;

        public FacilityPhotosController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        // GET: api/<FacilityPhotosController>
        [HttpGet("{hotelId}/facilities/{faciId}/photos")]
        public async Task<IActionResult> GetAsync(int hotelId, int faciId)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Hotel doesn't exist or wrong parameter");
            }

            var facilities = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, faciId);
            if (facilities == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Facility doesn't exist or wrong parameter");
            }
            var facilityPhotos = await _repositoryManager.FacilityPhotosRepository.FindAllFacilityPhotosAsync(faciId);


            var hotelDto = new HotelsDto
            {
                hotel_id = hotels.hotel_id,
                hotel_name = hotels.hotel_name,
                hotel_rating_star = hotels.hotel_rating_star,
                hotel_phonenumber = hotels.hotel_phonenumber,
            };

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

            if (facilityPhotos.Count() == 0)
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    facility = facilitiesDto,
                    photos = "there aren\'t any facility photos for this facility yet"
                });
            }
            else
            {
                var facilityPhotosDto = facilityPhotos.Select(f => new FacilityPhotosDto
                {
                    fapho_id = f.fapho_id,
                    fapho_thumbnail_filename = f.fapho_thumbnail_filename,
                    fapho_photo_filename = f.fapho_photo_filename,
                    fapho_primary = f.fapho_primary,
                    fapho_url = f.fapho_url,
                    fapho_modified_date = f.fapho_modified_date,
                });
                return Ok(new
                {
                    hotel = hotelDto,
                    facility = facilitiesDto,
                    photos = facilityPhotosDto
                });
            }
        }

        // GET api/<FacilityPhotosController>/5
        [HttpGet("{hotelId}/facilities/{faciId}/photos/{faphoId}")]
        public async Task<IActionResult> GetByIdAsync(int hotelId, int faciId, int faphoId)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Hotel doesn't exist or wrong parameter");
            }

            var facilities = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, faciId);
            if (facilities == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Facility doesn't exist or wrong parameter");
            }

            var hotelDto = new HotelsDto
            {
                hotel_id = hotels.hotel_id,
                hotel_name = hotels.hotel_name,
                hotel_rating_star = hotels.hotel_rating_star,
                hotel_phonenumber = hotels.hotel_phonenumber,
            };

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

            var facilityPhotos = await _repositoryManager.FacilityPhotosRepository.FindFacilityPhotosByIdAsync(faciId, faphoId);

            if (facilityPhotos != null)
            {
                var facilityPhotosDto = new FacilityPhotosDto
                {
                    fapho_id = facilityPhotos.fapho_id,
                    fapho_thumbnail_filename = facilityPhotos.fapho_thumbnail_filename,
                    fapho_photo_filename = facilityPhotos.fapho_photo_filename,
                    fapho_primary = facilityPhotos.fapho_primary,
                    fapho_url = facilityPhotos.fapho_url,
                    fapho_modified_date = facilityPhotos.fapho_modified_date
                };
                return Ok(new
                {
                    hotel = hotelDto,
                    facility = facilitiesDto,
                    photos = facilityPhotosDto
                });
            }
            else
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    facilities = facilitiesDto,
                    photos = "Data Not Found!"
                });
            }
        }

        // POST api/<FacilityPhotosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FacilityPhotosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacilityPhotosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
