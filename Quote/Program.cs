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
            try
            {
                var request = new LoanRequestBuilder(args).Build();



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
    }
}
