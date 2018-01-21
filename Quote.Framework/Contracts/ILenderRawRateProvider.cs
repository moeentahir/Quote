using System.Threading.Tasks;

namespace Quote.Framework
{
    public interface ILenderRawRateProvider
    {
        Task<string> Read();
    }
}