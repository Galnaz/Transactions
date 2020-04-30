using CsvHelper.Configuration.Attributes;
using System;

namespace TransactionManagement.Domain
{
    public class CsvParseRecords
    {

        [Index(0)]
        public string TransactionId { get; set; }

        [Index(1)]
        public decimal Amount { get; set; }

        [Index(2)]
        public string Currency { get; set; }
        
        [Index(3)]
        public string Date { get; set; }

        [Index(4)]
        public string Status { get; set; }
    }

    public enum CsvParseStatus
    {
        Approved,
        Failed,
        Finished
    }
}
