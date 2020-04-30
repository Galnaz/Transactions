using System;
using System.IO;
using System.Text;

namespace TransactionManagement.Domain
{
    public interface IParser
    {
        ParseTransactionResult Parse(Stream parsingContent);
    }

    public abstract class AbstractParser : IParser
    {
        public ParseTransactionResult Parse(Stream parsingContent)
        {
            try
            {
                var result = TryParse(parsingContent);
                return result;
            }
            catch (Exception ex)
            {
                return ParseTransactionResult.Error(ex.Message);
            }
        }

        protected abstract ParseTransactionResult TryParse(Stream parsingContent);
    }
}
