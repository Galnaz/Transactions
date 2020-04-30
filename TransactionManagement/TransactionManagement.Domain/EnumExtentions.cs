using System;
using System.Collections.Generic;
using System.Text;
using TransactionManagement.Shared;

namespace TransactionManagement.Domain
{
    public static class EnumExtentions
    {
        public static TransactionStatus ToTransactionStatus(this string csvParseStatus)
        {
            switch(csvParseStatus)
            {
                case "Approved":
                    return TransactionStatus.A;
                case "Failed":
                    return TransactionStatus.R;
                case "Finished":
                    return TransactionStatus.D;
                case "Rejected":
                    return TransactionStatus.R;
                case "Done":
                    return TransactionStatus.D;
                default:
                    throw new NotSupportedException();
            };
        }
    }
}
