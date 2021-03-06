using System.Net;

namespace ToyBlockFactoryWebApp.Responses
{
    public class AcceptedResponse : JSONResponse, IResponseHandler
    {
        public AcceptedResponse(object responseObject) : base(responseObject)
        {
        }
        
        public void Respond(HttpListenerResponse response)
        {
            Respond(response, HttpStatusCode.Accepted);
        }
        
    }
}