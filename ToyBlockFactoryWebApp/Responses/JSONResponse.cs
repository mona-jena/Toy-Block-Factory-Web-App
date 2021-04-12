using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToyBlockFactoryWebApp.Responses
{
    public abstract class JSONResponse
    {
        private readonly object _responseObject;

        public JSONResponse(object responseObject)
        {
            _responseObject = responseObject;
        }

        public virtual void Respond(HttpListenerResponse response, HttpStatusCode statusCode)
        {
            response.Headers.Set("Server", "mona's-server");
            response.StatusCode = (int) statusCode;

            var options = new JsonSerializerOptions
            {
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
            };
            byte[] buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(_responseObject, options));
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

       
    }
}