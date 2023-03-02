using Microsoft.VisualBasic;
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

namespace Realta.Persistence.Repositories
{
    internal class FacilityPriceHistoryRepository : RepositoryBase<FacilityPriceHistory>, IFacilityPriceHistoryRepository
    {
        public FacilityPriceHistoryRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public async Task<IEnumerable<FacilityPriceHistory>> FindAllFacilityPriceHistoryAsync(int hotelId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT faph.* " +
                "FROM Hotel.Facility_Price_History faph " +
                "INNER JOIN Hotel.Facilities faci ON faph.faph_faci_id = faci.faci_id " +
                "WHERE faci.faci_hotel_id = @faci_hotel_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },}
            };

            IAsyncEnumerator<FacilityPriceHistory> dataSet = FindAllAsync<FacilityPriceHistory>(model);
            var item = new List<FacilityPriceHistory>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<IEnumerable<FacilityPriceHistory>> FindAllFacilityPriceHistoryByFacilityAsync(int hotelId, int faciId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT faph.* " +
                "FROM Hotel.Facility_Price_History faph " +
                "INNER JOIN Hotel.Facilities faci ON faph.faph_faci_id = faci.faci_id " +
                "WHERE faci.faci_hotel_id = @faci_hotel_id AND faph.faph_faci_id = @faph_faci_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faph_faci_id",
                        DataType = DbType.Int32,
                        Value = faciId
                    },
                }
            };

            IAsyncEnumerator<FacilityPriceHistory> dataSet = FindAllAsync<FacilityPriceHistory>(model);
            var item = new List<FacilityPriceHistory>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<FacilityPriceHistory> FindAllFacilityPriceHistoryByIdAsync(int faciId, int faphId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Hotel.Facility_Price_History WHERE faph_id = @faph_id AND faph_faci_id = @faph_faci_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@faph_faci_id",
                        DataType = DbType.Int32,
                        Value = faciId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faph_id",
                        DataType = DbType.Int32,
                        Value = faphId
                    },
                }
            };

            IAsyncEnumerator<FacilityPriceHistory> dataSet = FindAllAsync<FacilityPriceHistory>(model);
            FacilityPriceHistory? item = dataSet.Current;

            while (await dataSet.MoveNextAsync())
            {
                item = dataSet.Current;
            }

            return item;
        }
    }
}
