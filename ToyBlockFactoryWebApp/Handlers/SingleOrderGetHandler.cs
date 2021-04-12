using System.Net;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.Handlers
{
    public class SingleOrderGetHandler : IRequestHandler
    {
        private readonly OrderController _orderController;

        public SingleOrderGetHandler(OrderController orderController)
        {
            _orderController = orderController;
        }
        
        public bool ShouldHandle(string url, string httpMethod)
        {
            return url == "/order" && httpMethod == "GET";
        }

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            var orders = _orderController.GetOrder(request.QueryString);
            if (orders == null)
            {
                return new BadRequestResponse();
            }
            return new AcceptedResponse(orders);
        }

    }
    
}