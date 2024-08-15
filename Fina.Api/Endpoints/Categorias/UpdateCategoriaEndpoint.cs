using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categoria;
using Fina.Core.Response;

namespace Fina.Api.Endpoints.Categorias
{
    public class UpdateCategoriaEndpoint : IEndpoint
    {
         public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        UpdateCategoryRequest request,
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