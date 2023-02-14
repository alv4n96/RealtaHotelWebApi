using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class HotelsRepository : RepositoryBase<Hotels>, IHotelsRepository
    {
        public HotelsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }



        public IEnumerable<Hotels> FindAllHotels()
        {
            IEnumerator<Hotels> dataSet = FindAll<Hotels>("[Hotel].[spSelectHotel];");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;

            }
        }

        public async Task<IEnumerable<Hotels>> FindAllHotelsAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "[Hotel].[spSelectHotel];",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }
            };

            IAsyncEnumerator<Hotels> dataSet = FindAllAsync<Hotels>(model);
            var item = new List<Hotels>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public Hotels FindHotelsById(int hotelId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Hotel.Hotels WHERE hotel_id = @hotelId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotelId",
                        DataType = DbType.Int32,
                        Value = hotelId
                    }
                }
            };
            var dataSet = FindByCondition<Hotels>(model);
            Hotels? hotel = dataSet.Current;

            while (dataSet.MoveNext())
            {
                hotel = dataSet.Current;
            }

            return hotel;
        }

        public void Insert(Hotels hotels)
        {
            throw new NotImplementedException();
        }

        public void Edit(Hotels hotels)
        {
            throw new NotImplementedException();
        }

        public void Remove(Hotels hotels)
        {
            throw new NotImplementedException();
        }
    }
}
