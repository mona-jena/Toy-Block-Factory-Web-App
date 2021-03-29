using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class HealthCheckController
    {
        public string HealthCheck()
        {
            return "{\"status\": \"ok\"}";  //TODO: FIX
        }
    }
}