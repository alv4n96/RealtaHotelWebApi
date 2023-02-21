using Microsoft.VisualBasic;
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
    internal class FacilitiesRepository : RepositoryBase<Facilities>, IFacilitiesRepository
    {
        public FacilitiesRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }


        public async Task<IEnumerable<Facilities>> FindAllFacilitiesAsync(int hotelId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },}
            };

            IAsyncEnumerator<Facilities> dataSet = FindAllAsync<Facilities>(model);
            var item = new List<Facilities>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<Facilities> FindFacilitiesByIdAsync(int hotelId, int facilitiesId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id AND faci_id = @faci_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_id",
                        DataType = DbType.Int32,
                        Value = facilitiesId
                    },
                }
            };

            IAsyncEnumerator<Facilities> dataSet = FindAllAsync<Facilities>(model);
            Facilities? item = dataSet.Current;

            while (await dataSet.MoveNextAsync())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public void Insert(Facilities hotelReviews)
        {
            throw new NotImplementedException();
        }
        public void Edit(Facilities hotelReviews)
        {
            throw new NotImplementedException();
        }

        public void Remove(Facilities hotelReviews)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Facilities> FindAllFacilities()
        {
            throw new NotImplementedException();
        }
    }
}
