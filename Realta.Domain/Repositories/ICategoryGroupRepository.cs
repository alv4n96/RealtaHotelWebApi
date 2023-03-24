using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface ICategoryGroupRepository
    {

        Task<IEnumerable<CategoryGroup>> FindAllCategoryGroupAsync();
        CategoryGroup FindCategoryGroupById(int id);
    }
}
