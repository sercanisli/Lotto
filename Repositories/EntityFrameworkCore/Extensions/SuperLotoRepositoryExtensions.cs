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

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<SuperLoto>(orderByQueryString);

            if(orderQuery == null)
            {
                return entities.OrderBy(e => e.Date);
            }
            return entities.OrderBy(orderQuery);
        }
    }
}
