using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class HealthCheckController
    {
        public record Health(string status)
        {
            
        }
        
        public Health HealthCheck()
        {
            var health = new Health("ok");
            return health;
        }
    }
}