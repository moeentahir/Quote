using Quote.Common;

namespace Quote.Framework
{
    public class LoanRequestedAmoundMaximumAmountRule : ILoanRequestedAmountValidationRule
    {
        public void Validate(int requestedAmount)
        {

            if (requestedAmount > 15000)
                throw new ValidationException("Loan amound should be less than or equal to £15,000.");
        }
    }
}
