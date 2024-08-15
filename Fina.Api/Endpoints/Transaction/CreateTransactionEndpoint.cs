using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Requests.Transaction;
using Fina.Core.Response;

namespace Fina.Api.Endpoints.Transaction
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma nova transação")
            .WithDescription("Cria uma nova transação")
            .WithOrder(1)
            .Produces<Response<Fina.Core.Models.Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            CreateTransactionRequest request)
        {
            request.UserId = ApiConfigurtion.UserId;
            var result = await handler.CreateAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result.Data);
        }
    }
}