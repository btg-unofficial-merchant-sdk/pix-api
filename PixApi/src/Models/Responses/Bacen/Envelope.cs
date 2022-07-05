namespace PixApi.Models.Responses.Bacen
{
    public class Envelope<T>
    {
        public T Body { get; set; }
        public Errors Errors { get; set; }

        public Envelope(T body)
        {
            Body = body;
        }

        public Envelope(T body, Errors errors)
        {
            Body = body;
            Errors = errors;
        }

        public Envelope(Errors errors)
        {
            Errors = errors;
        }
    }
}
