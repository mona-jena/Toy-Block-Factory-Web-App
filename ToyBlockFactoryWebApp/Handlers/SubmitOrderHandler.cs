using System.Net;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.Handlers
{
    public class SubmitOrderHandler : IRequestHandler
    {
        private readonly OrderController _orderController;

        public SubmitOrderHandler(OrderController orderController)
        {
            _orderController = orderController;
        }
        
        public bool ShouldHandle(string url, string httpMethod)
        {
            return url == "/order" && httpMethod == "PUT";
        }

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            var submittedOrder = _orderController.Put(request.QueryString);
            if (submittedOrder == null)
            {
                return new BadRequestResponse();
            }
            return new AcceptedResponse(submittedOrder);
        }
        
    }
    
}