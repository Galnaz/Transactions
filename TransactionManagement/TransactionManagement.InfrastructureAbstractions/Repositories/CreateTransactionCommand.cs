using System;
using TransactionManagement.Shared;

namespace TransactionManagement.InfrastructureAbstractions.Repositories
{
    public class CreateTransactionCommand
    {
        public Guid Id { get; set; }

        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime Date { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
