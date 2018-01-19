using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public interface ILoanRequestValidationRule
    {
        bool IsValid(LoanRequest request);
    }
}
