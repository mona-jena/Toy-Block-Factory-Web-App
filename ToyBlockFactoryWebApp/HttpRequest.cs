using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace ToyBlockFactoryWebApp
{
    class HttpRequest
    {
        public string Method { get; }
        public string Url { get; }
        public string Body { get; }

        public HttpRequest(HttpListenerRequest request)
        {
            Method = request.HttpMethod;
            Url = request.RawUrl;
            Body = GetRequestBody(request);
        }

        public string GetRequestBody(HttpListenerRequest request)
        {
            var body = request.InputStream;
            var encoding = request.ContentEncoding;
            var reader = new StreamReader(body, encoding);
            string bodyString = reader.ReadToEnd();
            body.Close();
            reader.Close();
            return bodyString;
        }
        
    }    
}