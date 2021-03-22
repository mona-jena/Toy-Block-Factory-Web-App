using System.Net;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryWebApp
{
    public class Router
    {
        private readonly HealthCheckController _healthCheckController;
        private readonly OrderController _orderController;

        public Router(HealthCheckController healthCheckController, OrderController orderController)
        {
            _healthCheckController = healthCheckController;
            _orderController = orderController;
        }

        public void ReadRequests(HttpListenerRequest request, HttpListenerContext context)
        {
            if (request.RawUrl == "/health" && request.HttpMethod == "GET")
            {
                _healthCheckController.HealthCheck(request, context.Response);
            }
            else if (request.RawUrl == "/order" && request.HttpMethod == "POST")
            {
                _orderController.Post(context); 
            }
            else if (request.Url.AbsolutePath == "/addblock" && request.HttpMethod == "POST")
            {
                _orderController.PostAddBlock(context); 
            }
            else if (request.Url.AbsolutePath == "/report" && request.HttpMethod == "GET")
            {
                _orderController.Get(context); 
            }
            
        }
    }
}