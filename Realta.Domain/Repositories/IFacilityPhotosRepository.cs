using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IFacilityPhotosRepository
    {
        IEnumerable<FacilityPhotos> FindAllFacilityPhotos();
        Task<IEnumerable<FacilityPhotos>> FindAllFacilityPhotosAsync(int faciId);
        Task<FacilityPhotos> FindFacilityPhotosByIdAsync(int faciId, int faPhoId);
        void Insert(FacilityPhotos facilityPhotos);
        void Edit(FacilityPhotos facilityPhotos);
        void Remove(FacilityPhotos facilityPhotos);
        Task FindFacilityPhotosByIdAsync(int faphoId);
    }
}
