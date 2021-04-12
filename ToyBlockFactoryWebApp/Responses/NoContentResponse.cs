using System.Net;

namespace ToyBlockFactoryWebApp.Responses
{
    public class NoContentResponse : IResponseHandler
    {
        public void Respond(HttpListenerResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.NoContent;
        }
        
    }
}