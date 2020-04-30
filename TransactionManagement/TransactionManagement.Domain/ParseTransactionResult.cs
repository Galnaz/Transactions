using System.Collections.Generic;

namespace TransactionManagement.Domain
{
    public class ParseTransactionResult
    {
        private ParseTransactionResult(List<Transaction> results, bool isSuccess, string message)
        {
            Results = results;
            IsSuccess = isSuccess;
            Message = message;
        }
        public static ParseTransactionResult Success(List<Transaction> results)
            => new ParseTransactionResult(results, true, string.Empty);
        public static ParseTransactionResult Error(string message = "error during parsing") 
            => new ParseTransactionResult(null, false, message);
        
        public bool IsSuccess { get; private set; }

        public string Message { get; private set; }

        public List<Transaction> Results { get; private set; }
    }
}
