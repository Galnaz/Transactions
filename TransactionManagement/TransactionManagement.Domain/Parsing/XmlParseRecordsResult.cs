using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TransactionManagement.Domain.Parsing
{
    [XmlRoot("Transactions")]
    public class XmlParseRecordsResult
    {
        [XmlElement("Transaction")]
        public List<XmlParseRecord> Transactions { get; set; }
    }

    public class XmlParseRecord
    {
        [XmlAttribute("id")]
        public string TransactionId { get; set; }

        [XmlElement("PaymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }

        [XmlElement("TransactionDate")]
        public DateTime Date { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }
    }
    public class PaymentDetails
    {
        [XmlElement("Amount")]
        public decimal Amount { get; set; }

        [XmlElement("CurrencyCode")]
        public string Currency { get; set; }
    }
}
