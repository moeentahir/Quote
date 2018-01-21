using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Common
{
    /// <summary>
    /// Use this exception if you want to display message to user as it is.
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message):base(message)
        {

        }
    }
}
