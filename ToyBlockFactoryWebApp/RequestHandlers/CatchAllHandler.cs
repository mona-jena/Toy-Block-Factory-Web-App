using System.Net;
using ToyBlockFactoryWebApp.Controllers;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.RequestHandlers
{
    public class CatchAllHandler : IRequestHandler
    {
        public bool ShouldHandle(string url, string httpMethod)
        {
            return true;
        }

        public IResponseHandler Handle(HttpListenerRequest request)
        {
            return new NotFoundResponse();
        }
        
    }
    
}