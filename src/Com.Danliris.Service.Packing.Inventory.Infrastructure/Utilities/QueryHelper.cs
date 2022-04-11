using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities
{
    public static class QueryHelper<TModel>
    {
        public static IQueryable<TModel> Filter(IQueryable<TModel> query, Dictionary<string, object> filterDictionary)
        {
            if (filterDictionary != null && !filterDictionary.Count.Equals(0))
            {
                foreach (var f in filterDictionary)
                {
                    string key = f.Key;
                    object Value = f.Value;
                    string filterQuery = string.Concat(string.Empty, key, " == @0");

                    query = query.Where(filterQuery, Value);
                }
            }
            return query;
        }

        public static IQueryable<TModel> Order(IQueryable<TModel> query, Dictionary<string, string> orderDictionary, bool ignoreDot = false)
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
                string Key = orderDictionary.Keys.First();
                string OrderType = orderDictionary[Key];

                query = query.OrderBy(string.Concat(ignoreDot ? Key : Key.Replace(".", ""), " ", OrderType));
            }
            return query;
        }

        public static IQueryable<TModel> Search(IQueryable<TModel> query, List<string> searchAttributes, string keyword, bool ToLowerCase = false, bool ignoreDot = false)
        {
            /* Search with Keyword */
            if (keyword != null)
            {
                string SearchQuery = String.Empty;
                foreach (string Attribute in searchAttributes)
                {
                    if (Attribute.Contains(".") && !ignoreDot)
                    {
                        var Key = Attribute.Split(".");
                        SearchQuery = string.Concat(SearchQuery, Key[0], $".Any({Key[1]}.Contains(@0)) OR ");
                    }
                    else
                    {
                        SearchQuery = string.Concat(SearchQuery, Attribute, ".Contains(@0) OR ");
                    }
                }

                SearchQuery = SearchQuery.Remove(SearchQuery.Length - 4);

                if (ToLowerCase)
                {
                    SearchQuery = SearchQuery.Replace(".Contains(@0)", ".ToLower().Contains(@0)");
                    keyword = keyword.ToLower();
                }

                query = query.Where(SearchQuery, keyword);
            }
            return query;
        }

        public static IQueryable ConfigureSelect(IQueryable<TModel> Query, Dictionary<string, string> SelectDictionary)
        {
            /* Custom Select */
            if (SelectDictionary != null && !SelectDictionary.Count.Equals(0))
            {
                var listHeaderColumns = SelectDictionary.Where(d => !d.Key.Contains("."))
                    .Select(d => (d.Value == "1") ? d.Key : string.Concat(d.Value, " as ", d.Key));

                var listChildColumns = SelectDictionary
                    .Where(d => d.Key.Contains(".") && d.Value == "1")
                    .Select(s =>
                    {
                        var keys = s.Key.Split(".");
                        return new KeyValuePair<string, string>(keys[0], keys[1]);
                    })
                    .GroupBy(g => g.Key)
                    .Select(s => string.Concat(s.Key, ".Select(new(", string.Join(",", s.Select(ss => ss.Value)), ")) as ", s.Key));

                var listColumns = listHeaderColumns.Concat(listChildColumns);

                string selectedColumns = string.Join(", ", listColumns);

                var SelectedQuery = Query.Select(string.Concat("new(", selectedColumns, ")"));

                return SelectedQuery;
            }

            /* Default Select */
            return Query;
        }
    }
}
