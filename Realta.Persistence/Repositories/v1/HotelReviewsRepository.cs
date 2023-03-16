using Microsoft.VisualBasic;
using Realta.Domain.Entities;
using Realta.Domain.Repositories.v1;
using Realta.Domain.RequestFeatures;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories.v1
{
    internal class HotelReviewsRepository : RepositoryBase<HotelReviews>, IHotelReviewsRepository
    {
        public HotelReviewsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(HotelReviews hotelReviews)
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
                        Value = hotelReviews.HoreUserReview
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_rating",
                        DataType = DbType.Byte,
                        Value = hotelReviews.HoreRating
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_user_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.HoreUserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.HoreHotelId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.HoreId
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public async Task<IEnumerable<HotelReviews>> FindAllHotelReviewsAsync(int hotelId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT " +
                "hore_id AS HoreId " +
                ",hore_user_review AS HoreUserReview " +
                ",hore_rating AS HoreRating " +
                ",hore_created_on AS HoreCreatedOn " +
                ",hore_user_id AS HoreUserId " +
                ",hore_hotel_id AS HoreHotelId " +
                "FROM Hotel.Hotel_Reviews " +
                "WHERE hore_hotel_id = @hore_hotel_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },}
            };

            IAsyncEnumerator<HotelReviews> dataSet = FindAllAsync<HotelReviews>(model);
            var item = new List<HotelReviews>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<HotelReviews> FindHotelReviewsByIdAsync(int hotelId, int hotelReviewsId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT " +
                "hore_id AS HoreId " +
                ",hore_user_review AS HoreUserReview " +
                ",hore_rating AS HoreRating " +
                ",hore_created_on AS HoreCreatedOn " +
                ",hore_user_id AS HoreUserId " +
                ",hore_hotel_id AS HoreHotelId " +
                "FROM Hotel.Hotel_Reviews " +
                "WHERE hore_hotel_id = @hore_hotel_id AND hore_id = @hore_id;",
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

            IAsyncEnumerator<HotelReviews> dataSet = FindAllAsync<HotelReviews>(model);
            HotelReviews? item = dataSet.Current;

            while (await dataSet.MoveNextAsync())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public void Insert(HotelReviews hotelReviews)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id) " +
                                "VALUES(@hore_user_review, @hore_rating, GETDATE(), @hore_user_id, @hore_hotel_id); " +
                                "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_user_review",
                        DataType = DbType.String,
                        Value = hotelReviews.HoreUserReview
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_rating",
                        DataType = DbType.Byte,
                        Value = hotelReviews.HoreRating
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_user_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.HoreUserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hore_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelReviews.HoreHotelId
                    },
                }
            };

            hotelReviews.HoreId = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(HotelReviews hotelReviews)
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
                        Value = hotelReviews.HoreId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }


        public IEnumerable<HotelReviews> FindAllHotelReviews()
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<HotelReviews>> GetReviewsPageList(ReviewsParameters reviewsParam, int hotelId)
        {
            throw new NotImplementedException();
        }
    }
}
