using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TransactionManagement.Domain.Parsing
{
    public class XmlParser : AbstractParser, IParser
    {
        protected override ParseTransactionResult TryParse(Stream parsingContent)
        {
            var serializer = new XmlSerializer(typeof(XmlParseRecordsResult));
            using (TextReader reader = new StreamReader(parsingContent))
            {
                XmlParseRecordsResult records = (XmlParseRecordsResult)serializer.Deserialize(reader);

                var result = records.Transactions.Select(ToTransaction).ToList();
                return ParseTransactionResult.Success(result);
            }
        }
        
        private Transaction ToTransaction(XmlParseRecord parsed)
        {
            return Transaction.Create(
                parsed.TransactionId,
                parsed.PaymentDetails.Amount,
                parsed.PaymentDetails.Currency,
                parsed.Date,
                parsed.Status.Trim().ToTransactionStatus());
        }
    }
}
