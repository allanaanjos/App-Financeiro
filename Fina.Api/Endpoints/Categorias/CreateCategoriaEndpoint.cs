using Fina.Api.Common.Api;
using Fina.Api.Handlers;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categoria;
using Fina.Core.Response;

namespace Fina.Api.Endpoints.Categorias
{
    public class CreateCategoriaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapPost("/", HandlerAsync)
           .WithName("")
           .Produces<Response<Category>>();

        private static async Task<IResult> HandlerAsync(
            CreateCategoryRequest request,
            ICategoryHandler handler
        )
        {
            request.UserId = ApiConfigurtion.UserId;
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                  ? TypedResults.Created($"v1/categorias/{response.Data?.Id}", response)
                  : TypedResults.BadRequest();


        }
    }
}