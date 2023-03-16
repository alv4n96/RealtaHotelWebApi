using Microsoft.VisualBasic;
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
    internal class FacilitiesRepository : RepositoryBase<Facilities>, IFacilitiesRepository
    {
        public FacilitiesRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }


        public async Task<IEnumerable<Facilities>> FindAllFacilitiesAsync(int hotelId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT " +
                              "faci_id AS FaciId " +
                              ",faci_name AS FaciName " +
                              ",faci_description AS FaciDescription " +
                              ",faci_max_number AS FaciMaxNumber  " +
                              ",faci_measure_unit AS FaciMeasureUnit  " +
                              ",faci_room_number AS FaciRoomNumber " +
                              ",faci_startdate AS FaciStartdate " +
                              ",faci_enddate AS FaciEnddate " +
                              ",faci_low_price AS FaciLowPrice " +
                              ",faci_high_price AS FaciHighPrice " +
                              ",faci_rate_price AS FaciRatePrice " +
                              ",faci_expose_price AS FaciExposePrice " +
                              ",faci_discount AS FaciDiscount " +
                              ",faci_tax_rate AS FaciTaxRate " +
                              ",faci_modified_date AS FaciModifiedDate " +
                              ",faci_cagro_id AS FaciCagroId " +
                              ",faci_hotel_id AS FaciHotelId " +
                              ",faci_user_id AS FaciUserId " +
                              "from hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },
                }
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
                CommandText = "SELECT " +
                              "faci_id AS FaciId " +
                              ",faci_name AS FaciName " +
                              ",faci_description AS FaciDescription " +
                              ",faci_max_number AS FaciMaxNumber " +
                              ",faci_measure_unit AS FaciMeasureUnit  " +
                              ",faci_room_number AS FaciRoomNumber " +
                              ",faci_startdate AS FaciStartdate " +
                              ",faci_enddate AS FaciEnddate " +
                              ",faci_low_price AS FaciLowPrice " +
                              ",faci_high_price AS FaciHighPrice " +
                              ",faci_rate_price AS FaciRatePrice " +
                              ",faci_expose_price AS FaciExposePrice " +
                              ",faci_discount AS FaciDiscount " +
                              ",faci_tax_rate AS FaciTaxRate " +
                              ",faci_modified_date AS FaciModifiedDate " +
                              ",faci_cagro_id AS FaciCagroId " +
                              ",faci_hotel_id AS FaciHotelId " +
                              ",faci_user_id AS FaciUserId " +
                              "FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id AND faci_id = @faci_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },
                    new SqlCommandParameterModel()
                    {
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

        public void Insert(Facilities facilities)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO Hotel.Facilities " +
                              "(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_endate, faci_low_price, faci_high_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id)" +
                              "VALUES(@faci_name, @faci_description, @faci_max_number, " +
                              "@faci_measure_unit, @faci_room_number, @faci_startdate, " +
                              "@faci_endate, @faci_low_price, @faci_high_price, " +
                              "@faci_discount, @faci_tax_rate, @faci_cagro_id, " +
                              "@faci_hotel_id, @faci_user_id);" +
                              "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_name",
                        DataType = DbType.String,
                        Value = facilities.FaciName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_description",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(facilities.FaciDescription)
                            ? DBNull.Value
                            : facilities.FaciDescription
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_max_number",
                        DataType = DbType.Int32,
                        Value = facilities.FaciMaxNumber == 0 ? DBNull.Value : facilities.FaciMaxNumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_measure_unit",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(facilities.FaciMeasureUnit)
                            ? DBNull.Value
                            : facilities.FaciMeasureUnit
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_room_number",
                        DataType = DbType.String,
                        Value = facilities.FaciRoomNumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_startdate",
                        DataType = DbType.DateTime,
                        Value = facilities.FaciStartdate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_endate",
                        DataType = DbType.DateTime,
                        Value = facilities.FaciEndDate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_low_price",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciLowPrice
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_high_price",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciHighPrice
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_discount",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciDiscount == 0 ? DBNull.Value : facilities.FaciDiscount
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_tax_rate",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciTaxRate == 0 ? DBNull.Value : facilities.FaciTaxRate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_cagro_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciCagroId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciHotelId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_user_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciUserId
                    },
                }
            };

            facilities.FaciId = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Edit(Facilities facilities)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE Hotel.Facilities " +
                              "SET " +
                              "faci_name = @faci_name, " +
                              "faci_description = @faci_description, " +
                              "faci_max_number = @faci_max_number, " +
                              "faci_measure_unit = @faci_measure_unit, " +
                              "faci_room_number = @faci_room_number, " +
                              "faci_startdate = @faci_startdate, " +
                              "faci_endate = @faci_endate, " +
                              "faci_low_price = @faci_low_price, " +
                              "faci_high_price = @faci_high_price, " +
                              "faci_discount = @faci_discount, " +
                              "faci_tax_rate = @faci_tax_rate, " +
                              "faci_cagro_id = @faci_cagro_id, " +
                              "faci_hotel_id = @faci_hotel_id, " +
                              "faci_user_id = @faci_user_id " +
                              "WHERE faci_id = @faci_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_id",
                        DataType = DbType.String,
                        Value = facilities.FaciId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_name",
                        DataType = DbType.String,
                        Value = facilities.FaciName
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_description",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(facilities.FaciDescription)
                            ? DBNull.Value
                            : facilities.FaciDescription
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_max_number",
                        DataType = DbType.Int32,
                        Value = facilities.FaciMaxNumber == 0 ? DBNull.Value : facilities.FaciMaxNumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_measure_unit",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(facilities.FaciMeasureUnit)
                            ? DBNull.Value
                            : facilities.FaciMeasureUnit
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_room_number",
                        DataType = DbType.String,
                        Value = facilities.FaciRoomNumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_startdate",
                        DataType = DbType.DateTime,
                        Value = facilities.FaciStartdate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_endate",
                        DataType = DbType.DateTime,
                        Value = facilities.FaciEndDate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_low_price",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciLowPrice
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_high_price",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciHighPrice
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_discount",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciDiscount == 0 ? DBNull.Value : facilities.FaciDiscount
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_tax_rate",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciTaxRate == 0 ? DBNull.Value : facilities.FaciTaxRate
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_cagro_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciCagroId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciHotelId
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_user_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciUserId
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Facilities facilities)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Hotel.Facilities WHERE faci_id = @faci_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Facilities> FindAllFacilities()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Facilities>> GetFacilitiesPageList(FacilitiesParameters facilitiesParam, int hotelId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT faci_id AS FaciId 
,faci_name AS FaciName 
,faci_description AS FaciDescription 
,faci_max_number AS FaciMaxNumber 
,faci_measure_unit AS FaciMeasureUnit  
,faci_room_number AS FaciRoomNumber 
,faci_startdate AS FaciStartdate 
,faci_enddate AS FaciEnddate 
,faci_low_price AS FaciLowPrice 
,faci_high_price AS FaciHighPrice 
,faci_rate_price AS FaciRatePrice 
,faci_expose_price AS FaciExposePrice 
,faci_discount AS FaciDiscount 
,faci_tax_rate AS FaciTaxRate 
,faci_modified_date AS FaciModifiedDate 
,faci_cagro_id AS FaciCagroId 
,faci_hotel_id AS FaciHotelId 
,faci_user_id AS FaciUserId 
FROM Hotel.Facilities 
WHERE faci_hotel_id = @faci_hotel_id
ORDER BY faci_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = hotelId
                    },
                }
            };

            var facilities = await GetAllAsync<Facilities>(model);
            //var totalRow = FindAllHotels().Count();



            var hotelSearch = facilities.AsQueryable()
                .SearchFacilities(facilitiesParam.SearchTerm);

            return PagedList<Facilities>.ToPagedList(hotelSearch.ToList(), facilitiesParam.PageNumber, facilitiesParam.PageSize);

        }
    }
}