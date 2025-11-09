namespace BookstoreApplication.Middleware
{
    public class RateLimitException : ApiCommunicationException
    {
        public RateLimitException() : base("Rate limit is reached due to too many requests being sent to the external API.")
        {
        }
    }
}
