using System;
using System.Net;

namespace ToyBlockFactoryWebApp
{
    public interface IRequestHandler
    {
        bool ShouldHandle(string url, string httpMethod);
        IResponseHandler Handle(string requestBody);
    }
}