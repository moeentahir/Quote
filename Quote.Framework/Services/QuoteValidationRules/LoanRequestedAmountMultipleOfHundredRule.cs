using Quote.Common;

namespace Quote.Framework
{
    public class LoanRequestedAmountMultipleOfHundredRule : ILoanRequestedAmountValidationRule
    {
        public void Validate(int requestedAmount)
        {

            if ((requestedAmount % 100M) != 0)
                throw new ValidationException("Loan amound should be multiple of 100.");
        }
    }
}
