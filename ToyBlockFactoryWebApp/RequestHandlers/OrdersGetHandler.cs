using System.Net;
using ToyBlockFactoryWebApp.Controllers;
using ToyBlockFactoryWebApp.RequestHandlers;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.Handlers
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