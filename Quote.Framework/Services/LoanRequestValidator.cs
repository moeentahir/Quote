using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanRequestValidator : ILoanRequestValidator
    {
        public List<ILoanRequestValidationRule> Rules { get ; set; }

        public LoanRequestValidator()
        {
            Rules = new List<ILoanRequestValidationRule>
            {
                new LoanRequestMinimumAmountRule(),
                new LoanRequestMaximumAmountRule(),
                new LoanRequestMultipleOfHundredRule()
            };
        }

        public bool Validate(decimal requestedAmount) => Rules.All(r => r.IsValid(requestedAmount));
    }
}
