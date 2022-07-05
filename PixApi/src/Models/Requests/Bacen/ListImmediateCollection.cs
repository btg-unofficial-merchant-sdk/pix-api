namespace PixApi.Models.Requests.Bacen
{
    public class ListImmediateCollection : ListBase
    {
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public Status? status { get; set; }
        public bool? locationPresente { get; set; }
    }

    public enum Status
    {
        ATIVA, 
        CONCLUIDA,
        REMOVIDA_PELO_USUARIO_RECEBEDOR,
        REMOVIDA_PELO_PSP
    }
}
