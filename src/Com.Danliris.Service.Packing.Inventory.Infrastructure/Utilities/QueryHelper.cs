using Com.Moonlay.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities
{
    public static class QueryHelper<TModel>
    {
        public static IQueryable<TModel> Order(IQueryable<TModel> query, Dictionary<string, string> orderDictionary)
        {
            /* Default Order */
            if (orderDictionary.Count.Equals(0))
            {
                string key = "LastModifiedUtc";
                string orderType = "desc";
                query = query.OrderBy(string.Concat(key, " ", orderType));
            }
            /* Custom Order */
            else
            {
                string key = orderDictionary.Keys.First();
                string orderType = orderDictionary[key];
                query = query.OrderBy(string.Concat(key.Replace(".", ""), " ", orderType));
            }
            return query;
        }
    }
}
