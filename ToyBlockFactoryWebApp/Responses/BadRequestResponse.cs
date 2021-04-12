using System.Net;

namespace ToyBlockFactoryWebApp.Responses
{
    public class BadRequestResponse : IResponseHandler
    {
        public void Respond(HttpListenerResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.BadRequest;
        }
    }
}