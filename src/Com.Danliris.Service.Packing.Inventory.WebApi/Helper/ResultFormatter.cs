using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Helper
{
    public static class ResultFormatter
    {
        public static Dictionary<string, string> FormatErrorMessage(ModelStateDictionary modelState)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count > 1)
                {
                    var errorValue = modelState[key].Errors.FirstOrDefault();
                    result.Add(key, errorValue.ErrorMessage);
                }
                else
                {
                    var errorValue = modelState[key].Errors.FirstOrDefault();
                    result.Add(key, errorValue.ErrorMessage);
                }

            }

            return result;
        }

        public static Dictionary<string, object> Fail(ServiceValidationException e)
        {
            Dictionary<string, object> Errors = new Dictionary<string, object>();

            foreach (ValidationResult error in e.ValidationResults)
            {
                string key = error.MemberNames.First();

                try
                {
                    Errors.Add(error.MemberNames.First(), JsonConvert.DeserializeObject(error.ErrorMessage));
                }
                catch (Exception)
                {
                    Errors.Add(error.MemberNames.First(), error.ErrorMessage);
                }
            }

            return Errors;
        }
    }

    public class ResultFormatterNew
    {
        public Dictionary<string, object> Result { get; set; }

        public ResultFormatterNew(string ApiVersion, int StatusCode, string Message)
        {
            Result = new Dictionary<string, object>();
            AddResponseInformation(Result, ApiVersion, StatusCode, Message);
        }

        public Dictionary<string, object> Ok()
        {
            return Result;
        }

        public Dictionary<string, object> Ok(object Data)
        {
            Result.Add("data", Data);
            return Result;
        }

        public Dictionary<string, object> Ok(object Data, object Info)
        {
            Result.Add("data", Data);
            Result.Add("info", Info);
            return Result;
        }

        public Dictionary<string, object> Fail(string Error)
        {
            Result.Add("error", Error);
            return Result;
        }

        public Dictionary<string, object> Fail()
        {
            Result.Add("error", "Request Failed");
            return Result;
        }

        public Dictionary<string, object> Fail(ServiceValidationExeption e)
        {
            Dictionary<string, object> Errors = new Dictionary<string, object>();

            foreach (ValidationResult error in e.ValidationResults)
            {
                string key = error.MemberNames.First();

                try
                {
                    Errors.Add(error.MemberNames.First(), JsonConvert.DeserializeObject(error.ErrorMessage));
                }
                catch (Exception)
                {
                    Errors.Add(error.MemberNames.First(), error.ErrorMessage);
                }
            }

            Result.Add("error", Errors);
            return Result;
        }

        public void AddResponseInformation(Dictionary<string, object> Result, string ApiVersion, int StatusCode, string Message)
        {
            Result.Add("apiVersion", ApiVersion);
            Result.Add("statusCode", StatusCode);
            Result.Add("message", Message);
        }
    }
}