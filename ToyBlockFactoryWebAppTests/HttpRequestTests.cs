using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
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
            //Have Start() 
        }


        [Fact]
        public async Task AppIsAbleToBeDeployed()
        {
            HttpResponseMessage response = await _client.GetAsync("http://localhost:3000/health");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal("{\"status\": \"ok\"}", responseBody); //FIX ACTUAL!!
        }

        [Fact]
        public async Task CanSendSubmitOrderRequest_AndGetBackOrderId()
        {
            var requestBody = 
                "{\"Name\": \"Mona\"," +
                "\"Address\": \"30 Symonds Rd\" }";
            var buffer = Encoding.UTF8.GetBytes(requestBody);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var body = _client.PostAsync("http://localhost:3000/order", byteContent).Result;
            var statusCode = body.StatusCode;
            body.EnsureSuccessStatusCode();     //DO I NEED THIS??
            string responseBody = await body.Content.ReadFromJsonAsync<string>();
            
            Assert.Equal("Accepted", statusCode.ToString()); 
            Assert.Equal("0001", responseBody); 
        }
        
        [Fact]
        public async Task CanAddBlocksToOrder_AndGetBackOrderDetails()
        {
            //NEED TO ADD ORDER- /order  FIRST
            var requestBody =
                "{" +
                    "\"Order\":[" +
                        "{" +
                            "\"Colour\":\"Red\"," +
                            "\"Shape\":\"Square\"," +
                            "\"Quantity\":2" +
                        "}," +
                        "{" +
                            "\"Colour\":\"Yellow\"," +
                            "\"Shape\":\"Triangle\"," +
                            "\"Quantity\":5" +
                        "}" +
                    "]" +
                "}";
            var buffer = Encoding.UTF8.GetBytes(requestBody);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var body = _client.PostAsync("http://localhost:3000/addblock?orderId=0001", byteContent).Result;
            var statusCode = body.StatusCode;
            body.EnsureSuccessStatusCode();     //DO I NEED THIS??
            string responseBody = await body.Content.ReadFromJsonAsync<string>();
            
            Assert.Equal("Accepted", statusCode.ToString()); 
            Assert.Equal("0001", responseBody); 
        }
    }
}