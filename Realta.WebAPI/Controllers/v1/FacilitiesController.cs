using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Facilities;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers.v1
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
            var hotels = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }
            var facilities = await _repositoryManager.FacilitiesRepository.FindAllFacilitiesAsync(hotelId);

            var hotelDto = new HotelsDto
            {
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelDescription = hotels.HotelDescription,
                HotelAddrDescription = hotels.HotelAddrDescription,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
                // HotelStatus = hotels.HotelStatus ? "available" : "unavailable",
                HotelStatus = hotels.HotelStatus,
                HotelModifiedDate = hotels.HotelModifiedDate,
            };

            if (facilities.Count() == 0)
            {
                _logger.LogError("Facilities object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }
            else
            {
                var hotelFacilitiesDto = facilities.Select(f => new FacilitiesDto
                {
                    FaciId = f.FaciId,
                    FaciName = f.FaciName,
                    FaciExposePrice = f.FaciExposePrice,
                    FaciRoomNumber = f.FaciRoomNumber,
                    FaciMaxNumber = f.FaciMaxNumber,
                    FaciMeasureUnit = f.FaciMeasureUnit,
                    FaciStartdate = f.FaciStartdate,
                    FaciEndDate = f.FaciEndDate,
                    FaciLowPrice = f.FaciLowPrice,
                    FaciHighPrice = f.FaciHighPrice,
                    FaciDiscount = f.FaciDiscount,
                    FaciRatePrice = f.FaciRatePrice,
                    FaciTaxRate = f.FaciTaxRate,
                    FaciModifiedDate = f.FaciModifiedDate
                });

                //var result = new HotelFaciAllDto()
                //{
                //    Hotels = hotelDto,
                //    Facilities = hotelFacilitiesDto
                //};
                return Ok(hotelFacilitiesDto);
            }
        }

        // GET api/<FacilitiesController>/5
        [HttpGet("{hotelId}/facilities/{faciId}", Name = "GetHotelFaciById")]
        public async Task<IActionResult> GetFacilitiesByIdAsync(int hotelId, int faciId)
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

            if (facilities != null)
            {
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
                    FaciExposePrice = facilities.FaciExposePrice,
                    FaciModifiedDate = facilities.FaciModifiedDate,
                    FaciDescription = facilities.FaciDescription
                };

                var result = new HotelFaciByIdDto()
                {
                    Hotels = hotelDto,
                    Facilities = facilitiesDto
                };

                return Ok(result);
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
        [HttpGet("{hotelId}/facilities/pageList/")]

        public async Task<IActionResult> GetFacilitiesPageList([FromQuery] FacilitiesParameters facilitiesParameter, int hotelId)
        {

            var facilities = await _repositoryManager.FacilitiesRepository.GetFacilitiesPageList(facilitiesParameter, hotelId);


            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(facilities.MetaData));

            return Ok(facilities);
        }

        // POST api/<FacilitiesController>
        [HttpPost("{hotelId}/facilities/")]
        public async Task<IActionResult> Post(int hotelId, [FromBody] FacilitiesDto dto)
        {
            var hotels = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            if (string.IsNullOrEmpty(dto.FaciMeasureUnit) || dto.FaciMeasureUnit != "people" && dto.FaciMeasureUnit != "beds")
            {
                _logger.LogError("facility measure unit got wrong parameter");
                return BadRequest("facility measure unit should fill : people or beds");
            }

            if (dto == null)
            {
                _logger.LogError("Hotel facility photos object sent from client is null");
                return BadRequest("Some parameters are missing");
            }

            var facilities = new Facilities()
            {
                FaciName = dto.FaciName,
                FaciDescription = string.IsNullOrEmpty(dto.FaciDescription) ? string.Empty : dto.FaciDescription,
                FaciMaxNumber = dto.FaciMaxNumber == null ? 0 : dto.FaciMaxNumber,
                FaciMeasureUnit = string.IsNullOrEmpty(dto.FaciMeasureUnit) ? string.Empty : dto.FaciMeasureUnit,
                FaciRoomNumber = dto.FaciRoomNumber,
                FaciStartdate = dto.FaciStartdate,
                FaciEndDate = dto.FaciEndDate,
                FaciLowPrice = dto.FaciLowPrice,
                FaciHighPrice = dto.FaciHighPrice,
                FaciDiscount = (decimal)(dto.FaciDiscount == null ? 0 : dto.FaciDiscount),
                FaciTaxRate = (decimal)(dto.FaciTaxRate == null ? 0 : dto.FaciTaxRate),
                FaciCagroId = dto.FaciCagroId,
                FaciHotelId = hotelId,
                FaciUserId = dto.FaciUserId,
            };

            //post data to db
            _repositoryManager.FacilitiesRepository.Insert(facilities);

            var result = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, facilities.FaciId);


            var resDto = new FacilitiesDto()
            {
                FaciId = result.FaciId,
                FaciName = result.FaciName,
                FaciRoomNumber = result.FaciRoomNumber,
                FaciMaxNumber = result.FaciMaxNumber,
                FaciMeasureUnit = result.FaciMeasureUnit,
                FaciStartdate = result.FaciStartdate,
                FaciEndDate = result.FaciEndDate,
                FaciLowPrice = result.FaciLowPrice,
                FaciHighPrice = result.FaciHighPrice,
                FaciDiscount = result.FaciDiscount,
                FaciRatePrice = result.FaciRatePrice,
                FaciTaxRate = result.FaciTaxRate,
                FaciCagroId = result.FaciCagroId,
                FaciUserId = result.FaciUserId,
            };

            if (resDto == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record Data Error!");
            }

            return Ok(resDto);
        }

        // PUT api/<FacilitiesController>/5
        [HttpPut("{hotelId}/facilities/{faciId}")]
        public async Task<IActionResult> Put(int hotelId, int faciId, [FromBody] FacilitiesDto dto)
        {
            var hotels = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            if (string.IsNullOrEmpty(dto.FaciMeasureUnit) || dto.FaciMeasureUnit != "people" && dto.FaciMeasureUnit != "beds")
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
                FaciId = dto.FaciId,
                FaciName = dto.FaciName,
                FaciDescription = string.IsNullOrEmpty(dto.FaciDescription) ? string.Empty : dto.FaciDescription,
                FaciMaxNumber = dto.FaciMaxNumber == null ? 0 : dto.FaciMaxNumber,
                FaciMeasureUnit = string.IsNullOrEmpty(dto.FaciMeasureUnit) ? string.Empty : dto.FaciMeasureUnit,
                FaciRoomNumber = dto.FaciRoomNumber,
                FaciStartdate = dto.FaciStartdate,
                FaciEndDate = dto.FaciEndDate,
                FaciLowPrice = dto.FaciLowPrice,
                FaciHighPrice = dto.FaciHighPrice,
                FaciDiscount = (decimal)(dto.FaciDiscount == null ? 0 : dto.FaciDiscount),
                FaciTaxRate = (decimal)(dto.FaciTaxRate == null ? 0 : dto.FaciTaxRate),
                FaciCagroId = dto.FaciCagroId,
                FaciHotelId = hotelId,
                FaciUserId = dto.FaciUserId,
            };

            _repositoryManager.FacilitiesRepository.Edit(facilities);

            var result = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, facilities.FaciId);


            var resDto = new FacilitiesDto()
            {
                FaciId = result.FaciId,
                FaciName = result.FaciName,
                FaciRoomNumber = result.FaciRoomNumber,
                FaciMaxNumber = result.FaciMaxNumber,
                FaciMeasureUnit = result.FaciMeasureUnit,
                FaciStartdate = result.FaciStartdate,
                FaciEndDate = result.FaciEndDate,
                FaciLowPrice = result.FaciLowPrice,
                FaciHighPrice = result.FaciHighPrice,
                FaciDiscount = result.FaciDiscount,
                FaciRatePrice = result.FaciRatePrice,
                FaciTaxRate = result.FaciTaxRate,
                FaciCagroId = result.FaciCagroId,
                FaciUserId = result.FaciUserId,
            };

            //forward 
            return Ok(resDto);
        }

        // DELETE api/<FacilitiesController>/5
        [HttpDelete("{hotelId}/facilities/{faciId}")]
        public async Task<IActionResult> Delete(int hotelId, int faciId)
        {
            var hotels = await _repositoryManager.HotelsRepository.FindHotelsByIdAsync(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            //2. Find id first
            var facilities = await _repositoryManager.FacilitiesRepository.FindFacilitiesByIdAsync(hotelId, faciId);
            if (facilities == null)
            {
                _logger.LogError($"Facility with id {faciId} Record doesn't exist or wrong parameter");
                return NotFound("Facility Not Found");
            }

            _repositoryManager.FacilitiesRepository.Remove(facilities);
            return Ok("Data Has Been Remove.");
        }
    }
}
