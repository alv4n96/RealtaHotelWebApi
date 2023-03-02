﻿using Microsoft.VisualBasic;
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
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_name",
                        DataType = DbType.String,
                        Value = facilities.FaciName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_description",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(facilities.FaciDescription) ? DBNull.Value : facilities.FaciDescription
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_max_number",
                        DataType = DbType.Int32,
                        Value = facilities.FaciMaxNumber == 0 ? DBNull.Value : facilities.FaciMaxNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_measure_unit",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(facilities.FaciMeasureUnit) ? DBNull.Value : facilities.FaciMeasureUnit
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_room_number",
                        DataType = DbType.String,
                        Value = facilities.FaciRoomNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_startdate",
                        DataType = DbType.DateTime,
                        Value = facilities.FaciStartdate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_endate",
                        DataType = DbType.DateTime,
                        Value = facilities.FaciEndate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_low_price",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciLowPrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_high_price",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciHighPrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_discount",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciDiscount == 0 ? DBNull.Value : facilities.FaciDiscount
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_tax_rate",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciTaxRate == 0 ? DBNull.Value : facilities.FaciTaxRate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_cagro_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciCagroId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciHotelId
                    },
                    new SqlCommandParameterModel() {
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
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_id",
                        DataType = DbType.String,
                        Value = facilities.FaciId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_name",
                        DataType = DbType.String,
                        Value = facilities.FaciName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_description",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(facilities.FaciDescription) ? DBNull.Value : facilities.FaciDescription
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_max_number",
                        DataType = DbType.Int32,
                        Value = facilities.FaciMaxNumber == 0 ? DBNull.Value : facilities.FaciMaxNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_measure_unit",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(facilities.FaciMeasureUnit) ? DBNull.Value : facilities.FaciMeasureUnit
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_room_number",
                        DataType = DbType.String,
                        Value = facilities.FaciRoomNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_startdate",
                        DataType = DbType.DateTime,
                        Value = facilities.FaciStartdate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_endate",
                        DataType = DbType.DateTime,
                        Value = facilities.FaciEndate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_low_price",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciLowPrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_high_price",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciHighPrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_discount",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciDiscount == 0 ? DBNull.Value : facilities.FaciDiscount
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_tax_rate",
                        DataType = DbType.Decimal,
                        Value = facilities.FaciTaxRate == 0 ? DBNull.Value : facilities.FaciTaxRate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_cagro_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciCagroId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@faci_hotel_id",
                        DataType = DbType.Int32,
                        Value = facilities.FaciHotelId
                    },
                    new SqlCommandParameterModel() {
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
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
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
    }
}
