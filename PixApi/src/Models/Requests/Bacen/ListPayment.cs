namespace PixApi.Models.Requests.Bacen
{
    public class ListPayment : ListBase
    {
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public string txId { get; set; }
        public bool? txIdPresente { get; set; }
        public bool? devolucaoPresente { get; set; }
    }
}
