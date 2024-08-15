using Fina.Api.Data;
using Fina.Core.Common;
using Fina.Core.Enum;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transaction;
using Fina.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: EtransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceiveAt = request.PaidOrReceiveAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação feita com sucesso!!!");
            }
            catch
            {

                return new Response<Transaction?>(null, 500, "Não possivel fazer a transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {

                var transaction = await context.Transactions
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "transção não encontrada.");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "transação Excluida com sucesso.");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel excluir a transação");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                 ? new Response<Transaction?>(null, 404, "Transação não encontrada")
                 : new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não possivel encontrar a transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500,
                    "Não foi possível determinar a data de início ou término");
            }

            try
            {
                var query = context
                    .Transactions
                    .AsNoTracking()
                    .Where(x =>
                        x.PaidOrReceiveAt >= request.StartDate &&
                        x.PaidOrReceiveAt <= request.EndDate &&
                        x.UserId == request.UserId)
                    .OrderBy(x => x.PaidOrReceiveAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transactions,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {

            if (request is { Type: EtransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {

                var transaction = await context.Transactions
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação nãoencontrada");

                transaction.UserId = request.UserId;
                transaction.CategoryId = request.CategoryId;
                transaction.CreatedAt = DateTime.Now;
                transaction.Amount = request.Amount;
                transaction.PaidOrReceiveAt = request.PaidOrReceiveAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "transação atualizada com sucesso!!!");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel Atualizar a transação");
            }
        }
    }
}