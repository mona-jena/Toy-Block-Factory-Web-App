using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToyBlockFactoryWebApp
{
    public class Router
    {
        private readonly IRequestHandler[] _handlers;

        public Router(OrderController orderController)
        {
            _handlers = new IRequestHandler[] {new HealthCheckHandler(), 
                new OrderCreateHandler(orderController), 
                new OrderAddBlocksHandler(orderController), 
                new SingleOrderGetHandler(orderController),
                new OrdersGetHandler(orderController),
                new OrderDeleteHandler(orderController),
                new SubmitOrderHandler(orderController),
                new GetReportHandler(orderController)
            };
        }

        public void ReadRequests(HttpListenerContext context)
        {
            var request = context.Request;
            var handler = _handlers.FirstOrDefault(h => h.ShouldHandle(request.Url?.AbsolutePath, request.HttpMethod));
            if (handler != null)
            {
                var responseHandler = handler.Handle(request);
                responseHandler.Respond(context.Response);
                return;
            }
            /*switch (request.Url?.AbsolutePath)
            {
                case "/order" when request.HttpMethod == "POST":
                    var orderId = _orderController.Post(requestBody);
                    if (orderId == null) SendResponse(context.Response, HttpStatusCode.BadRequest);
                    else SendResponse(context.Response, HttpStatusCode.Accepted, orderId);
                    break;
                 
                case "/addblock" when request.HttpMethod == "POST":
                    try
                    {
                        var blockOrder = _orderController.PostAddBlock(request.QueryString, requestBody);
                        SendResponse(context.Response, HttpStatusCode.Accepted, blockOrder);
                    }
                    catch (ArgumentException)
                    {
                        SendResponse(context.Response, HttpStatusCode.BadRequest);
                    }
                    break;
                
                case "/order" when request.HttpMethod == "GET":
                    var order = _orderController.GetOrder(request.QueryString);
                    if (order == null) SendResponse(context.Response, HttpStatusCode.NotFound);
                    else SendResponse(context.Response, HttpStatusCode.Accepted, order);
                    break;
            
                case "/orders" when request.HttpMethod == "GET":
                    try
                    {
                        var orders = _orderController.GetAllOrders();
                        SendResponse(context.Response, HttpStatusCode.Accepted, orders);
                    }
                    catch (ArgumentException)
                    {
                        SendResponse(context.Response, HttpStatusCode.BadRequest);
                    }
                    break;
                
                case "/order" when request.HttpMethod == "DELETE":
                    var deleted = _orderController.Delete(request.QueryString);
                    if (!deleted) SendResponse(context.Response, HttpStatusCode.BadRequest);
                    else SendResponse(context.Response, HttpStatusCode.Accepted);
                    break;
                
                case "/order" when request.HttpMethod == "PUT":
                    var submittedOrder = _orderController.Put(request.QueryString);
                    if (submittedOrder == null) SendResponse(context.Response, HttpStatusCode.BadRequest);
                    else SendResponse(context.Response, HttpStatusCode.Accepted, submittedOrder); 
                    break;
                
                case "/report" when request.HttpMethod == "GET":
                    try
                    {
                        var report = _orderController.GetReport(request.QueryString);
                        SendResponse(context.Response, HttpStatusCode.Accepted, report);
                    }
                    catch (InvalidDataException)
                    {
                        SendResponse(context.Response, HttpStatusCode.Forbidden);
                    }
                    break;
            }*/
        }

        private static void SendResponse(HttpListenerResponse response, HttpStatusCode statusCode, Object @object = null)
        {
            Console.WriteLine("End of client data.");
            response.Headers.Set("Server", "mona's-server");
            response.StatusCode = (int) statusCode;
            if (@object == null) return;              //TODO: CAUSING THREADING ISSUES!!
            
            var options = new JsonSerializerOptions
            {
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
            };
            string responseString = JsonSerializer.Serialize(@object, options);
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            response.Close();
        }
        
    }
}