using Fina.Api.Common.Api;
using Fina.Api.Endpoints.Categorias;
using Fina.Api.Endpoints.Transaction;

namespace Fina.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
             .WithTags("Health Check")
             .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .MapEndpoint<CreateCategoriaEndpoint>()
            .MapEndpoint<UpdateCategoriaEndpoint>()
            .MapEndpoint<DeleteCategoriaEndpoint>()
            .MapEndpoint<GetByIdCategoriaEndpoint>()
            .MapEndpoint<GetAllCategoriasEndpoint>();

            endpoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetByIdTransactionEndpoint>()
            .MapEndpoint<GetByPeriodTransactionEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
          where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}