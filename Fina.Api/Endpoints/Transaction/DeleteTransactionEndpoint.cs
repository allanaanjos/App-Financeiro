using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Requests.Transaction;
using Fina.Core.Response;

namespace Fina.Api.Endpoints.Transaction
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapDelete("/{id}", HandleAsync)
           .WithName("Transactions: Delete")
           .WithSummary("Exclui uma transação")
           .WithDescription("Exclui uma transação")
           .WithOrder(3)
           .Produces<Response<Core.Models.Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            long id)
        {
            var request = new DeleteTransactionRequest
            {
                UserId = ApiConfigurtion.UserId,
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}