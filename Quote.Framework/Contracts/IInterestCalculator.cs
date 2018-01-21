namespace Quote.Framework
{
    public interface IInterestCalculator
    {
        decimal Calculate(InterestCalculationParameters parameters);
    }
}
