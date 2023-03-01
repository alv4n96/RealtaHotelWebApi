using Realta.Domain.Base;
using Realta.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFacilityPhotoServices> _lazyFacilityPhotoServices;
        private readonly Lazy<IUtilityService> _lazyUtilityService;

        public ServiceManager(IRepositoryManager repositoryManager, IUtilityService _lazyUtilityService)
        {
            _lazyFacilityPhotoServices = new Lazy<IFacilityPhotoServices>(()=> new FacilityPhotoServices(repositoryManager, _lazyUtilityService));
        }

        public IFacilityPhotoServices FacilityPhotoServices => throw new NotImplementedException();
    }
}
