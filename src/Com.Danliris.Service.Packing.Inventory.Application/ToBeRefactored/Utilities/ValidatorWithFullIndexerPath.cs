using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities
{
    public abstract class ValidatorWithFullIndexerPath<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);
            FixIndexedPropertyErrorMessage(result);

            return result;
        }
        public override async Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default(CancellationToken))
        {
            var result = await base.ValidateAsync(context, cancellation);
            FixIndexedPropertyErrorMessage(result);

            return result;
        }

        protected void FixIndexedPropertyErrorMessage(ValidationResult result)
        {
            if (result.Errors?.Any() ?? false)
            {
                foreach (var error in result.Errors)
                {
                    // check if 
                    if (Regex.IsMatch(error.PropertyName, @"\[\d+\]") &&
                        error.FormattedMessagePlaceholderValues.TryGetValue("PropertyName", out var propertyName))
                    {
                        string arrayPropertyName = error.PropertyName.Split('[')[0];
                        int index = Convert.ToInt32(error.PropertyName.Split('[', ']')[1]);
                        var selectedError = result.Errors.FirstOrDefault(s => s.PropertyName == arrayPropertyName);
                        if (selectedError == null)
                        {
                            List<dynamic> obj = new List<dynamic>()
                            {
                                new
                                {
                                    propertyName = 
                                }
                            };
                            result.Errors.Add(new ValidationFailure(arrayPropertyName, "[]"));
                        }
                        else
                        {

                        }

                        // replace PropertyName with its full path
                        error.ErrorMessage = error.ErrorMessage
                            .Replace($"'{propertyName}'", $"'{error.PropertyName}'");
                    }
                }
            }
        }
    }
}
