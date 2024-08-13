using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categoria
{
    public class DeleteCategoryRequest : Request
    {
        [Required(ErrorMessage = "Id Inválido")]
        public long Id { get; set; }
    }
}