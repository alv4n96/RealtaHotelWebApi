using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.History;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.Base;
using Realta.Domain.RequestFeatures.HotelParameters;
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

                var result = new HotelFaciHistoryGetAllDto()
                {
                    Hotels = hotelDto,
                    History = facilityPriceHistoryDto
                };
                return Ok(result);
            }
        }

        // GET: api/<FacilityPriceHistoryController>
        [HttpGet("{hotelId}/facility/{faciId}/history")]
        public async Task<IActionResult> GetFacilityPriceHistoryByFacilityIdAsync(int hotelId, int faciId)
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
            var facilities = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, faciId);

            if (facilities == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Facilities doesn't exist or wrong parameter");
            }

            var facilitiesDto = new FacilitiesDto()
                {
                    FaciId = facilities.FaciId,
                    FaciName = facilities.FaciName,
                    FaciRoomNumber = facilities.FaciRoomNumber,
                    FaciMaxNumber = facilities.FaciMaxNumber,
                    FaciMeasureUnit = facilities.FaciMeasureUnit,
                    FaciStartdate = facilities.FaciStartdate,
                    FaciEndDate = facilities.FaciEndDate,
                    FaciLowPrice = facilities.FaciLowPrice,
                    FaciHighPrice = facilities.FaciHighPrice,
                    FaciDiscount = facilities.FaciDiscount,
                    FaciRatePrice = facilities.FaciRatePrice,
                    FaciTaxRate = facilities.FaciTaxRate,
                    FaciExposePrice =facilities.FaciExposePrice,
                    FaciModifiedDate = facilities.FaciModifiedDate,
                    FaciDescription = facilities.FaciDescription
                };


            var facilityPriceHistory = await _repositoryManager.FacilityPriceHistoryRepository.FindAllFacilityPriceHistoryByFacilityAsync(hotelId, faciId);

            if (facilityPriceHistory.Count() == 0)
            {
                _logger.LogError("Facilities object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
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

                
                return Ok(facilityPriceHistoryDto);
            }
        }


        [HttpGet("{hotelId}/facility/{faciId}/history/pagelist/")]
        public async Task<IActionResult> GetFacilitiesPageList([FromQuery] HistoryParameters historyParameters, int hotelId, int faciId)
        {
            var history = await _repositoryManager.FacilityPriceHistoryRepository.GetFacilityPriceHistoryPageList(historyParameters,hotelId,faciId);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(history.MetaData));

            return Ok(history);
        }


        // GET api/<FacilityPriceHistoryController>/5
        [HttpGet("{hotelId}/facility/{faciId}/price/{faphId}/facility")]
        public async Task<IActionResult> GetFacilityPriceHistoryByFacilityPriceHistoryIdAsync(int hotelId, int faciId, int faphId)
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
