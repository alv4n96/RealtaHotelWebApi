using Realta.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services.Abstraction
{
    public interface IFacilityPhotoServices
    {
        public void InsertFacilityAndFacilityPhoto(FacilityPhotoGroupDto facilityPhotoGroupDto, out int faphoId);
    }
}
