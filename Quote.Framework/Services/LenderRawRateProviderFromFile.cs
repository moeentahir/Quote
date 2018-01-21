using Quote.Common;
using System.IO;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class LenderRawRateProviderFromFile : ILenderRawRateProvider
    {
        public LenderRawRateProviderFromFile(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }

        public async Task<string> Read()
        {
            var result = string.Empty;

            if (!File.Exists(FilePath))
                throw new ValidationException($"The file path '{FilePath}' does not exist");

            using (var reader = File.OpenText(FilePath))
            {
                result = await reader.ReadToEndAsync();
            }

            return result;
        }
    }
}