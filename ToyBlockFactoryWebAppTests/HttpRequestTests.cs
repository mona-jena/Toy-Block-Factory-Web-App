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
        static readonly HttpClient _client = new HttpClient();

        public HttpRequestTests()
        {
            string[] prefixes = {"http://*:3000/"};
            ToyBlockFactory toyBlockFactory = new (new LineItemsCalculator());
            var toyServer = new ToyServer(prefixes, toyBlockFactory);
            _router = new Router(new HealthCheckController(), new OrderController(toyBlockFactory));
            
        }
        
        
        [Fact]
        public async Task HandlesGetRequest()
        {
            HttpResponseMessage response = await _client.GetAsync("http://*:3000/report");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            
            
        }
    }
}