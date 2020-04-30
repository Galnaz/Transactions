using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionManagement.InfrastructureAbstractions.Repositories;

namespace TransactionManagement.InfrastructureAbstractions.Consistency
{
    public interface IUnitOfWork
    {
        ITransactionsRepository Transactions { get; }

        Task SaveChanges();
    }
}
