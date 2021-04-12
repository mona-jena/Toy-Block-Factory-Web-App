using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class GetReportHandler : IRequestHandler
    {
        private readonly OrderController _orderController;

        public GetReportHandler(OrderController orderController)
        {
            _orderController = orderController;
        }
        
        public bool ShouldHandle(string url, string httpMethod)
        {
            return url == "/report" && httpMethod == "GET";
        }

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            var report = _orderController.GetReport(request.QueryString);
            if (report == null)
            {
                return new BadRequestResponse();
            }
            return new AcceptedResponse(report);
        }
        
       
    }
    
}