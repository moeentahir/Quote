using System.Collections.Generic;

namespace Quote.Framework
{
    public interface ILoanRequestedAmountValidator
    {
        List<ILoanRequestedAmountValidationRule> Rules { get; set; }

        void Validate(int requestedAmount);
    }
}
