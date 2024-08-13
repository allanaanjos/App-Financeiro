using Fina.Core.Models;
using Fina.Core.Requests.Transaction;
using Fina.Core.Response;

namespace Fina.Core.Handler
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
        Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<Response<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request);
    }
}