using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToyBlockFactoryWebApp
{
    public class Router
    {
        private readonly HealthCheckController _healthCheckController;
        private readonly IController _orderController;

        public Router(HealthCheckController healthCheckController, IController orderController)
        {
            _healthCheckController = healthCheckController;
            _orderController = orderController;
        }

        private string GetRequestBody(HttpListenerRequest request)
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

        public void ReadRequests(HttpListenerContext context)
        {
            var request = context.Request;
            var requestBody = GetRequestBody(request);
            Console.WriteLine(requestBody);
            switch (request.Url?.AbsolutePath)
            {
                case "/health" when request.HttpMethod == "GET":
                    var healthMessage = _healthCheckController.HealthCheck();
                    SendResponse(context.Response, HttpStatusCode.OK, healthMessage);
                    break;
                
                case "/order" when request.HttpMethod == "POST":
                    var orderId = _orderController.Post(requestBody);
                    if (orderId == null) SendResponse(context.Response, HttpStatusCode.BadRequest);
                    else SendResponse(context.Response, HttpStatusCode.Accepted, orderId);
                    break;
                
                case "/addblock" when request.HttpMethod == "POST":
                    _orderController.PostAddBlock(request.QueryString, requestBody);
                    SendResponse(context.Response, HttpStatusCode.Accepted); 
                    break;
                
                case "/order" when request.HttpMethod == "DELETE":
                    var deleted = _orderController.Delete(request.QueryString);
                    if (!deleted) SendResponse(context.Response, HttpStatusCode.BadRequest);
                    else SendResponse(context.Response, HttpStatusCode.Accepted);
                    break;
                
                case "/order" when request.HttpMethod == "PUT":
                    var submitted = _orderController.Put(request.QueryString);
                    if (!submitted) SendResponse(context.Response, HttpStatusCode.BadRequest);
                    else SendResponse(context.Response, HttpStatusCode.Accepted); //TODO:RETURNS NULL IS FINE?
                    break;
                
                case "/report" when request.HttpMethod == "GET":
                    try
                    {
                        var report = _orderController.Get(request.QueryString);
                        if (report == null) SendResponse(context.Response, HttpStatusCode.BadRequest);
                        else SendResponse(context.Response, HttpStatusCode.NotFound, report); 
                    }
                    catch (ArgumentException e)
                    {
                        SendResponse(context.Response, HttpStatusCode.NotFound); 
                    }
                    SendResponse(context.Response, HttpStatusCode.Accepted);
                    break;
            }
        }

        private static void SendResponse(HttpListenerResponse response, HttpStatusCode statusCode, Object @object = null)
        {
            Console.WriteLine("End of client data.");
            response.Headers.Set("Server", "mona's-server");
            response.StatusCode = (int) statusCode;
            var options = new JsonSerializerOptions
            {
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
            };
            string responseString = JsonSerializer.Serialize(@object, options);
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

    }
}