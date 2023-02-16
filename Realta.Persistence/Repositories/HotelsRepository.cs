using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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



        public Hotels FindHotelsByName(string name)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM [Hotel].[Hotels] WHERE hotel_name LIKE '%@hotelName%';",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@hotelName",
                        DataType = DbType.String,
                        Value = name,
                    }
                }
            };

            Console.WriteLine(model);

            var dataSet = FindByCondition<Hotels>(model);
            //var item = new List<Hotels>();

            Hotels? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
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
                        Value = hotelId,
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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_rating_star, hotel_phonenumber, " +
                "hotel_modified_date, hotel_addr_id) " +
                "VALUES (@hotel_name, @hotel_description, @hotel_rating_star, @hotel_phonenumber, GETDATE(), @hotel_addr_id);" +
                " SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_name",
                        DataType = DbType.String,
                        Value = hotels.hotel_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_description",
                        DataType = DbType.String,
                        Value = hotels.hotel_description
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_rating_star",
                        DataType = DbType.Int16,
                        Value = hotels.hotel_rating_star
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_phonenumber",
                        DataType = DbType.String,
                        Value = hotels.hotel_phonenumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_addr_id",
                        DataType = DbType.Int32,
                        Value = hotels.hotel_addr_id
                    },
                }
            };

            hotels.hotel_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Edit(Hotels hotels)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE Hotel.Hotels " +
                "SET hotel_name = @hotel_name, " +
                "hotel_description = @hotel_description, " +
                "hotel_rating_star = @hotel_rating_star, " +
                "hotel_phonenumber = @hotel_phonenumber, " +
                "hotel_modified_date = GETDATE(), " +
                "hotel_addr_id = @hotel_addr_id " +
                "WHERE hotel_id = @hotel_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_id",
                        DataType = DbType.Int32,
                        Value = hotels.hotel_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_name",
                        DataType = DbType.String,
                        Value = hotels.hotel_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_description",
                        DataType = DbType.String,
                        Value = hotels.hotel_description
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_rating_star",
                        DataType = DbType.Int16,
                        Value = hotels.hotel_rating_star
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_phonenumber",
                        DataType = DbType.String,
                        Value = hotels.hotel_phonenumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_addr_id",
                        DataType = DbType.Int32,
                        Value = hotels.hotel_addr_id
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Hotels hotels)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM [Hotel].[Hotels] " +
                "WHERE [hotel_id] = @hotel_id",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@hotel_id",
                        DataType = DbType.Int32,
                        Value = hotels.hotel_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
        
    }
}
