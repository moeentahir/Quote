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
        public bool IsValid(decimal requestedAmount) => requestedAmount >= 1000;
    }
}
