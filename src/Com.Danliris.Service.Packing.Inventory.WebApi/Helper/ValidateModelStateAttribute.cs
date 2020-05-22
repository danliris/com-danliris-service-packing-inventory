using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Helper
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                if (!context.ModelState.IsValid)
                {
                    var errors = context.ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                    var responseObj = new
                    {
                        message = "data does not pass validation",
                        error = errors,
                        statusCode = 400
                    };

                    context.Result = new JsonResult(responseObj)
                    {
                        StatusCode = 400
                    };
                }
            }
        }
    }
}
