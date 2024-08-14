using Fina.Api.Data;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categoria;
using Fina.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {

        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };

            try
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria Criada Com Sucesso!!!");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possivel criar a sua Categoria");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {

                var category = await context.Categories
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada.");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria Excluida com sucesso.");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possivel excluir a categoria");
            }

        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query =
                 context.Categories
                 .AsNoTracking()
                 .Where(x => x.UserId == request.UserId)
                 .OrderBy(x => x.Title);

                var categorias = await query
                .Skip((request.PageNumber - 1) * request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(categorias, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>?>(null,0);
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var categoria = await context.Categories
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return categoria is null
                 ? new Response<Category?>(null, 404, "Categoria não encontrada")
                 : new Response<Category?>(categoria);
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não possivel encontrar a categoria");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                var data = new Category
                {
                    Title = category.Title,
                    Description = category.Description,
                };

                context.Categories.Update(data);
                await context.SaveChangesAsync();

                return new Response<Category?>(data, message: "Categoria Atualizada com Sucesso!!!");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não possivel Atualizar a Categoria");
            }
        }
    }
}