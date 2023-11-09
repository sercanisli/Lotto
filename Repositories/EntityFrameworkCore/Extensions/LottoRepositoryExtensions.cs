using System.Linq.Dynamic.Core;

namespace Repositories.EntityFrameworkCore.Extensions
{
    public static class LottoRepositoryExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> entities, string orderByQueryString) 
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                var defaultOrderProperty = typeof(T).GetProperty("Date");
                if(defaultOrderProperty != null)
                {
                    return entities.OrderBy(e => defaultOrderProperty.GetValue(e));
                }
                return entities;
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<T>(orderByQueryString);

            if(orderQuery == null)
            {
                return entities;
            }
            return entities.OrderBy(orderQuery);
        }
    }
}
