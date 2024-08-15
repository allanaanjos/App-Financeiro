namespace Fina.Api.Common.Api
{
    public static class AppExtencion
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            // app.MapSwagger().RequireAuthorization();
        }
    }
}