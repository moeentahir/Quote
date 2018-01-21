using Quote.Common;

namespace Quote.Framework
{
    public class LoanRequestBuilder
    {
        const int FileArgumentIndex = 0;
        const int LoanArgumentIndex = 1;

        private string[] Args;

        public LoanRequestBuilder(string[] args) => Args = args;

        public LoanRequest Build()
        {
            Validate();

            return new LoanRequest()
            {
                FilePath = Args[FileArgumentIndex],
                LoanAmount = int.Parse(Args[LoanArgumentIndex])
            };
        }

        private void Validate()
        {
            if (Args == null || Args.Length != 2)
                throw new ValidationException("Two arguments are needed to get the quote. First should be lender data CSV file path and second should be Loan Amount.");

            var loanAMount = Args[LoanArgumentIndex].TryParseAs<int>();
            if (loanAMount == null)
                throw new ValidationException("The provided loan amount should be a number.");

        }
    }
}