using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryWebApp;
using Xunit;

namespace ToyBlockFactoryWebAppTests
{
    public class HttpRequestTests
    {
        private readonly ToyBlockOrdersFixture _toyBlockOrdersFixture = new ToyBlockOrdersFixture();

        public HttpRequestTests()
        {
            /*string[] prefixes = {"http://*:3000/"};
            var toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());
            var toyServer = new ToyServer(prefixes, toyBlockFactory);
            toyServer.Start();*/
            //_toyBlockOrdersFixture = new ToyBlockOrdersFixture();
        }


        [Fact]
        public async Task AppIsAbleToBeDeployed()       //TODO: "DEPLOYED" -> too technical?
        { 
            HttpResponseMessage response = await _toyBlockOrdersFixture.Client.GetAsync("http://localhost:3000/health");
            var statusCode = response.StatusCode;
            string responseBody = await response.Content.ReadAsStringAsync();
            
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("{\"status\":\"ok\"}", responseBody); //TODO:don't test literal string?!!
        }

        [Fact]
        public async Task CanCreateOrder_AndGetOrderId()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            
            var requestBody = await _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request);
            var statusCode = requestBody.StatusCode;
            string responseBody = await requestBody.Content.ReadFromJsonAsync<string>();

            Assert.Equal(HttpStatusCode.Accepted, statusCode);
            Assert.NotNull(responseBody);
            Assert.Matches("[0-9]{4}", responseBody);  //we don't care whats the orderID, only that an ID is returned
        }
        
        [Fact]
        public async Task CanAddBlocksToOrder()
        {
            string[] prefixes = {"http://*:3000/"};
            var toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());
            var toyServer = new ToyServer(prefixes, toyBlockFactory);
            toyServer.Start();
            
            var orderRequest = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderResponse = await _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", orderRequest);
            var orderNumber = orderResponse.Content.ReadFromJsonAsync<string>();
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            
            var requestBody = await _toyBlockOrdersFixture.Client.PostAsync($"http://localhost:3000/addblock?orderId={orderNumber}", blockOrderRequest);
            var statusCode = requestBody.StatusCode;
            //TODO:SHOULD I BE RETURNING SOMETHING? Or is this enough to show it worked?
                                                
            Assert.Equal(HttpStatusCode.Accepted, statusCode);
        }

        [Fact]
        public async Task CanDeleteOrder()
        {
            var orderRequest = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderResponse = await _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", orderRequest);
            var orderNumber = orderResponse.Content.ReadFromJsonAsync<string>();
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var blockOrderRequestBody = _toyBlockOrdersFixture.Client.PostAsync($"http://localhost:3000/addblock?orderId={orderNumber}", blockOrderRequest);
           
            var deletedOrder = await _toyBlockOrdersFixture.Client.DeleteAsync($"http://localhost:3000/order?orderId={orderNumber}");
            var statusCode = deletedOrder.StatusCode;

            Assert.Equal(HttpStatusCode.Accepted, statusCode);
        }

        [Fact]
        public async Task CanSubmitOrder()
        {
            var orderRequest = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderResponse = await _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", orderRequest);
            var orderNumber = orderResponse.Content.ReadFromJsonAsync<string>();
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var body = _toyBlockOrdersFixture.Client.PostAsync($"http://localhost:3000/addblock?orderId={orderNumber}", blockOrderRequest);

            var putContent = new StringContent(await JsonConvert.SerializeObjectAsync(body), Encoding.UTF8, "application/json");
            var submittedOrder = await _toyBlockOrdersFixture.Client.PutAsync($"http://localhost:3000/order?orderid={orderNumber}", putContent);
            var statusCode = submittedOrder.StatusCode;
            
            Assert.Equal(HttpStatusCode.Accepted, statusCode);
        }

        [Fact]
        public async Task CanGetReport()
        {
            var orderRequest = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderResponse = await _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", orderRequest);
            var orderNumber = orderResponse.Content.ReadFromJsonAsync<string>();
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var body = _toyBlockOrdersFixture.Client.PostAsync($"http://localhost:3000/addblock?orderId={orderNumber}", blockOrderRequest);
        
            HttpResponseMessage order = await _toyBlockOrdersFixture.Client.GetAsync($"http://localhost:3000/report?orderid={orderNumber}&ReportType=Invoice");
            var statusCode = order.StatusCode;
            string responseBody = await order.Content.ReadAsStringAsync();
        
            Assert.Equal(HttpStatusCode.Accepted, statusCode);
            Assert.Equal(InvoiceReport(), responseBody);
        }
    
        private string InvoiceReport(){
            return 
            "{"+
                "\"LineItems\":["+
                    "{"+
                        "\"Description\":\"Square\","+
                        "\"Quantity\":2,"+
                        "\"Price\":1,"+
                        "\"Total\":2"+
                    "},"+
                    "{"+
                        "\"Description\":\"Triangle\","+
                        "\"Quantity\":5,"+
                        "\"Price\":2,"+
                        "\"Total\":10"+
                    "},"+
                    "{"+
                        "\"Description\":\"Red colour surcharge\","+
                        "\"Quantity\":2,"+
                        "\"Price\":1," +
                        "\"Total\":2"+
                    "}"+
                "],"+
                "\"Total\":14,"+
                "\"ReportType\":\"invoice\","+
                "\"Name\":\"Mona\","+
                "\"Address\":\"30 Symonds Rd\","+
                "\"DueDate\":\"2021-04-05T00:00:00+12:00\","+
                "\"OrderId\":\"0001\","+
                "\"OrderTable\":["+
                    "{"+
                        "\"Shape\":\"square\","+
                        "\"TableColumn\":["+
                            "{"+
                                "\"MeasuredItem\":\"Red\","+
                                "\"Quantity\":2"+
                            "},"+
                            "{"+
                                "\"MeasuredItem\":\"Yellow\","+
                                "\"Quantity\":0"+
                            "}"+
                        "]"+
                    "},"+
                    "{"+
                        "\"Shape\":\"triangle\","+
                        "\"TableColumn\":["+
                            "{"+
                                "\"MeasuredItem\":\"Red\","+
                                "\"Quantity\":0"+
                            "},"+
                            "{"+
                                "\"MeasuredItem\":\"Yellow\","+
                                "\"Quantity\":5"+
                            "}"+
                        "]"+
                    "}"+
                "]"+
            "}";
        }
        
    }
}