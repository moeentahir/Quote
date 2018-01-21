using Quote.Common;
using System;
using Quote.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            try
            {
                // Step 1: Validate and build command line arguments
                var request = new LoanRequestBuilder(args).Build();

                // Step 2: Calculate interest
                var quote = await new LoanQuoteProcessor(
                    new LoanRequestedAmountValidator(),
                    new CsvLenderRateDeserializer(new LenderRawRateProviderFromFile(request.FilePath)),
                    new CompoundInterestCalculator(),
                    request.LoanAmount)
                    .Process();

                // Step 3: Display results
                DisplayQuote(quote);
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while processing your request. Error: {ex.Message}");
            }
        }

        private static void DisplayQuote(LoanQuote quote)
        {
            Console.WriteLine($"Requested amount: {quote.LoanRequested.ToString("c")}");
            Console.WriteLine($"Rate: {quote.Rate.DisplayPercentage()}");
            Console.WriteLine($"Monthly repayment: {quote.MonthlyRepayment.ToString("c")}");
            Console.WriteLine($"Total repayment:  {quote.Total.ToString("c")}");
        }
    }
}
