using System.Net;
using ToyBlockFactoryWebApp.Controllers;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.RequestHandlers
{
    public class OrderCreateHandler : IRequestHandler
    {
        private readonly OrderController _orderController;

        public OrderCreateHandler(OrderController orderController)
        {
            _orderController = orderController;
        }
        
        public bool ShouldHandle(string url, string httpMethod)
        {
            return url == "/order" && httpMethod == "POST";
        }

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            var orderId = _orderController.Post(request.GetRequestBody());
            if (orderId == null)
            {
                return new BadRequestResponse();
            }
            return new AcceptedResponse(orderId);
        }

    }
}