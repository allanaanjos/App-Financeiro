using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Requests.Transaction;
using Fina.Core.Response;

namespace Fina.Api.Endpoints.Transaction
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
         public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma transação")
            .WithDescription("Atualiza uma transação")
            .WithOrder(2)
            .Produces<Response<Core.Models.Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        UpdateTransactionRequest request,
        long id)
    {
        request.UserId = ApiConfigurtion.UserId;
        request.Id = id;

        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    }
}