using System.Text.Json;

namespace BookstoreApplication.Interfaces
{
    public interface IComicVineConnection
    {
        Task<string> Get(string url);
        void HandleUnsuccessfulRequest(HttpResponseMessage response, JsonDocument jsonDocument)
        {
            HandleUnsuccessfulRequest(response, jsonDocument);
        }
    }
}
