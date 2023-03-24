using Realta.Domain.RequestFeatures;
using Realta.Domain.Entities;
using Realta.Domain.Repositories.v1;
using Realta.Persistence.Base;
using Realta.Persistence.Repositories.RepositoriesExtensions;
using Realta.Persistence.RepositoryContext;
using System.Data;
using Realta.Domain.RequestFeatures.HotelParameters;

namespace Realta.Persistence.Repositories.v1
{
    internal class HotelsRepository : RepositoryBase<Hotels>, IHotelsRepository
    {
        public HotelsRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public IEnumerable<Hotels> FindAllHotels()
        {
            IEnumerator<Hotels> dataSet = FindAll<Hotels>("	SELECT " +
                "hotel_id AS HotelId " +
                ",hotel_name AS HotelName " +
                ",hotel_description AS HotelDescription " +
                ",hotel_status AS HotelStatus " +
                ",hotel_reason_status AS HotelReasonStatus " +
                ",hotel_rating_star AS HotelRatingStar " +
                ",hotel_phonenumber AS HotelPhonenumber " +
                ",hotel_modified_date AS HotelModifiedDate " +
                ",hotel_addr_id AS HotelAddrId " +
                ",hotel_addr_description AS HotelAddrDescription " +
                "FROM Hotel.Hotels;");

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

        public IEnumerable<Hotels> FindHotelsByName(string name)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "[Hotel].[spSelectHotelByName]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotelName",
                        DataType = DbType.String,
                        Value = name,
                    }
                }
            };

            var dataSet = FindByCondition<Hotels>(model);

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<Hotels> FindHotelsByIdAsync(int hotelId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "[Hotel].[spSelectHotelById]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotelId",
                        DataType = DbType.Int32,
                        Value = hotelId,
                    }
                }
            };


            IAsyncEnumerator<Hotels> dataSet = FindAllAsync<Hotels>(model);
            Hotels? item = dataSet.Current;

            while (await dataSet.MoveNextAsync())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public void Insert(Hotels hotels)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "[Hotel].[spInsertHotel]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_name",
                        DataType = DbType.String,
                        Value = hotels.HotelName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_phonenumber",
                        DataType = DbType.String,
                        Value = hotels.HotelPhonenumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_status",
                        DataType = DbType.Boolean,
                        Value = hotels.HotelStatus
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@add_id",
                        DataType = DbType.String,
                        Value = hotels.HotelAddrDescription
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_description",
                        DataType = DbType.String,
                        Value = hotels.HotelDescription
                    },
                }
            };

            hotels.HotelId = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Edit(Hotels hotels)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "[Hotel].[spUpdateHotel]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_id",
                        DataType = DbType.Int32,
                        Value = hotels.HotelId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_name",
                        DataType = DbType.String,
                        Value = hotels.HotelName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_phonenumber",
                        DataType = DbType.String,
                        Value = hotels.HotelPhonenumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@add_id",
                        DataType = DbType.String,
                        Value = hotels.HotelAddrDescription
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_description",
                        DataType = DbType.String,
                        Value = hotels.HotelDescription
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void EditStatus(Hotels hotels)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "[Hotel].[spUpdateStatusHotel]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_id",
                        DataType = DbType.Int32,
                        Value = hotels.HotelId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_status",
                        DataType = DbType.Boolean,
                        Value = hotels.HotelStatus
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_reason_status",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(hotels.HotelReasonStatus) ? DBNull.Value : hotels.HotelReasonStatus
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Hotels hotels)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "[Hotel].[spDeleteHotel]",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotel_id",
                        DataType = DbType.Int32,
                        Value = hotels.HotelId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public async Task<IEnumerable<Hotels>> GetHotelPaging(HotelsParameters hotelParam)
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
                ",hotel_addr_id AS HotelAddrId " +
                "FROM Hotel.Hotels " +
                "ORDER BY hotel_id " +
                "OFFSET @pageNo ROWS FETCH NEXT @pageSize ROWS ONLY;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                            ParameterName = "@pageNo",
                            DataType = DbType.Int32,
                            Value = hotelParam.PageNumber
                        },
                     new SqlCommandParameterModel() {
                            ParameterName = "@pageSize",
                            DataType = DbType.Int32,
                            Value = hotelParam.PageSize
                        }
                }

            };

            IAsyncEnumerator<Hotels> dataSet = FindAllAsync<Hotels>(model);

            var item = new List<Hotels>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<PagedList<Hotels>> GetHotelPageList(HotelsParameters hotelParam)
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
                ",hotel_addr_id AS HotelAddrId " +
                "FROM Hotel.Hotels " +                
                "order by hotel_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                }
            };

            var hotels = await GetAllAsync<Hotels>(model);
            //var totalRow = FindAllHotels().Count();



            var hotelSearch = hotels.AsQueryable()
                                .SearchHotels(hotelParam.SearchTerm)
                                .Sort(hotelParam.OrderBy);

            return PagedList<Hotels>.ToPagedList(hotelSearch.ToList(),hotelParam.PageNumber,hotelParam.PageSize);

            //return new PagedList<Hotels>(products.ToList(), totalRow, productParameters.PageNumber, productParameters.PageSize);
            //return new PagedList<Hotels>(productSearch.ToList(), totalRow, hotelParam.PageNumber, hotelParam.PageSize);
        }
    }
}