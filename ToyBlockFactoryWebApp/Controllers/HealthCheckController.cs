using ToyBlockFactoryWebApp.DTOs;

namespace ToyBlockFactoryWebApp.Controllers
{
    public class HealthCheckController
    {
        public HealthDTO HealthCheck()
        {
            return new ("ok");
        }
        
    }
}