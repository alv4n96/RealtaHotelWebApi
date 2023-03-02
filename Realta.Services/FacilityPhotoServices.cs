using Realta.Contract.Models;
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
                fapho_thumbnail_filename = facilityPhotoGroupDto.FacilityPhotos.fapho_thumbnail_filename,
                fapho_photo_filename = facilityPhotoGroupDto.FacilityPhotos.fapho_photo_filename,
                fapho_primary = facilityPhotoGroupDto.FacilityPhotos.fapho_primary,
                fapho_url = facilityPhotoGroupDto.FacilityPhotos.fapho_url,
            };

            //2. insert product to table
            _repositoryManager.FacilityPhotosRepository.Insert(facilitiesPhoto);

            //3. if insert product success then get productId
            faphoId = facilitiesPhoto.fapho_id;


            //4. extract photos
            var allPhotos = facilityPhotoGroupDto.AllPhotos;

            foreach (var itemPhoto in allPhotos)
            {
                var fileName = _utilityService.UploadSingleFile(itemPhoto);
                var facilityPhotos = new FacilityPhotos()
                {
                    fapho_photo_filename = fileName,
                    fapho_file_size = (short)itemPhoto.Length,
                    fapho_file_type = itemPhoto.ContentType,
                    fapho_original_filename = itemPhoto.FileName,
                    fapho_primary = facilitiesPhoto.fapho_primary,
                    fapho_id = faphoId,
                };

                _repositoryManager.FacilityPhotosRepository.Insert(facilityPhotos);
            }
        }
    }
}
