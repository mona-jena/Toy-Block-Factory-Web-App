using System;
using System.IO;
using System.Net;

namespace ToyBlockFactoryWebApp
{
    public interface IResponseHandler
    {
        void Respond(HttpListenerResponse response);
        
        
    }

    
}