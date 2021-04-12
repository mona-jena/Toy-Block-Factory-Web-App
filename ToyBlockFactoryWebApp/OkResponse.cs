using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class OkResponse : JSONResponse, IResponseHandler
    {
        public OkResponse(object responseObject) : base(responseObject)
        {
        }
        
        public void Respond(HttpListenerResponse response)
        {
            Respond(response, HttpStatusCode.OK);
        }
        
    }
}