using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryWebApp;
using Xunit.Sdk;

namespace ToyBlockFactoryWebAppTests
{
    public class ToyBlockOrdersFixture : IDisposable
    {
        private readonly Router _router;    
        public readonly HttpClient Client = new();
        public ToyServer _toyServer;
        private ToyBlockFactory _toyBlockFactory;

        public ToyBlockOrdersFixture()
        {
            string[] prefixes = {"http://+:3000/"};
            _toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());
            _toyServer = new ToyServer(prefixes, _toyBlockFactory);
            _toyServer.Start();
        }

        public ByteArrayContent CreateOrderRequest()
        {
            var requestBody = 
                "{\"Name\": \"Mona\"," +
                "\"Address\": \"30 Symonds Rd\" }";
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

        public void Dispose()
        {
         
        }
        
    }
}
