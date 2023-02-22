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
    public class FacilityPriceHistoryController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;

        public FacilityPriceHistoryController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        // GET: api/<FacilityPriceHistoryController>
        [HttpGet("{hotelId}/history")]
        public async Task<IActionResult> GetFacilityPriceHistoryByHotel(int hotelId)
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

            var facilityPriceHistory = await _repositoryManager.FacilityPriceHistoryRepository.FindAllFacilityPriceHistoryAsync(hotelId);

            if (facilityPriceHistory.Count() == 0)
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    history = "there aren\'t any history for this hotel yet"
                });
            }
            else
            {
                var facilityPriceHistoryDto = facilityPriceHistory.Select(faph => new FacilityPriceHistoryDto
                {
                    faph_id = faph.faph_id,
                    faph_startdate = faph.faph_startdate,
                    faph_enddate = faph.faph_enddate,
                    faph_low_price = faph.faph_low_price,
                    faph_high_price = faph.faph_high_price,
                    faph_rate_price = faph.faph_rate_price,
                    faph_discount = faph.faph_discount,
                    faph_tax_rate = faph.faph_tax_rate,
                    faph_modified_date = faph.faph_modified_date
                });

                return Ok(new
                {
                    hotel = hotelDto,
                    history = facilityPriceHistoryDto
                });
            }
        }

        // GET: api/<FacilityPriceHistoryController>
        [HttpGet("{hotelId}/facility/{faciId}/history")]
        public async Task<IActionResult> GetFacilityPriceHistoryByFacilityIdAsync(int hotelId, int faciId)
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

            var facilityPriceHistory = await _repositoryManager.FacilityPriceHistoryRepository.FindAllFacilityPriceHistoryByFacilityAsync(hotelId, faciId);

            if (facilityPriceHistory.Count() == 0)
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    history = "there aren\'t any history for this facility yet"
                });
            }
            else
            {
                var facilityPriceHistoryDto = facilityPriceHistory.Select(faph => new FacilityPriceHistoryDto
                {
                    faph_id = faph.faph_id,
                    faph_startdate = faph.faph_startdate,
                    faph_enddate = faph.faph_enddate,
                    faph_low_price = faph.faph_low_price,
                    faph_high_price = faph.faph_high_price,
                    faph_rate_price = faph.faph_rate_price,
                    faph_discount = faph.faph_discount,
                    faph_tax_rate = faph.faph_tax_rate,
                    faph_modified_date = faph.faph_modified_date
                });

                return Ok(new
                {
                    hotel = hotelDto,
                    history = facilityPriceHistoryDto
                });
            }
        }

        // GET api/<FacilityPriceHistoryController>/5
        [HttpGet("{hotelId}/facility/{faciId}/price/{faphId}/facility")]
        public async Task<IActionResult> GetFacilityPriceHistoryByFacilityPriceHistoryIdAsync(int hotelId, int faciId, int faphId)
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

            var facilityPriceHistory = await _repositoryManager.FacilityPriceHistoryRepository.FindAllFacilityPriceHistoryByIdAsync(faciId, faphId);

            if (facilityPriceHistory != null)
            {
                var facilityPriceHistoryDto =  new FacilityPriceHistoryDto
                {
                    faph_id = facilityPriceHistory.faph_id,
                    faph_startdate = facilityPriceHistory.faph_startdate,
                    faph_enddate = facilityPriceHistory.faph_enddate,
                    faph_low_price = facilityPriceHistory.faph_low_price,
                    faph_high_price = facilityPriceHistory.faph_high_price,
                    faph_rate_price = facilityPriceHistory.faph_rate_price,
                    faph_discount = facilityPriceHistory.faph_discount,
                    faph_tax_rate = facilityPriceHistory.faph_tax_rate,
                    faph_modified_date = facilityPriceHistory.faph_modified_date
                };

                return Ok(new
                {
                    hotel = hotelDto,
                    history = facilityPriceHistoryDto
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
    }
}
