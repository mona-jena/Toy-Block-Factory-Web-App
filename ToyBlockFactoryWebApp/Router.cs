using System.Net;

namespace ToyBlockFactoryWebApp
{
    class Router
    {
        private HealthCheckController _healthCheckController;

        public Router(HealthCheckController healthCheckController)
        {
            _healthCheckController = healthCheckController;
        }

        public void ReadRequests(HttpListenerRequest request, HttpListenerContext context)
        {
            if (request.RawUrl == "/health" && request.HttpMethod == "GET")
            {
                _healthCheckController.HealthCheck(request, context.Response);
            }
            else if (request.RawUrl == "/order" && request.HttpMethod == "POST")
            {
                OrderController.Post(context);
            }
            
        }
    }
}