using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToyBlockFactoryWebApp.DTOs;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.Handlers
{
    public class OrderAddBlocksHandler : IRequestHandler
    {
        private readonly OrderController _orderController;

        public OrderAddBlocksHandler(OrderController orderController)
        {
            _orderController = orderController;
        }
        
        public bool ShouldHandle(string url, string httpMethod)
        {
            return url.StartsWith("/order") && url.EndsWith("/addblock") && httpMethod == "POST";
        }

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            var orderId = request.Url?.AbsolutePath.Replace("/order/", "").Replace("/addblock", "");
            var options = new JsonSerializerOptions
            {
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
            };
            var orderDetails = JsonSerializer.Deserialize<BlockOrderDTO>(request.GetRequestBody(), options) 
                               ?? throw new InvalidDataException("Invalid block order data");
            var blockOrder = _orderController.PostAddBlock(orderId, orderDetails);
            if (blockOrder == null)
            {
                return new BadRequestResponse();
            }
            return new AcceptedResponse(blockOrder);
        }
        
       
    }
}