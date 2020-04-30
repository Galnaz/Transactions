using System;
using TransactionManagement.Shared;

namespace TransactionManagement.Domain
{
    public class Transaction
    {
        private Transaction(
            Guid id,
            string transactionId,
            decimal amount,
            string currency,
            DateTime date,
            TransactionStatus status)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new TransactionManagementException($"{nameof(transactionId)} is required");
            if (string.IsNullOrEmpty(currency))
                throw new TransactionManagementException($"{nameof(currency)} is required");
            if(date == default(DateTime))
                throw new TransactionManagementException($"{nameof(date)} is required");
            Id = id;
            TransactionId = transactionId;
            Amount = amount;
            Currency = currency;
            Date = date;
            Status = status;
        }

        public static Transaction Create(
            string transactionId,
            decimal amount,
            string currency,
            DateTime date,
            TransactionStatus status)
        {
            return new Transaction(Guid.NewGuid(), transactionId, amount, currency, date, status);
        }
        public static Transaction Restore(
            Guid id,
            string transactionId,
            decimal amount,
            string currency,
            DateTime date,
            TransactionStatus status)
        {
            return new Transaction(id, transactionId, amount, currency, date, status);
        }
        public Guid Id { get; set; }

        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime Date { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
