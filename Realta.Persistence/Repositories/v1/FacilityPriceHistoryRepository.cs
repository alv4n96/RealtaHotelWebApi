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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories.v1
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
                CommandText = "SELECT " +
                "faph.faph_id AS FaphId" +
                ",faph.faph_startdate AS FaphStartdate" +
                ",faph.faph_enddate AS FaphEnddate" +
                ",faph.faph_low_price AS FaphLowPrice" +
                ",faph.faph_high_price AS FaphHighPrice" +
                ",faph.faph_rate_price AS FaphRatePrice" +
                ",faph.faph_discount AS FaphDiscount" +
                ",faph.faph_tax_rate AS FaphTaxRate" +
                ",faph.faph_modified_date AS FaphModifiedDate" +
                ",faph.faph_faci_id AS FaphFaciId" +
                ",faph.faph_user_id AS FaphUserId " +
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
                CommandText = "SELECT " +
                "faph.faph_id AS FaphId" +
                ",faph.faph_startdate AS FaphStartdate" +
                ",faph.faph_enddate AS FaphEnddate" +
                ",faph.faph_low_price AS FaphLowPrice" +
                ",faph.faph_high_price AS FaphHighPrice" +
                ",faph.faph_rate_price AS FaphRatePrice" +
                ",faph.faph_discount AS FaphDiscount" +
                ",faph.faph_tax_rate AS FaphTaxRate" +
                ",faph.faph_modified_date AS FaphModifiedDate" +
                ",faph.faph_faci_id AS FaphFaciId" +
                ",faph.faph_user_id AS FaphUserId " +
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
                CommandText = "SELECT " +
                "faph.faph_id AS FaphId" +
                ",faph.faph_startdate AS FaphStartdate" +
                ",faph.faph_enddate AS FaphEnddate" +
                ",faph.faph_low_price AS FaphLowPrice" +
                ",faph.faph_high_price AS FaphHighPrice" +
                ",faph.faph_rate_price AS FaphRatePrice" +
                ",faph.faph_discount AS FaphDiscount" +
                ",faph.faph_tax_rate AS FaphTaxRate" +
                ",faph.faph_modified_date AS FaphModifiedDate" +
                ",faph.faph_faci_id AS FaphFaciId" +
                ",faph.faph_user_id AS FaphUserId " +
                "FROM Hotel.Facility_Price_History faph " +
                "WHERE faph_id = @faph_id AND faph_faci_id = @faph_faci_id;",
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

        public Task<PagedList<FacilityPriceHistory>> GetFacilityPriceHistoryPageList(HistoryParameters historyParam, int hotelId, int faciId)
        {
            throw new NotImplementedException();
        }
    }
}
