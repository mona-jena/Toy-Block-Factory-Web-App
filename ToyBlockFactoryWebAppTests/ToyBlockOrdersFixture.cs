using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryWebApp;

namespace ToyBlockFactoryWebAppTests
{
    public class ToyBlockOrdersFixture
    {
        private readonly Router _router;    
        public readonly HttpClient Client = new();
        public readonly ToyServer ToyServer;
        private readonly ToyBlockFactory _toyBlockFactory;
        private JsonSerializerOptions _options = new JsonSerializerOptions
        {
            Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
        };

        public ToyBlockOrdersFixture()
        {
            string[] prefixes = {"http://+:3000/"};
            _toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());
            ToyServer = new ToyServer(prefixes, _toyBlockFactory);
            ToyServer.Start();
        }

        public ByteArrayContent CreateOrderRequest()
        {
            var requestBody = JsonSerializer.Serialize(new {Name = "Mona", Address = "30 Symonds Rd", DueDate = DateTime.Today}, _options);
            var buffer = Encoding.UTF8.GetBytes(requestBody);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            return byteContent;
        }

        public ByteArrayContent AddBlocks()
        {
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
            
            return byteContent;
        }

        
        
    }
}
