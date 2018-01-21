using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public interface ILoanRequestValidator
    {
        List<ILoanRequestValidationRule> Rules { get; set; }

        void Validate(int requestedAmount);
    }
}
