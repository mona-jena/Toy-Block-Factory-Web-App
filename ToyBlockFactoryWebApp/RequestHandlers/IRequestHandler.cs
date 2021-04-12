using System.Net;
using ToyBlockFactoryWebApp.Responses;

namespace ToyBlockFactoryWebApp.RequestHandlers
{
    public interface IRequestHandler
    {
        bool ShouldHandle(string url, string httpMethod);
        IResponseHandler Handle(HttpListenerRequest request);
    }
}