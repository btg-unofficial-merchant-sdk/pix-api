namespace PixApi.Models.Requests
{
    public class Authorization
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public int request_timeout_in_seconds { get; set; } = 300;
        public bool is_production { get; set; } = false;
    }
}
