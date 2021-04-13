using System.Net;
using ToyBlockFactoryWebApp.Controllers;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.RequestHandlers
{
    public class OrdersGetHandler : IRequestHandler
    {
        private readonly OrderController _orderController;

        public OrdersGetHandler(OrderController orderController)
        {
            _orderController = orderController;
        }
        
        public bool ShouldHandle(string url, string httpMethod)
        {
            return url == "/orders" && httpMethod == "GET";
        }

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            var orders = _orderController.GetAllOrders();
            if (orders == null)
            {
                return new BadRequestResponse();
            }
            return new AcceptedResponse(orders);
        }

    }
    
}