using System;
using System.Collections.Generic;
using System.Text;
using TransactionManagement.Shared;

namespace TransactionManagement.Infrastructure.Data.Entities
{
    public class TransactionEntity
    {
        public Guid Id { get; set; }

        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime Date { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
