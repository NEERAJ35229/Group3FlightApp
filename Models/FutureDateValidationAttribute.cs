using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Group3Flight.Models
{
    public class FutureDateValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        private int maxYears;

        public FutureDateValidationAttribute(int years)
        {
            maxYears = years;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext ctx)
        {
            if (value is DateTime date)
            {
                DateTime today = DateTime.Today;

                if (date <= today)
                {
                    return new ValidationResult(GetMsg(ctx.DisplayName ?? "Date"));
                }

                if (date > today.AddYears(maxYears))
                {
                    return new ValidationResult($"Date cannot be more than {maxYears} years ahead.");
                }

                return ValidationResult.Success!;
            }

            return new ValidationResult(GetMsg(ctx.DisplayName ?? "Date"));
        }

        // CLIENT-SIDE VALIDATION
        public void AddValidation(ClientModelValidationContext ctx)
        {
            if (!ctx.Attributes.ContainsKey("data-val"))
                ctx.Attributes.Add("data-val", "true");
            ctx.Attributes.Add("data-val-futuredate-years", maxYears.ToString());
            ctx.Attributes.Add("data-val-futuredate",
                GetMsg(ctx.ModelMetadata.DisplayName ?? ctx.ModelMetadata.Name ?? "Date"));
        }

        private string GetMsg(string name) =>
            base.ErrorMessage ?? $"{name} must be greater than today and within {maxYears} years.";
    }
}
