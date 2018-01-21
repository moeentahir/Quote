using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanRequestMultipleOfHundredRule : ILoanRequestValidationRule
    {
        public bool IsValid(decimal requestedAmount) => (requestedAmount % 100M) == 0;
    }
}
