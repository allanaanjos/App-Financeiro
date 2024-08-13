using System.ComponentModel.DataAnnotations;
using Fina.Core.Enum;

namespace Fina.Core.Requests.Transaction
{
    public class UpdateTransactionRequest : Request
    {
        [Required(ErrorMessage = "ID Inválido")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Titulo inválido")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type inválido")]
        public EtransactionType Type { get; set; } = EtransactionType.Withdraw;

        [Required(ErrorMessage = "Valor inválido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria inválida")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data inválida")]
        public DateTime? PaidOrReceiveAt { get; set; }

    }
}