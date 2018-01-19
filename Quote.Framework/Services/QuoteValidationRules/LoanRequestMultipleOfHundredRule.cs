using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanRequestMultipleOfHundredRule : ILoanRequestValidationRule
    {
        public bool IsValid(LoanRequest request) => (request.LoanAmount % 100M) == 0;
    }
}
