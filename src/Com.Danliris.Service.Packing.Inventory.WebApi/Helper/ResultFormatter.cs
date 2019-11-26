using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Helper
{
    public static class ResultFormatter
    {
        public static Dictionary<string, string> FormatErrorMessage(ModelStateDictionary modelState)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in modelState.Keys)
            {
                var errorValue = modelState[key].Errors.FirstOrDefault();
                result.Add(key, errorValue.ErrorMessage);
            }

            return result;
        }
    }
}