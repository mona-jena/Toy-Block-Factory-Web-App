using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class NoContentResponse : IResponseHandler
    {
        public void Respond(HttpListenerResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.NoContent;
        }
        
    }
}