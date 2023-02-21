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
    internal class FacilityPhotosRepository : RepositoryBase<Facility_Photos>, IFacilityPhotosRepository
    {
        public FacilityPhotosRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public async Task<IEnumerable<Facility_Photos>> FindAllFacilityPhotosAsync(int faciId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * from hotel.Facility_Photos WHERE fapho_faci_id = @fapho_faci_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_faci_id",
                        DataType = DbType.Int32,
                        Value = faciId
                    },}
            };

            IAsyncEnumerator<Facility_Photos> dataSet = FindAllAsync<Facility_Photos>(model);
            var item = new List<Facility_Photos>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<Facility_Photos> FindFacilityPhotosByIdAsync(int faciId, int faphoId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * from hotel.Facility_Photos WHERE fapho_faci_id = @fapho_faci_id AND fapho_id = @fapho_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_faci_id",
                        DataType = DbType.Int32,
                        Value = faciId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_id",
                        DataType = DbType.Int32,
                        Value = faphoId
                    },
                }
            };

            IAsyncEnumerator<Facility_Photos> dataSet = FindAllAsync<Facility_Photos>(model);
            Facility_Photos? item = dataSet.Current;

            while (await dataSet.MoveNextAsync())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public void Insert(Facility_Photos facilityPhotos)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO Hotel.Facility_Photos " +
                "(fapho_thumbnail_filename, fapho_photo_filename, fapho_primary, fapho_url, fapho_modified_date, fapho_faci_id) " +
                "VALUES(@fapho_thumbnail_filename, @fapho_photo_filename, @fapho_primary, @fapho_url, GETDATE(), @fapho_faci_id); " +
                "SELECT CAST(scope_identity() as int);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_thumbnail_filename",
                        DataType = DbType.String,
                        Value = facilityPhotos.fapho_thumbnail_filename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_photo_filename",
                        DataType = DbType.String,
                        Value = facilityPhotos.fapho_photo_filename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_primary",
                        DataType = DbType.Boolean,
                        Value = facilityPhotos.fapho_primary
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_url",
                        DataType = DbType.String,
                        Value = facilityPhotos.fapho_url
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_faci_id",
                        DataType = DbType.Int32,
                        Value = facilityPhotos.fapho_faci_id
                    },
                }
            };

            facilityPhotos.fapho_id = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Edit(Facility_Photos facilityPhotos)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE Hotel.Facility_Photos " +
                "SET " +
                "fapho_thumbnail_filename = @fapho_thumbnail_filename, " +
                "fapho_photo_filename = @fapho_photo_filename, " +
                "fapho_primary = @fapho_primary, " +
                "fapho_url = @fapho_url, " +
                "fapho_modified_date = GETDATE(), " +
                "fapho_faci_id = @fapho_faci_id " +
                "WHERE fapho_id = @fapho_id;", 
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_id",
                        DataType = DbType.Int32,
                        Value = facilityPhotos.fapho_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_thumbnail_filename",
                        DataType = DbType.String,
                        Value = facilityPhotos.fapho_thumbnail_filename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_photo_filename",
                        DataType = DbType.String,
                        Value = facilityPhotos.fapho_photo_filename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_primary",
                        DataType = DbType.Boolean,
                        Value = facilityPhotos.fapho_primary
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_url",
                        DataType = DbType.String,
                        Value = facilityPhotos.fapho_url
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_faci_id",
                        DataType = DbType.Int32,
                        Value = facilityPhotos.fapho_faci_id
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Facility_Photos facilityPhotos)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Hotel.Facility_Photos " +
                "WHERE fapho_id = @fapho_id; ",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_id",
                        DataType = DbType.Int32,
                        Value = facilityPhotos.fapho_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Facility_Photos> FindAllFacilityPhotos()
        {
            throw new NotImplementedException();
        }
    }
}
