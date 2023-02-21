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
        IEnumerable<Facility_Photos> FindAllFacilityPhotos();
        Task<IEnumerable<Facility_Photos>> FindAllFacilityPhotosAsync(int faciId);
        Task<Facility_Photos> FindFacilityPhotosByIdAsync(int faciId, int faPhoId);
        void Insert(Facility_Photos facilityPhotos);
        void Edit(Facility_Photos facilityPhotos);
        void Remove(Facility_Photos facilityPhotos);
    }
}
