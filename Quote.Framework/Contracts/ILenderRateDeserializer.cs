using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public interface ILenderRateDeserializer
    {
        Task<IEnumerable<LenderRate>> Deserialize();
    }
}
