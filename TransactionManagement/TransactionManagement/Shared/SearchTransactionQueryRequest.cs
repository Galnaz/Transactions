using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionManagement.Shared
{
    public class SearchTransactionQueryRequest
    {
        public string Currency { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public TransactionStatus? Status { get; set; }
    }

    public class SearchTransactionQueryResponse
    {
        public TransactionDto[] Transactions { get; set; }
    }
}
