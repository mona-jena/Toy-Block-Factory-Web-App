using System.Linq;
using System.Net;
using ToyBlockFactoryWebApp.Controllers;
using ToyBlockFactoryWebApp.RequestHandlers;

namespace ToyBlockFactoryWebApp
{
    public class Router
    {
        private readonly IRequestHandler[] _handlers;

        public Router(OrderController orderController)
        {
            _handlers = new IRequestHandler[] {
                new HealthCheckHandler(), 
                new OrderCreateHandler(orderController), 
                new OrderAddBlocksHandler(orderController), 
                new SingleOrderGetHandler(orderController),
                new OrdersGetHandler(orderController),
                new OrderDeleteHandler(orderController),
                new SubmitOrderHandler(orderController),
                new GetReportHandler(orderController)
            };
        }

        public void ReadRequests(HttpListenerContext context)
        {
            var request = context.Request;
            var handler = _handlers.FirstOrDefault(h => h.ShouldHandle(request.Url?.AbsolutePath, request.HttpMethod));
            if (handler != null)
            {   
                var responseHandler = handler.Handle(request);
                responseHandler.Respond(context.Response);
            }
        }
        
    }
}