using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class HealthCheckController
    {
        public HealthDTO HealthCheck()
        {
            return new ("ok");
        }
    }
}