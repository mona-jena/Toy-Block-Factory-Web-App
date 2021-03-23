using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ToyBlockFactoryWebApp
{
    public class Router
    {
        private readonly HealthCheckController _healthCheckController;
        private readonly OrderController _orderController;

        public Router(HealthCheckController healthCheckController, OrderController orderController)
        {
            _healthCheckController = healthCheckController;
            _orderController = orderController;
        }
        
        public string GetRequestBody(HttpListenerRequest request)
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

        public void ReadRequests(HttpListenerRequest request, HttpListenerContext context)
        {
            var requestBody = GetRequestBody(request);
            switch (request.Url.AbsolutePath)
            {
                case "/health" when request.HttpMethod == "GET":
                    var healthMessage = _healthCheckController.HealthCheck();
                    SendResponse(context.Response, HttpStatusCode.Accepted, healthMessage);
                    break;
                case "/order" when request.HttpMethod == "POST":
                    var orderId = _orderController.Post(requestBody);
                    SendResponse(context.Response, HttpStatusCode.Accepted, orderId);
                    break;
                case "/addblock" when request.HttpMethod == "POST":
                    var postResult = _orderController.PostAddBlock(request.QueryString, requestBody);
                    SendResponse(context.Response, HttpStatusCode.Accepted, postResult);
                    break;
                case "/report" when request.HttpMethod == "GET":
                    var getResult = _orderController.Get(request.QueryString, requestBody);
                    SendResponse(context.Response, HttpStatusCode.Accepted, getResult);
                    break;
                case "/order" when request.HttpMethod == "PUT":
                    var ifSubmitted = _orderController.Put(request.QueryString, requestBody);
                    if (!ifSubmitted)
                    {
                        SendResponse(context.Response, HttpStatusCode.BadRequest);
                    }
                    SendResponse(context.Response, HttpStatusCode.Accepted);
                    break;
                case "/order" when request.HttpMethod == "DELETE":
                    var ifDeleted = _orderController.Delete(request.QueryString, requestBody);
                    if (!ifDeleted)
                    {
                        SendResponse(context.Response, HttpStatusCode.BadRequest);
                    }
                    SendResponse(context.Response, HttpStatusCode.Accepted);
                    break;
            }
        }

        public static void SendResponse(HttpListenerResponse response, HttpStatusCode statusCode, object @object = null)
        {
            response.Headers.Set("Server", "mona's-server");
            response.StatusCode = (int) statusCode;

            string responseString = JsonSerializer.Serialize(@object);
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
        
        
        
    }
}