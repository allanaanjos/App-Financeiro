using Fina.Core.Models;
using Fina.Core.Requests.Categoria;
using Fina.Core.Response;

namespace Fina.Core.Handler
{
    public interface ICategoryHandler 
    {
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
        Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request);
    }
}