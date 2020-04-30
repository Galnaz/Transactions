using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionManagement.Shared
{
    public class TransactionDto
    {
        public string Id { get; set; }

        public string Payment { get; set; }

        public string Status { get; set; }
    }
}
