using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categoria;
using Fina.Core.Response;

namespace Fina.Api.Endpoints.Categorias
{
    public class GetByIdCategoriaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapGet("/{id}", HandleAsync)
           .WithName("Categories: Get By Id")
           .WithSummary("Recupera uma categoria")
           .WithDescription("Recupera uma categoria")
           .WithOrder(4)
           .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            long id)
        {
            var request = new GetCategoryByIdRequest
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