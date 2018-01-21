using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Quote.Framework
{
    public class CsvLenderRateDeserializer: ILenderRateDeserializer
    {
        public ILenderRawRateProvider Reader { get; }

        public CsvLenderRateDeserializer(ILenderRawRateProvider reader)
        {
            Reader = reader;
        }

        async public Task<IEnumerable<LenderRate>> Deserialize()
        {
            var lenderData = await Reader.Read();

            using (var reader = new StringReader(lenderData))
            {
                try
                {
                    var result = new List<LenderRate>();
                    var csv = new CsvReader(reader);
                    csv.Read();
                    csv.ReadHeader();
                    while (await csv.ReadAsync())
                    {
                        result.Add(csv.GetRecord<LenderRate>());
                    }

                    if (result.Count == 0)
                        throw new Quote.Common.ValidationException("Cannot find any lender in the file.");

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Quote.Common.ValidationException(ex.Message);
                }
            }
        }
    }
}