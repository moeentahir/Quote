using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LoanQuoteProcessor
    {
        public ILoanRequestValidator Validator { get; }
        public LoanRequest Request { get; }

        public LoanQuoteProcessor(ILoanRequestValidator validator, LoanRequest request)
        {
            Validator = validator;
            Request = request;
        }

        public void GetQuote()
        {
            Validator.Validate(Request);
        }

    }
}
