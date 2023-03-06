using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Contract.Models.Hotels;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers.v1
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
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
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
                    FaphId = faph.FaphId,
                    FaphStartdate = faph.FaphStartdate,
                    FaphEnddate = faph.FaphEnddate,
                    FaphLowPrice = faph.FaphLowPrice,
                    FaphHighPrice = faph.FaphHighPrice,
                    FaphRatePrice = faph.FaphRatePrice,
                    FaphDiscount = faph.FaphDiscount,
                    FaphTaxRate = faph.FaphTaxRate,
                    FaphModifiedDate = faph.FaphModifiedDate
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
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
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
                    FaphId = faph.FaphId,
                    FaphStartdate = faph.FaphStartdate,
                    FaphEnddate = faph.FaphEnddate,
                    FaphLowPrice = faph.FaphLowPrice,
                    FaphHighPrice = faph.FaphHighPrice,
                    FaphRatePrice = faph.FaphRatePrice,
                    FaphDiscount = faph.FaphDiscount,
                    FaphTaxRate = faph.FaphTaxRate,
                    FaphModifiedDate = faph.FaphModifiedDate
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
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
            };

            var facilityPriceHistory = await _repositoryManager.FacilityPriceHistoryRepository.FindAllFacilityPriceHistoryByIdAsync(faciId, faphId);

            if (facilityPriceHistory != null)
            {
                var facilityPriceHistoryDto = new FacilityPriceHistoryDto
                {
                    FaphId = facilityPriceHistory.FaphId,
                    FaphStartdate = facilityPriceHistory.FaphStartdate,
                    FaphEnddate = facilityPriceHistory.FaphEnddate,
                    FaphLowPrice = facilityPriceHistory.FaphLowPrice,
                    FaphHighPrice = facilityPriceHistory.FaphHighPrice,
                    FaphRatePrice = facilityPriceHistory.FaphRatePrice,
                    FaphDiscount = facilityPriceHistory.FaphDiscount,
                    FaphTaxRate = facilityPriceHistory.FaphTaxRate,
                    FaphModifiedDate = facilityPriceHistory.FaphModifiedDate
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
