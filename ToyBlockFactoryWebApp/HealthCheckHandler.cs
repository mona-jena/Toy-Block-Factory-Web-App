using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class HealthCheckHandler : IRequestHandler
    {
        public bool ShouldHandle(string url, string httpMethod)
        {
            return url == "/health" && httpMethod == "GET";
        }
        

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            var healthCheckController = new HealthCheckController();
            var healthMessage = healthCheckController.HealthCheck();
            
            return new OkResponse(healthMessage);
        }
    }
}