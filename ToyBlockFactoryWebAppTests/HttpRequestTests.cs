using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
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
        public async Task AppIsAbleToBeDeployed()
        {
            HttpResponseMessage response = await _toyBlockOrdersFixture.Client.GetAsync("http://localhost:3000/health");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal("{\"status\": \"ok\"}", responseBody); //FIX ACTUAL!!
        }

        [Fact]
        public async Task CanSendSubmitOrder_AndGetBackOrderId()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            
            var body = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request).Result;
            body.EnsureSuccessStatusCode();
            var statusCode = body.StatusCode;
            string responseBody = await body.Content.ReadFromJsonAsync<string>();
            _toyBlockOrdersFixture.Dispose();
            
            Assert.Equal("Accepted", statusCode.ToString()); 
            Assert.Equal("0001", responseBody); 
        }
        
        [Fact]
        public async Task CanAddBlocksToOrder_AndGetBackOrderDetails()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderRequest = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request).Result;
            
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var body = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/addblock?orderId=0001", blockOrderRequest).Result;
            var statusCode = body.StatusCode;
            body.EnsureSuccessStatusCode();     //DO I NEED THIS??
            _toyBlockOrdersFixture.Dispose();
                                                //SHOULD I BE RETURNING SOMETHING? Or is this enough to show it worked?
                                                
            Assert.Equal("Accepted", statusCode.ToString());
        }

        [Fact]
        public async Task CanDeleteOrder()
        {
            var request = _toyBlockOrdersFixture.CreateOrderRequest();
            var orderRequest = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/order", request).Result;
            var blockOrderRequest = _toyBlockOrdersFixture.AddBlocks();
            var body = _toyBlockOrdersFixture.Client.PostAsync("http://localhost:3000/addblock?orderId=0001", blockOrderRequest).Result;

            var deleteOrder = _toyBlockOrdersFixture.Client.DeleteAsync("http://localhost:3000/order?orderId=0001");
            var statusCode = body.StatusCode;
            body.EnsureSuccessStatusCode();     //DO I NEED THIS??
            _toyBlockOrdersFixture.Dispose();
            
            Assert.Equal("Accepted", statusCode.ToString());
        }
    }
}