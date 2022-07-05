namespace PixApi.Models.Requests.Bacen
{
    public class CreateReversal
    {
        /// <summary>
        /// Identifier of received payment
        /// </summary>
        public string endToEndId { get; set; }

        /// <summary>
        /// Idempotency key of client (client request id)
        /// </summary>
        public string clientRequestId { get; set; }

        /// <summary>
        /// Amount to refund
        /// </summary>
        public string valor { get; set; }
    }
}
