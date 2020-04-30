using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionManagement.Infrastructure.Data.Repositories;
using TransactionManagement.InfrastructureAbstractions.Consistency;
using TransactionManagement.InfrastructureAbstractions.Repositories;

namespace TransactionManagement.Infrastructure.Consistency
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly TransactionManagementContext _context;
        public UnitOfWork(TransactionManagementContext context)
        {
            _context = context;
            _transactions = new Lazy<ITransactionsRepository>(() => new TransactionsRepository(context));
        }

        private Lazy<ITransactionsRepository> _transactions;
        public ITransactionsRepository Transactions => _transactions.Value;

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}
