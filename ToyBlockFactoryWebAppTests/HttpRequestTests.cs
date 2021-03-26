using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryWebApp;
using Xunit;

namespace ToyBlockFactoryWebAppTests
{
    public class HttpRequestTests
    {
        private readonly Router _router;
        static readonly HttpClient _client = new();
        private ToyServer _toyServer;

        public HttpRequestTests()
        {
            Task.Run(() =>
            {
                string[] prefixes = {"http://*:3000/"};
                ToyBlockFactory toyBlockFactory = new(new LineItemsCalculator());
                _toyServer = new ToyServer(prefixes, toyBlockFactory);
            });
        }
        
        
        [Fact]
        public async Task HandlesGetRequest()
        {
            HttpResponseMessage response = await _client.GetAsync("http://localhost:3000/health");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        
        }
    }
}