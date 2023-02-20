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
    internal class HotelReviewsRepository : RepositoryBase<Hotel_Reviews>, IHotelReviewsRepository
    {
        public HotelReviewsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Hotel_Reviews hotelReviews)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE Hotel.Hotel_Reviews " +
                "SET hore_user_review = @hore_user_review, " +
                "hore_rating = @hore_rating, " +
                "hore_created_on = GETDATE(), " +
                "hore_user_id = @hore_user_id, " +
                "hore_hotel_id = @hore_hotel_id " +
                "WHERE hore_id = @hore_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_user_review",
                        DataType = DbType.String,
                        Value = hotelReviews.hore_user_review
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_rating",
                        DataType = DbType.Byte,
                        Value = hotelReviews.hore_rating
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_user_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.hore_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.hore_hotel_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.hore_id
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
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

        public async Task<Hotel_Reviews> FindHotelReviewsByIdAsync(int hotelId, int hotelReviewsId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Hotel.Hotel_Reviews WHERE hore_hotel_id = @hore_hotel_id AND hore_id = @hore_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_id",
                        DataType = DbType.Int32,
                        Value = hotelReviewsId
                    },
                }
            };

            IAsyncEnumerator<Hotel_Reviews> dataSet = FindAllAsync<Hotel_Reviews>(model);
            Hotel_Reviews? item = dataSet.Current;

            while (await dataSet.MoveNextAsync())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public void Insert(Hotel_Reviews hotelReviews)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText =   "INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id) " +
                                "VALUES(@hore_user_review, @hore_rating, GETDATE(), @hore_user_id, @hore_hotel_id); " +
                                "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_user_review",
                        DataType = DbType.String,
                        Value = hotelReviews.hore_user_review
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_rating",
                        DataType = DbType.Byte,
                        Value = hotelReviews.hore_rating
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_user_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.hore_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.hore_hotel_id
                    },
                }
            };

            hotelReviews.hore_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(Hotel_Reviews hotelReviews)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Hotel.Hotel_Reviews " +
                "WHERE hore_id = @hore_id; ",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.hore_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }


        public IEnumerable<Hotel_Reviews> FindAllHotelReviews()
        {
            throw new NotImplementedException();
        }


    }
}
