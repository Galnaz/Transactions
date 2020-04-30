using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionManagement.Domain;
using TransactionManagement.InfrastructureAbstractions.Adapters;
using TransactionManagement.InfrastructureAbstractions.Consistency;
using TransactionManagement.InfrastructureAbstractions.Repositories;

namespace TransactionManagement.Application.Documents
{
    public class DocumentsService
    {
        private readonly IFileStorageAdapter _fileStorageAdapter;
        private readonly IParserFactory _parserFactory;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentsService(
            IFileStorageAdapter fileStorageAdapter,
            IParserFactory parserFactory, IUnitOfWork unitOfWork)
        {
            _fileStorageAdapter = fileStorageAdapter;
            _parserFactory = parserFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task Upload(Stream fileStream, string name, string fileType)
        {
            await _fileStorageAdapter.Upload(fileStream, name);
            // This is done to have ability later make this process asyncronus 
            var fileContent = await _fileStorageAdapter.DownloadFile(name);
            //var fileString = await _fileStorageAdapter.DownloadAsString(name);
            var parser = _parserFactory.GetParser(fileType);
            var parsingResult = parser.Parse(fileContent);
            if (!parsingResult.IsSuccess)
            {
                throw new Exception();
            }
            if(parsingResult.Results.Count == 0)
            {
                return;
            }

            var transactionsRepository = _unitOfWork.Transactions;
            var createTranscationCommands = parsingResult.Results.Select(ToCommand).ToArray();

            await transactionsRepository.AddRange(createTranscationCommands);
            await _unitOfWork.SaveChanges();
        }

        private CreateTransactionCommand ToCommand(Transaction transaction)
        {
            return new CreateTransactionCommand
            {
                Id = transaction.Id,
                TransactionId = transaction.TransactionId,
                Amount = transaction.Amount,
                Currency = transaction.Currency,
                Date = transaction.Date,
                Status = transaction.Status
            };
        }
    }
}
