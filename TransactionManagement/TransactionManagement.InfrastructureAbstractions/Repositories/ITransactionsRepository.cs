using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionManagement.Domain;

namespace TransactionManagement.InfrastructureAbstractions.Repositories
{
    public interface ITransactionsRepository
    {
        Task Create(CreateTransactionCommand createTransaction);

        Task AddRange(CreateTransactionCommand[] commands);

        Task<Transaction[]> Search(SearchTransactionQuery searchOptions);
    }
}
