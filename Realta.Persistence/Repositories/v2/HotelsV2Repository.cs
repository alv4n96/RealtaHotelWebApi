using Realta.Domain.Entities;
using Realta.Domain.Repositories.v2;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories.v2
{
    internal class HotelsV2Repository : RepositoryBase<Hotels>, IHotelsV2Repository
    {
        public HotelsV2Repository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public IEnumerable<Hotels> FindAllHotels()
        {
            IEnumerator<Hotels> dataSet = FindAll<Hotels>("[Hotel].[spSelectHotel]");

            while (dataSet.MoveNext())
            {
                var item = dataSet.Current;
                yield return item;
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
                CommandText = "SELECT " +
                "hotel_id AS HotelId " +
                ",hotel_name AS HotelName " +
                ",hotel_description AS HotelDescription " +
                ",hotel_status AS HotelStatus " +
                ",hotel_reason_status AS HotelReasonStatus " +
                ",hotel_rating_star AS HotelRatingStar " +
                ",hotel_phonenumber AS HotelPhonenumber " +
                ",hotel_modified_date AS HotelModifiedDate " +
                ",hotel_addr_id AS HotelAddrId  " +
                "FROM Hotel.Hotels " +
                "WHERE hotel_id = @hotelId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotelId",
                        DataType = DbType.Int32,
                        Value = hotelId
                    }
                }
            };

            var dataSet = FindByCondition<Hotels>(model);

            var item = new Hotels();

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public int GetSequenceId(string sql)
        {
            throw new NotImplementedException();
        }

        public void Insert(Hotels supplier)
        {
            throw new NotImplementedException();
        }

        public void Edit(Hotels supplier)
        {
            throw new NotImplementedException();
        }

        public void Remove(Hotels supplier)
        {
            throw new NotImplementedException();
        }
    }
}
