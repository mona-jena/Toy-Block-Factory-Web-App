using System;
using System.Net;

namespace ToyBlockFactoryWebApp
{
    class HttpResponse
    {
        public int StatusCode { get; }
        
        public HttpResponse(HttpListenerResponse response, Object order)
        {
            StatusCode = response.StatusCode;
            //GetReponseBody(response, order);
        }

        /*private void GetReponseBody(HttpListenerResponse response, object order)
        {
            string responseString = JsonSerializer.Serialize(order);
            // Get a response stream and write the response to it.
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }*/
    }
}