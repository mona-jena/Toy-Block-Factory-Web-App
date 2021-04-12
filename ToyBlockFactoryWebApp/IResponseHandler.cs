using System.Net;

namespace ToyBlockFactoryWebApp
{
    public interface IResponseHandler
    {
        void Respond(HttpListenerResponse response);
    }
}