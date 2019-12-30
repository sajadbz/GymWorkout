using System.Globalization;
using System.Windows.Controls;

namespace GymWorkout.Application.Validation
{
    class RangeValidator : ValidationRule
    {
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

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Message = "عدد وارد شده می بایست بین " + _min + " تا " + _max + " باشد";
            if (value != null && value.ToString() != "")
            {
                int number = 0;
                if (int.TryParse(value.ToString(), out number))
                {
                    if (number >= _min && number <= _max)
                        return new ValidationResult(true, null);
                }
            }
            return new ValidationResult(false, Message);
        }
    }
}
