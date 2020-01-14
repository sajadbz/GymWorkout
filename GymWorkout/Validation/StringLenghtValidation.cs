using System.Globalization;
using System.Windows.Controls;

namespace GymWorkout.Validation
{
    public class StringLenghtValidation : ValidationRule
    {
        public StringLenghtValidation()
        {
            _min = 0;
            _max = 50;
        }
        private int _min;

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }
        private int _max;

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var message = "متن وارد شده می بایست بین " + _min + " تا " + _max + "کاراکتر باشد";
            if (value == null || value.ToString() == "")
                return new ValidationResult(true, null);
            else
            {
                int number = value.ToString().Length;
                if (number >= _min && number <= _max)
                    return new ValidationResult(true, null);
                return new ValidationResult(false, message);
            }
        }
    }
}
