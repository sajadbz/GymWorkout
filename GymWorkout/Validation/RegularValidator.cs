using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace GymWorkout.Validation
{
    public class RegularValidator : ValidationRule
    {
        public RegularValidator()
        {
            _pattern = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            _message = "ایمیل وارد شده صحیح نمی باشد";
        }
        private string _pattern;

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null )
            {
                var text = value.ToString().Trim();
                if (text != "")
                {
                    var isValid = !Regex.IsMatch(text, _pattern);
                    if (isValid)
                        return new ValidationResult(false, _message);
                }
                
            }
            return new ValidationResult(true, null);
        }
    }
}
