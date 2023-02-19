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
    internal class HotelReviewsRepository : RepositoryBase<Hotel_Reviews>, IHotelReviewsRepository
    {
        public HotelReviewsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Hotel_Reviews hotelReviews)
        {
            throw new NotImplementedException();
        }

        public void EditStatus(Hotel_Reviews hotelReviews)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hotel_Reviews> FindAllHotelReviews()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Hotel_Reviews>> FindAllHotelReviewsAsync(int hotelId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Hotel.Hotel_Reviews WHERE hore_hotel_id = @hore_hotel_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },}
            };

            IAsyncEnumerator<Hotel_Reviews> dataSet = FindAllAsync<Hotel_Reviews>(model);
            var item = new List<Hotel_Reviews>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public Hotel_Reviews FindHotelReviewsById(int hotelId, int hotelReviewsId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Hotel_Reviews hotelReviews)
        {
            throw new NotImplementedException();
        }

        public void Remove(Hotel_Reviews hotelReviews)
        {
            throw new NotImplementedException();
        }
    }
}
