using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public interface ILenderRateDeserializer
    {
        Task<IEnumerable<LenderRate>> Deserialize();
    }
}
