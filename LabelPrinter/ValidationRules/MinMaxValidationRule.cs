using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace LabelPrinter.ValidationRules
{
    public class MinMaxValidationRule : ValidationRule
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var message = $"Value must be between {MinValue} and {MaxValue}";

            if (!int.TryParse($"{value}", out int intValue))
                return new ValidationResult(false, "Not a valid integer");

            if (intValue < MinValue || intValue > MaxValue)
                return new ValidationResult(false, message);

            return ValidationResult.ValidResult;
        }
    }
}
