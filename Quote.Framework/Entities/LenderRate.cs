namespace Quote.Framework
{
    public class LenderRate
    {
        public string Lender { get; set; }
        public decimal Rate { get; set; }
        public decimal Available { get; set; }

        public override string ToString()
        {
            return $"{Lender} | {Available}({Rate})";
        }
    }
}