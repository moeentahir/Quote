using Quote.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanRequestMaximumAmountRule : ILoanRequestValidationRule
    {
        public void Validate(int requestedAmount)
        {

            if (requestedAmount > 15000)
                throw new ValidationException("Loan amound should be less than or equal to £15,000.");
        }
    }
}
