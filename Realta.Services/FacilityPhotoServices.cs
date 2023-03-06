using Realta.Contract.Models.v1;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services
{
    internal class FacilityPhotoServices : IFacilityPhotoServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUtilityService _utilityService;

        public FacilityPhotoServices(IRepositoryManager repositoryManager, IUtilityService utilityService)
        {
            _repositoryManager = repositoryManager;
            _utilityService = utilityService;
        }

        public void InsertFacilityAndFacilityPhoto(FacilityPhotoGroupDto facilityPhotoGroupDto, out int faphoId)
        {
            //1. insert into table Faciity photos
            var facilitiesPhoto = new FacilityPhotos()
            {
                FaphoThumbnailFilename = facilityPhotoGroupDto.FacilityPhotos.FaphoThumbnailFilename,
                FaphoPhotoFilename = facilityPhotoGroupDto.FacilityPhotos.FaphoPhotoFilename,
                FaphoPrimary = facilityPhotoGroupDto.FacilityPhotos.FaphoPrimary,
                FaphoUrl = facilityPhotoGroupDto.FacilityPhotos.FaphoUrl,
            };

            //2. insert product to table
            _repositoryManager.FacilityPhotosRepository.Insert(facilitiesPhoto);

            //3. if insert product success then get productId
            faphoId = facilitiesPhoto.FaphoId;


            //4. extract photos
            var allPhotos = facilityPhotoGroupDto.AllPhotos;

            foreach (var itemPhoto in allPhotos)
            {
                var fileName = _utilityService.UploadSingleFile(itemPhoto);
                var facilityPhotos = new FacilityPhotos()
                {
                    FaphoPhotoFilename = fileName,
                    FaphoFileSize = (short)itemPhoto.Length,
                    FaphoFileType = itemPhoto.ContentType,
                    FaphoOriginalFilename = itemPhoto.FileName,
                    FaphoPrimary = facilitiesPhoto.FaphoPrimary,
                    FaphoId = faphoId,
                };

                _repositoryManager.FacilityPhotosRepository.Insert(facilityPhotos);
            }
        }
    }
}
