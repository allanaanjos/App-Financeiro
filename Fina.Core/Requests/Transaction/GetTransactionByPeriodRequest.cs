namespace Fina.Core.Requests.Transaction
{
    public class GetTransactionByPeriodRequest : Request
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}