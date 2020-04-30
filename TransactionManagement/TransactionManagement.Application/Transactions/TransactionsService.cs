using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionManagement.Domain;
using TransactionManagement.InfrastructureAbstractions.Consistency;
using TransactionManagement.InfrastructureAbstractions.Repositories;
using TransactionManagement.Shared;

namespace TransactionManagement.Application.Transactions
{
    public class TransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionDto[]> Search (SearchTransactionQuery query)
        {
            var transactions = await _unitOfWork.Transactions.Search(query);
            return transactions.Select(ToDto).ToArray();
        }

        private TransactionDto ToDto(Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.TransactionId,
                Payment = $"{transaction.Amount} {transaction.Currency}",
                Status = transaction.Status.ToString()
            };
        }
    }
}
