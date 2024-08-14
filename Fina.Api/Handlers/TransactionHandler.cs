using Fina.Api.Data;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transaction;
using Fina.Core.Response;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}