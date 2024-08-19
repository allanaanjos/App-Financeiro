using System.Net.Http.Json;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categoria;
using Fina.Core.Response;

namespace Fina.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClient) : ICategoryHandler
    {
        private readonly HttpClient http = httpClient.CreateClient(WebConfiguration.HttpClientName);
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await http.PostAsJsonAsync("v1/categories", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
             ?? new Response<Category?>(null, 400, "Falha ao criar categorias");
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await http.DeleteAsync($"v1/categories/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
             ?? new Response<Category?>(null, 400, "Falha ao deletar categ");
        }
        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
         => await http.GetFromJsonAsync<PagedResponse<List<Category>?>>("v1/categories")
           ?? new PagedResponse<List<Category>?>(null, 400, "Não foi possível obter as categorias");

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
           => await http.GetFromJsonAsync<Response<Category?>>($"v1/categories/{request.Id}")
            ?? new Response<Category?>(null, 400, "Não foi possivel obter a categoria");


        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await http.PutAsJsonAsync($"v1/categories/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
             ?? new Response<Category?>(null, 400, "Falha ao Atualizar categoria");
        }
    }
}