using System.Net;

namespace ToyBlockFactoryWebApp.Responses
{
    public class NotFoundResponse : IResponseHandler
    {
        public void Respond(HttpListenerResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.NotFound;
        }
        
    }
}