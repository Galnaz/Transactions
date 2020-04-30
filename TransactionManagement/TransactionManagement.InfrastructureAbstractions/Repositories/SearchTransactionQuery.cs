using System;
using TransactionManagement.Shared;

namespace TransactionManagement.InfrastructureAbstractions.Repositories
{
    public class SearchTransactionQuery
    {
        public string Currency { get; set; }

        public DateTime? DateStart { get; set; }
        
        public DateTime? DateEnd { get; set; }

        public TransactionStatus? Status { get; set; }
    }
}
