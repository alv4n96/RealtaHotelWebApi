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

        public Task<Facility_Photos> FindFacilityPhotosByIdAsync(int faciId, int faPhoId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Facility_Photos facilityPhotos)
        {
            throw new NotImplementedException();
        }

        public void Edit(Facility_Photos facilityPhotos)
        {
            throw new NotImplementedException();
        }

        public void Remove(Facility_Photos facilityPhotos)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Facility_Photos> FindAllFacilityPhotos()
        {
            throw new NotImplementedException();
        }
    }
}
