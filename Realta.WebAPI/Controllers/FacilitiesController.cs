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
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
            };

            if (facilities.Count() == 0)
            {
                return Ok(new
                {
                    hotel = hotelDto,
                    facilities = "there aren\'t any facility for this hotel yet"
                });
            }
            else
            {
                var hotelReviewsDto = facilities.Select(f => new FacilitiesDto
                {
                    FaciId = f.FaciId,
                    FaciName = f.FaciName,
                    FaciRoomNumber = f.FaciRoomNumber,
                    FaciMaxNumber = f.FaciMaxNumber,
                    FaciMeasureUnit = f.FaciMeasureUnit,
                    FaciStartdate = f.FaciStartdate,
                    FaciEndate = f.FaciEndate,
                    FaciLowPrice = f.FaciLowPrice,
                    FaciHighPrice = f.FaciHighPrice,
                    FaciDiscount = f.FaciDiscount,
                    FaciRatePrice = f.FaciRatePrice,
                    FaciTaxRate = f.FaciTaxRate
                });
                return Ok(new
                {
                    hotel = hotelDto,
                    facilities = hotelReviewsDto
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
                    FaciEndate = facilities.FaciEndate,
                    FaciLowPrice = facilities.FaciLowPrice,
                    FaciHighPrice = facilities.FaciHighPrice,
                    FaciDiscount = facilities.FaciDiscount,
                    FaciRatePrice = facilities.FaciRatePrice,
                    FaciTaxRate = facilities.FaciTaxRate
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

            if (String.IsNullOrEmpty(dto.FaciMeasureUnit) || (dto.FaciMeasureUnit != "people" && dto.FaciMeasureUnit != "beds"))
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
                FaciName= dto.FaciName,
                FaciDescription= string.IsNullOrEmpty(dto.FaciDescription) ? string.Empty : dto.FaciDescription,
                FaciMaxNumber = dto.FaciMaxNumber == null ? 0 : dto.FaciMaxNumber,
                FaciMeasureUnit = string.IsNullOrEmpty(dto.FaciMeasureUnit) ? string.Empty : dto.FaciMeasureUnit,
                FaciRoomNumber = dto.FaciRoomNumber,
                FaciStartdate = dto.FaciStartdate,
                FaciEndate = dto.FaciEndate,
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
                FaciEndate = result.FaciEndate,
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
        public async Task<IActionResult> Put(int hotelId,int faciId, [FromBody] FacilitiesDto dto)
        {
            var hotels = _repositoryManager.HotelsRepository.FindHotelsById(hotelId);
            if (hotels == null)
            {
                _logger.LogError("Hotel object sent from client is null");
                return BadRequest("Record doesn't exist or wrong parameter");
            }

            if (String.IsNullOrEmpty(dto.FaciMeasureUnit) || (dto.FaciMeasureUnit != "people" && dto.FaciMeasureUnit != "beds"))
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
                FaciEndate = dto.FaciEndate,
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
                FaciEndate = result.FaciEndate,
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
                _logger.LogError($"Facility with id {faciId} Record doesn't exist or wrong parameter");
                return NotFound("Facility Not Found");
            }

            _repositoryManager.FacilitiesRepository.Remove(facilities);
            return Ok("Data Has Been Remove.");
        }
    }
}
