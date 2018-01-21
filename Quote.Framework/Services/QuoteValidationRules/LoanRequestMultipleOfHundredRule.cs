using Quote.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanRequestMultipleOfHundredRule : ILoanRequestValidationRule
    {
        public void Validate(int requestedAmount)
        {

            if ((requestedAmount % 100M) != 0)
                throw new ValidationException("Loan amound should be multiple of 100.");
        }
    }
}
