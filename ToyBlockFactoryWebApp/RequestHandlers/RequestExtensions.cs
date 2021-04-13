using System;
using System.IO;
using System.Net;

namespace ToyBlockFactoryWebApp.RequestHandlers
{
    public static class RequestExtensions
    {
        public static string GetRequestBody(this HttpListenerRequest request)
        {
            Console.WriteLine("Start of client data:");
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