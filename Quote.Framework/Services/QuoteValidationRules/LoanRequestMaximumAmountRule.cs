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
        public bool IsValid(LoanRequest request) => request.LoanAmount <= 15000;
    }
}
