using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Application.Transactions;
using TransactionManagement.InfrastructureAbstractions.Repositories;
using TransactionManagement.Shared;

namespace TransactionManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsService _transactionsService;

        public TransactionsController(TransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpGet]
        public async Task<SearchTransactionQueryResponse> Get()
        {
            SearchTransactionQueryRequest request = new SearchTransactionQueryRequest();
            var transactions = await _transactionsService.Search(new SearchTransactionQuery
            {
                Currency = request.Currency,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd,
                Status = request.Status
            });
            return new SearchTransactionQueryResponse
            {
                Transactions = transactions
            };
        }
    }
}
