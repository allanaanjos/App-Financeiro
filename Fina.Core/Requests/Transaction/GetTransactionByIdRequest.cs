using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transaction
{
    public class GetTransactionByIdRequest : Request
    {
        [Required(ErrorMessage = "ID Inválido")]
        public long Id { get; set; }
    }
}