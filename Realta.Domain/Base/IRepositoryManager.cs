using Realta.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Base
{
    public interface IRepositoryManager
    {
        IHotelsRepository HotelsRepository { get; }
        IHotelReviewsRepository HotelReviewsRepository { get; }
    }
}
