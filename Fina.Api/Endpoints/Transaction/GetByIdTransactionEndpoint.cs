using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Requests.Transaction;
using Fina.Core.Response;

namespace Fina.Api.Endpoints.Transaction
{
    public class GetByIdTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Recupera uma transação")
            .WithDescription("Recupera uma transação")
            .WithOrder(4)
            .Produces<Response<Core.Models.Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = ApiConfigurtion.UserId,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}