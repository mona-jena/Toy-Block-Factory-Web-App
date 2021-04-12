using System.Net;
using ToyBlockFactoryWebApp.Controllers;
using ToyBlockFactoryWebApp.RequestHandlers;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.Handlers
{
    public class OrderDeleteHandler : IRequestHandler
    {
        private readonly OrderController _orderController;

        public OrderDeleteHandler(OrderController orderController)
        {
            _orderController = orderController;
        }
        
        public bool ShouldHandle(string url, string httpMethod)
        {
            return url == "/order" && httpMethod == "DELETE";
        }

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            var orderId = _orderController.Delete(request.QueryString);
            if (orderId == false)
            {
                return new BadRequestResponse();
            }

            return new NoContentResponse();
        }
        
    }
}