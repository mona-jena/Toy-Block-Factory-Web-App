using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class BadRequestResponse : IResponseHandler
    {
        public void Respond(HttpListenerResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.BadRequest;
        }
    }
}