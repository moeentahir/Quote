using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework.Services
{
    public class LoanRequestValidator : ILoanRequestValidator
    {
        public List<ILoanRequestValidationRule> Rules { get ; set; }

        public LoanRequestValidator()
        {
            Rules = new List<ILoanRequestValidationRule>
            {
                new LoanRequestValidFileRule(),
                new LoanRequestMinimumAmountRule(),
                new LoanRequestMaximumAmountRule(),
                new LoanRequestMultipleOfHundredRule()
            };
        }

        public bool Validate(LoanRequest request) => Rules.All(r => r.IsValid(request));
    }
}
