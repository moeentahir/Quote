namespace Quote.Framework
{
    public class InterestCalculationParameters
    {
        /// <summary>
        /// The principal investment amount(the initial deposit or loan amount)
        /// </summary>
        public int PrincipalAmount { get; set; }

        /// <summary>
        /// the annual interest rate(decimal)
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// the number of times that interest is compounded per year
        /// </summary>
        public int CompoundsPerYear { get; set; }

        /// <summary>
        /// the number of years the money is invested or borrowed for
        /// </summary>
        public int YearsBorrowed { get; set; }
    }
}