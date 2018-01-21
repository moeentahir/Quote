using Quote.Common;

namespace Quote.Framework
{
    public class LoanRequestedAmoundMinimumAmountRule : ILoanRequestedAmountValidationRule
    {
        public void Validate(int requestedAmount) {

            if (requestedAmount < 1000)
                throw new ValidationException("Loan amound should be greater than or equal to £1,000.");
        }
    }
}
