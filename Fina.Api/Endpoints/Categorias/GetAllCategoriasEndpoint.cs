using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categoria;
using Fina.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categorias
{
    public class GetAllCategoriasEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Recupera todas as categorias")
            .WithDescription("Recupera todas as categorias")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();
    
    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = ApiConfigurtion.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    }
}