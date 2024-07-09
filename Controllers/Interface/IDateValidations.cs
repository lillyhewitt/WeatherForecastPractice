namespace WebApplication1.Interface
{
    public interface IDateValidations
    {
        bool IsDateWithinMonths(DateTime date, int months);
        bool IsDateWithinYears(DateTime startDate, int years);
    }
}
