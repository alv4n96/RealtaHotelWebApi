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
    internal class FacilityPhotosRepository : RepositoryBase<FacilityPhotos>, IFacilityPhotosRepository
    {
        public FacilityPhotosRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public async Task<IEnumerable<FacilityPhotos>> FindAllFacilityPhotosAsync(int faciId)
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

            IAsyncEnumerator<FacilityPhotos> dataSet = FindAllAsync<FacilityPhotos>(model);
            var item = new List<FacilityPhotos>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<FacilityPhotos> FindFacilityPhotosByIdAsync(int faciId, int faphoId)
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

            IAsyncEnumerator<FacilityPhotos> dataSet = FindAllAsync<FacilityPhotos>(model);
            FacilityPhotos? item = dataSet.Current;

            while (await dataSet.MoveNextAsync())
            {
                item = dataSet.Current;
            }

            return item;
        }

        public void Insert(FacilityPhotos facilityPhotos)
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
                        Value = facilityPhotos.FaphoThumbnailFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_photo_filename",
                        DataType = DbType.String,
                        Value = facilityPhotos.FaphoPhotoFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_primary",
                        DataType = DbType.Boolean,
                        Value = facilityPhotos.FaphoPrimary
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_url",
                        DataType = DbType.String,
                        Value = facilityPhotos.FaphoUrl
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_faci_id",
                        DataType = DbType.Int32,
                        Value = facilityPhotos.FaphoFaciId
                    },
                }
            };

            facilityPhotos.FaphoId = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Edit(FacilityPhotos facilityPhotos)
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
                        Value = facilityPhotos.FaphoId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_thumbnail_filename",
                        DataType = DbType.String,
                        Value = facilityPhotos.FaphoThumbnailFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_photo_filename",
                        DataType = DbType.String,
                        Value = facilityPhotos.FaphoPhotoFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_primary",
                        DataType = DbType.Boolean,
                        Value = facilityPhotos.FaphoPrimary
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_url",
                        DataType = DbType.String,
                        Value = facilityPhotos.FaphoUrl
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@fapho_faci_id",
                        DataType = DbType.Int32,
                        Value = facilityPhotos.FaphoFaciId
                    },
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(FacilityPhotos facilityPhotos)
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
                        Value = facilityPhotos.FaphoId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<FacilityPhotos> FindAllFacilityPhotos()
        {
            throw new NotImplementedException();
        }
    }
}
