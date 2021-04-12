using System.Net;

namespace ToyBlockFactoryWebApp.Responses
{
    public interface IResponseHandler
    {
        void Respond(HttpListenerResponse response);
    }

}