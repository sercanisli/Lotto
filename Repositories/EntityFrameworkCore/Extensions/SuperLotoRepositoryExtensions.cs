using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repositories.EntityFrameworkCore.Extensions
{
    public static class SuperLotoRepositoryExtensions
    {
        public static IQueryable<SuperLoto> Sort(this IQueryable<SuperLoto> entities, string orderByQueryString) 
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities.OrderBy(e => e.Date);
            }

            var orderParams = orderByQueryString.Trim().Split(',');

            var propertyInfos = typeof(SuperLoto).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            foreach ( var param in orderParams) 
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }
                var propertyFromQueryName = param.Split(' ')[0];

                var objectProperty = propertyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.OrdinalIgnoreCase));

                if(objectProperty == null)
                {
                    continue;
                }
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',',' ');
            if(orderQuery == null)
            {
                return entities.OrderBy(e => e.Date);
            }
            return entities.OrderBy(orderQuery);
        }
    }
}
