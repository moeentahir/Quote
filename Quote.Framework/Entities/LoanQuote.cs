namespace Quote.Framework
{
    public class LoanQuote
    {
        public int LoanRequested { get; set; }

        public decimal Rate { get; set; }

        public decimal MonthlyRepayment { get; set; }

        public decimal Total { get; set; }
    }
}