using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Realta.Domain.Entities;

namespace Realta.Persistence.Repositories.RepositoriesExtensions
{
    public static class RepositoryFacilitiesExtensions
    {
        public static IQueryable<Facilities> SearchFacilities(this IQueryable<Facilities> faci, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return faci;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            
            return faci.Where(p => p.FaciName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Facilities> Sort(this IQueryable<Facilities> faci, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return faci.OrderBy(e => e.FaciName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Facilities).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return faci.OrderBy(e => e.FaciName);

            return faci.OrderBy(orderQuery);
        }
    }
}
