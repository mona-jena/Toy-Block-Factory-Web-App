using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace ToyBlockFactoryWebAppTests
{
    public class HttpRequestTests
    {
        private readonly ToyBlockOrdersFixture _toyBlockOrdersFixture;

        public HttpRequestTests()
        {
            _toyBlockOrdersFixture = new ToyBlockOrdersFixture();
        }


        [Fact]
        public async Task AppIsAbleToBeDeployed()       //TODO:DEPLOYED -> too technical?
        { 
            HttpResponseMessage response = await _toyBlockOrdersFixture.Client.GetAsync("http://localhost:3000/health");
            response.EnsureSuccessStatusCode();
            var statusCode = response.StatusCode;
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("{\"status\":\"ok\"}", responseBody); //TODO:don't test literal string?!!
        }

        [Fact]
        public async Task CanSendSubmitOrder_AndGetOrderId()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            
            var body = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request).Result;
            body.EnsureSuccessStatusCode();
            var statusCode = body.StatusCode;
            string responseBody = await body.Content.ReadFromJsonAsync<string>();
            _toyBlockOrdersFixture.Dispose();
            
            Assert.Equal(HttpStatusCode.Accepted, statusCode);
            Assert.Equal("0001", responseBody); 
        }
        
        [Fact]
        public async Task CanAddBlocksToOrder()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderRequest = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request).Result;
            
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var body = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/addblock?orderId=0001", blockOrderRequest).Result;
            body.EnsureSuccessStatusCode();     //TODO:DO I NEED THIS??
            var statusCode = body.StatusCode;
            _toyBlockOrdersFixture.Dispose();
            //TODO:SHOULD I BE RETURNING SOMETHING? Or is this enough to show it worked?
                                                
            Assert.Equal(HttpStatusCode.Accepted, statusCode);
        }

        [Fact]
        public async Task CanDeleteOrder()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderRequest = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request).Result;
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var body = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/addblock?orderId=0001", blockOrderRequest).Result;
           
            var deletedOrder = _toyBlockOrdersFixture.Client.DeleteAsync("http://localhost:3000/order?orderId=0001").Result;
            body.EnsureSuccessStatusCode();     //TODO:DO I NEED THIS??
            var statusCode = deletedOrder.StatusCode;
            _toyBlockOrdersFixture.Dispose();
            
            Assert.Equal(HttpStatusCode.Accepted, statusCode);
        }

        [Fact]
        public async Task CanSubmitOrder()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderRequest = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request).Result;
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var body = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/addblock?orderId=0001", blockOrderRequest).Result;

            var putContent = new StringContent(await JsonConvert.SerializeObjectAsync(body), Encoding.UTF8, "application/json");
            var submittedOrder = _toyBlockOrdersFixture.Client.PutAsync("http://localhost:3000/order?orderid=0001", putContent).Result;
            var statusCode = submittedOrder.StatusCode;
            _toyBlockOrdersFixture.Dispose();
            
            Assert.Equal(HttpStatusCode.Accepted, statusCode);
        }

        [Fact]
        public async Task CanGetReport()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderRequest = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request).Result;
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var body = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/addblock?orderId=0001", blockOrderRequest).Result;
        
            HttpResponseMessage order = await _toyBlockOrdersFixture.Client.GetAsync("http://localhost:3000/report?orderid=0001&ReportType=Invoice");
            order.EnsureSuccessStatusCode();
            var statusCode = order.StatusCode;
            string responseBody = await order.Content.ReadAsStringAsync();
            _toyBlockOrdersFixture.Dispose();
        
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