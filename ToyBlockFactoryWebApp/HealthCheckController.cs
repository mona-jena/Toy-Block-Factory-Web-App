using System.Net;
using ToyBlockFactoryWebApp.DTOs;

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