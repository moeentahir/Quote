namespace Quote.Framework
{
    public interface ILoanRequestedAmountValidationRule
    {
        void Validate(int requestedAmount);
    }
}
