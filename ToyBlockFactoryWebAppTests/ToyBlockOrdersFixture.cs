using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryWebApp;

namespace ToyBlockFactoryWebAppTests
{
    public class ToyBlockOrdersFixture
    {
        public readonly HttpClient Client = new();
        private readonly JsonSerializerOptions _options = new()
        {
            Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
        };

        public ToyBlockOrdersFixture()
        {
            string[] prefixes = {"http://+:3000/"};
            var toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());
            var toyServer = new ToyServer(prefixes, toyBlockFactory);
            toyServer.Start();
            Thread.Sleep(100);
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
            const string requestBody =
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
