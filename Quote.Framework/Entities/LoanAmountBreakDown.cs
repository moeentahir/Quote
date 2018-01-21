namespace Quote.Framework
{
    internal class LoanAmountBreakDown
    {
        public decimal Amount { get; set; }

        public decimal Rate { get; set; }

        public override string ToString()
        {
            return $"{Amount} : {Rate}";
        }
    }
}