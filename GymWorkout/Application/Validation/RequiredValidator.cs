using System.Globalization;
using System.Windows.Controls;

namespace GymWorkout.Application.Validation
{
    class RequiredValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                var text = value.ToString().Trim();
                if (string.IsNullOrEmpty(text))
                    return new ValidationResult(false, "لطفا فیلد مورد نظر را پر کنید");
                else
                    return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "لطفا فیلد مورد نظر را پر کنید");
        }
    }
}
