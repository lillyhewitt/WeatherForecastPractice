using WebApplication1.Interface;

namespace WebApplication1.Validations
{
    public class TimeValidation : ITimeValidations
    {
        // check if hour is between 1-24
        public bool IsValid(int hour)
        {
            // return false if hour is not between 1-24
            if (hour < 1 || hour > 24)
            {
                return false;
            }
            return true;
        }
    }
}
