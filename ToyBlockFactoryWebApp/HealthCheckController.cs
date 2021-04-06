using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class HealthCheckController
    {
        public HealthDTO HealthCheck()
        {
            var health = new HealthDTO("ok");
            return health;
        }
    }
}