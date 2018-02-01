using Quote.Common;

namespace Quote.Framework
{
    public class LoanRequestedAmountMaximumAmountRule : ILoanRequestedAmountValidationRule
    {
        public void Validate(int requestedAmount)
        {

            if (requestedAmount > 15000)
                throw new ValidationException("Loan amount should be less than or equal to £15,000.");
        }
    }
}
