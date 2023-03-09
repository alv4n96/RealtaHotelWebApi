using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;
using Realta.Domain.Base;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Hotels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityPhotosController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;
        private readonly IServiceManager _serviceManager;

        public FacilityPhotosController(ILoggerManager logger, IRepositoryManager repositoryManager, IServiceManager serviceManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
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
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
            };

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
                    FaphoId = f.FaphoId,
                    FaphoThumbnailFilename = f.FaphoThumbnailFilename,
                    FaphoPhotoFilename = f.FaphoPhotoFilename,
                    FaphoOriginalFilename = f.FaphoOriginalFilename,
                    FaphoFileType = f.FaphoFileType,
                    FaphoFileSize = f.FaphoFileSize,
                    FaphoPrimary = f.FaphoPrimary,
                    FaphoUrl = f.FaphoUrl,
                    FaphoModifiedDate = f.FaphoModifiedDate,
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
        [HttpGet("{hotelId}/facilities/{faciId}/photos/{faphoId}", Name = "GetHotelFacilityPhotosById")]
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
                HotelId = hotels.HotelId,
                HotelName = hotels.HotelName,
                HotelRatingStar = hotels.HotelRatingStar,
                HotelPhonenumber = hotels.HotelPhonenumber,
            };

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

            var facilityPhotos = await _repositoryManager.FacilityPhotosRepository.FindFacilityPhotosByIdAsync(faciId, faphoId);

            if (facilityPhotos != null)
            {
                var facilityPhotosDto = new FacilityPhotosDto
                {
                    FaphoId = facilityPhotos.FaphoId,
                    FaphoThumbnailFilename = facilityPhotos.FaphoThumbnailFilename,
                    FaphoOriginalFilename = facilityPhotos.FaphoOriginalFilename,
                    FaphoFileType = facilityPhotos.FaphoFileType,
                    FaphoFileSize = facilityPhotos.FaphoFileSize,
                    FaphoPhotoFilename = facilityPhotos.FaphoPhotoFilename,
                    FaphoPrimary = facilityPhotos.FaphoPrimary,
                    FaphoUrl = facilityPhotos.FaphoUrl,
                    FaphoModifiedDate = facilityPhotos.FaphoModifiedDate
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
        [HttpPost("{hotelId}/facilities/{faciId}/photos"), DisableRequestSizeLimit]
        public async Task<IActionResult> PostAsync(int hotelId, int faciId, [FromBody] FacilityPhotosDto dto)
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
                _logger.LogError("facility object sent from client is null");
                return BadRequest("Facility doesn't exist or wrong parameter");
            }

            if (dto == null)
            {
                _logger.LogError("Hotel region object sent from client is null");
                return BadRequest("Some parameters are missing");
            }
            //1. declare formCollection to hold form-data
            var formCollection = await Request.ReadFormAsync();

            //2. extract files to variable files
            var files = formCollection.Files;

            //3. hold each ouput formCollection to each variable
            formCollection.TryGetValue("FaphoThumbnailFilename", out var FaphoThumbnailFilename);
            formCollection.TryGetValue("FaphoPhotoFilename", out var FaphoPhotoFilename);
            formCollection.TryGetValue("FaphoPrimary", out var FaphoPrimary);
            formCollection.TryGetValue("FaphoUrl", out var FaphoUrl);
            formCollection.TryGetValue("FaphoFaciId", out var FaphoFaciId);

            //4. declare variable and store in object 
            var facilityPhotoDto = new FacilityPhotosDto
            {
                FaphoThumbnailFilename = FaphoThumbnailFilename.ToString(),
                FaphoPhotoFilename = FaphoPhotoFilename.ToString(),
                FaphoPrimary = bool.Parse(FaphoPrimary.ToString()),
                FaphoUrl = FaphoUrl.ToString(),
                FaphoFaciId = int.Parse(FaphoFaciId.ToString())

            };

            //5. store to list
            var allPhotos = new List<IFormFile>();
            foreach (var file in formCollection.Files) allPhotos.Add(file);

            //6. declare variable productphotogroup
            var facilityPhotoGroup = new FacilityPhotoGroupDto
            {
                FacilityPhotos = facilityPhotoDto,
                AllPhotos = allPhotos
            };

            if (allPhotos != null)
            {
                _serviceManager.FacilityPhotoServices.InsertFacilityAndFacilityPhoto(facilityPhotoGroup, out var faphoId);
                var productResult = _repositoryManager.FacilityPhotosRepository.FindFacilityPhotosByIdAsync(faciId, faphoId);
                return Ok(productResult);
            }

            return BadRequest();

            //End of file
            var facilityPhotos = new FacilityPhotos()
            {
                FaphoThumbnailFilename = dto.FaphoThumbnailFilename,
                FaphoPhotoFilename = dto.FaphoPhotoFilename,
                FaphoPrimary = dto.FaphoPrimary,
                FaphoUrl = dto.FaphoUrl,
                FaphoFaciId = faciId
            };

            _repositoryManager.FacilityPhotosRepository.Insert(facilityPhotos);

            var result = await _repositoryManager.FacilityPhotosRepository.FindFacilityPhotosByIdAsync(faciId, facilityPhotos.FaphoId);

            var resDto = new FacilityPhotosDto
            {
                FaphoId = result.FaphoId,
                FaphoThumbnailFilename = result.FaphoThumbnailFilename,
                FaphoPhotoFilename = result.FaphoPhotoFilename,
                FaphoPrimary = result.FaphoPrimary,
                FaphoUrl = result.FaphoUrl,
                FaphoModifiedDate = result.FaphoModifiedDate
            };

            return Ok(resDto);
        }

        // PUT api/<FacilityPhotosController>/5
        [HttpPut("{hotelId}/facilities/{faciId}/photos/{faphoId}")]
        public async Task<IActionResult> PutAsync(int hotelId, int faciId, int faphoId, [FromBody] FacilityPhotosDto dto)
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
                _logger.LogError("facility object sent from client is null");
                return BadRequest("Facility doesn't exist or wrong parameter");
            }

            if (dto == null)
            {
                _logger.LogError("Hotel region object sent from client is null");
                return BadRequest("Some parameters are missing");
            }

            var facilityPhotos = new FacilityPhotos()
            {
                FaphoId = faphoId,
                FaphoThumbnailFilename = dto.FaphoThumbnailFilename,
                FaphoPhotoFilename = dto.FaphoPhotoFilename,
                FaphoPrimary = dto.FaphoPrimary,
                FaphoUrl = dto.FaphoUrl,
                FaphoFaciId = faciId
            };

            _repositoryManager.FacilityPhotosRepository.Edit(facilityPhotos);

            var result = await _repositoryManager.FacilityPhotosRepository.FindFacilityPhotosByIdAsync(faciId, faphoId);

            var resDto = new FacilityPhotosDto
            {
                FaphoId = result.FaphoId,
                FaphoThumbnailFilename = result.FaphoThumbnailFilename,
                FaphoPhotoFilename = result.FaphoPhotoFilename,
                FaphoPrimary = result.FaphoPrimary,
                FaphoUrl = result.FaphoUrl,
                FaphoModifiedDate = result.FaphoModifiedDate
            };

            return Ok(resDto);
        }

        // DELETE api/<FacilityPhotosController>/5
        [HttpDelete("{hotelId}/facilities/{faciId}/photos/{faphoId}")]
        public async Task<IActionResult> DeleteAsync(int hotelId, int faciId, int faphoId)
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
                _logger.LogError("facility object sent from client is null");
                return BadRequest("Facility doesn't exist or wrong parameter");
            }

            var facilityPhotos = await _repositoryManager.FacilityPhotosRepository.FindFacilityPhotosByIdAsync(faciId, faphoId);
            if (facilityPhotos == null)
            {
                _logger.LogError($"Facility Photos with id {faphoId} Record doesn't exist or wrong parameter");
                return NotFound("Facility Photos id Not Found!");
            }

            _repositoryManager.FacilityPhotosRepository.Remove(facilityPhotos);
            return Ok("Data Has Been Remove.");
        }
    }
}
