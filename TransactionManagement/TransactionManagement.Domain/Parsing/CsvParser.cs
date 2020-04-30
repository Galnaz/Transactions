using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace TransactionManagement.Domain
{
    public class CsvParser : AbstractParser, IParser
    {
        protected override ParseTransactionResult TryParse(Stream parsingContent)
        {
            using (TextReader reader = new StreamReader(parsingContent))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.IgnoreBlankLines = true;
                var records = csv.GetRecords<CsvParseRecords>().ToList();

                var transactions = records.Select(ToTransaction).ToList();
                
                return ParseTransactionResult.Success(transactions);
            }
        }

        private Transaction ToTransaction(CsvParseRecords parsed)
        {
            return Transaction.Create(
                parsed.TransactionId,
                parsed.Amount,
                parsed.Currency,
                DateTime.ParseExact(parsed.Date.Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                parsed.Status.Replace('\0', ' ').Trim().ToTransactionStatus());
        }
    }
}
