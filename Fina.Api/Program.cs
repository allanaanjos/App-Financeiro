using Fina.Api;
using Fina.Api.Common.Api;
using Fina.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
      app.ConfigureDevEnvironment();

app.UseCors(ApiConfigurtion.CorsPolicyName);
app.MapEndpoints();

app.Run();
