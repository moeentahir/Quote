using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class OperationResult<T>
    {
        public bool IsSuccessfull { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }

        public T Data { get; set; }
    }
}
