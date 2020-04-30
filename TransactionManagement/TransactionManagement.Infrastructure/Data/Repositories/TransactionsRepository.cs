using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TransactionManagement.Infrastructure.Data.Entities;
using TransactionManagement.InfrastructureAbstractions.Repositories;

namespace TransactionManagement.Infrastructure.Data.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private TransactionManagementContext _context;

        public TransactionsRepository(TransactionManagementContext context)
        {
            _context = context;
        }

        public async Task Create(CreateTransactionCommand createTransaction)
        {
            var entity = new TransactionEntity
            {
                Id = createTransaction.Id,
                Amount = createTransaction.Amount,
                Currency = createTransaction.Currency,
                Date = createTransaction.Date,
                Status = createTransaction.Status,
                TransactionId = createTransaction.TransactionId
            };

            await _context.Transactions.AddAsync(entity);
        }

        public async Task AddRange(CreateTransactionCommand[] commands)
        {
            var entities = commands.Select(ToEntity);
            await _context.Transactions.AddRangeAsync(entities);
        }

        public async Task<Domain.Transaction[]> Search(SearchTransactionQuery searchOptions)
        {
            var querybleEntities = _context.Transactions.AsQueryable<TransactionEntity>();
            if (!string.IsNullOrEmpty(searchOptions.Currency))
                querybleEntities = querybleEntities.Where(x => x.Currency == searchOptions.Currency);
            if (searchOptions.DateEnd.HasValue)
                querybleEntities = querybleEntities.Where(x => x.Date <= searchOptions.DateEnd.Value);
            if (searchOptions.DateStart.HasValue)
                querybleEntities = querybleEntities.Where(x => x.Date >= searchOptions.DateStart.Value);
            if (searchOptions.Status.HasValue)
                querybleEntities = querybleEntities.Where(x => x.Status >= searchOptions.Status.Value);

            var entities = await querybleEntities.ToListAsync();
            return entities.Select(ToDomainModel).ToArray();
        }
        private TransactionEntity ToEntity(CreateTransactionCommand createTransaction)
        {
            return new TransactionEntity
            {
                Id = createTransaction.Id,
                Amount = createTransaction.Amount,
                Currency = createTransaction.Currency,
                Date = createTransaction.Date,
                Status = createTransaction.Status,
                TransactionId = createTransaction.TransactionId
            };
        }

        private Domain.Transaction ToDomainModel (TransactionEntity entity)
        {
            return Domain.Transaction.Restore(
                entity.Id,
                entity.TransactionId,
                entity.Amount,
                entity.Currency,
                entity.Date,
                entity.Status);
        }
    }
}
