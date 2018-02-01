using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanRequestedAmountValidator : ILoanRequestedAmountValidator
    {
        public List<ILoanRequestedAmountValidationRule> Rules { get ; set; }

        public LoanRequestedAmountValidator()
        {
            Rules = new List<ILoanRequestedAmountValidationRule>
            {
                new LoanRequestedAmountMinimumAmountRule(),
                new LoanRequestedAmountMaximumAmountRule(),
                new LoanRequestedAmountMultipleOfHundredRule()
            };
        }

        public void Validate(int requestedAmount)
        {
            foreach (var rule in Rules)
            {
                rule.Validate(requestedAmount);
            }
        }
    }
}
