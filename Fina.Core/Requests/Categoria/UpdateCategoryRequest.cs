using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categoria
{
    public class UpdateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Id Inválido")]
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Title Inválido")]
        [MaxLength(80, ErrorMessage = "O titulo deve conter ate 80 caracteres")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}