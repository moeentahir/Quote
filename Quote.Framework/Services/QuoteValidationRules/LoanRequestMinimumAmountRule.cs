using Quote.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanRequestMinimumAmountRule : ILoanRequestValidationRule
    {
        public void Validate(int requestedAmount) {

            if (requestedAmount < 1000)
                throw new ValidationException("Loan amound should be greater than or equal to £1,000.");
        }
    }
}
