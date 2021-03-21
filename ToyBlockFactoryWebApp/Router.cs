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
                _orderController.Post(context); //BAD TO GIVE IT THE WHOLE CONTEXT???
            }
            else if (request.Url.AbsolutePath == "/order" && request.HttpMethod == "GET")
            {
                _orderController.Get(context); //BAD TO GIVE IT THE WHOLE CONTEXT???
            }
            
        }
    }
}