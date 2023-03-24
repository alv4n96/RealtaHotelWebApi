using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories.v1
{
    internal class CategoryGroupRepository : RepositoryBase<CategoryGroup>, ICategoryGroupRepository
    {
        public CategoryGroupRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }



        public async Task<IEnumerable<CategoryGroup>> FindAllCategoryGroupAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"
SELECT 
    cagro_id AS CagroId 
    ,cagro_name AS CagroName 
    ,cagro_description AS CagroDescription 
    ,cagro_type AS CagroType 
    ,cagro_icon AS CagroIcon 
    ,cagro_icon_url AS CagroIconUrl  
FROM master.category_group;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                }
            };

            IAsyncEnumerator<CategoryGroup> dataset = FindAllAsync<CategoryGroup>(model);

            var item = new List<CategoryGroup>();

            while (await dataset.MoveNextAsync())
            {
                item.Add(dataset.Current);
            }

            return item;
        }


        public CategoryGroup FindCategoryGroupById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM master.category_group where cagro_id=@cagro_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@cagro_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<CategoryGroup>(model);

            CategoryGroup? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }
    }
}
