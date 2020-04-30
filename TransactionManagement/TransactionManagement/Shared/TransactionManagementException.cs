using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionManagement.Shared
{
    public class TransactionManagementException: Exception
    {
        public TransactionManagementException(string message) : base(message) { }
    }
}
