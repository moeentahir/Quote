using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanRequestValidFileRule : ILoanRequestValidationRule
    {
        public bool IsValid(LoanRequest request) => File.Exists(request.FilePath);
    }
}
