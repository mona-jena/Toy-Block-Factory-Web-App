using System;
using System.Net;
using System.Text.Json;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryWebApp
{
    class Program
    {
        private static ToyBlockFactory _toyBlockFactory = new (new LineItemsCalculator());
        static Order _order;
        
        static void Main(string[] args)
        {
            string[] prefixes = {"http://localhost:3000/"};
            SimpleListenerExample(prefixes);
        }

        private static void SimpleListenerExample(string[] prefixes)
        {
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("prefixes");
            }
                
            HttpListener listener = new HttpListener();
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();        //HTTP REQUEST
                HandleRequest(context);
            }
            
            listener.Stop();
        }
        
        private static void HandleRequest(HttpListenerContext context)
        {
            // desc the request - HttpMethod string, UserAgent string, and request body data 
            HttpListenerRequest request = context.Request;
            var order = CollectCustomerDetails(request);
            order.AddBlock(Shape.Square, Colour.Blue);
            order.AddBlock(Shape.Square, Colour.Yellow);
            order.AddBlock(Shape.Square, Colour.Blue);
            var orderId = _toyBlockFactory.SubmitOrder(order);
            if (order != null)
            {
                SendResponse(context, _toyBlockFactory.GetReport(orderId, ReportType.Invoice));
            }
        }
        
        public record NewOrderDTO(string Name, string Address)
        {
            
        }

        public static Order CollectCustomerDetails(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                Console.WriteLine("No client data was sent with the request.");
                return null;
            }
            var body = request.InputStream;
            var encoding = request.ContentEncoding;
            var reader = new System.IO.StreamReader(body, encoding);
            if (request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", request.ContentType);
            }
            Console.WriteLine("Client data content length {0}", request.ContentLength64);
            Console.WriteLine("Start of client data:");
            string bodyString = reader.ReadToEnd();
            Console.WriteLine(bodyString);
            
            
            if (request.RawUrl == "/order" && request.HttpMethod == "POST")
            {
                var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(bodyString);
                _order = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address);
            }
            else if (request.RawUrl == "/order" && request.HttpMethod == "GET")
            {
                var orderId = request.QueryString.Get("orderId");
                _order = _toyBlockFactory.GetOrder(orderId);
            }
            
            Console.WriteLine("End of client data:");
                
            body.Close();
            reader.Close();

            return _order;
        }

        private static void SendResponse(HttpListenerContext context, Object order)
        {
            // object that will be sent to the client in response to the client's request
            HttpListenerResponse response = context.Response;
            
            response.Headers.Set("Server", "mona's-server");
            response.StatusCode = 201;
            
            // converts order to JSON string
            string responseString = JsonSerializer.Serialize(order);
            // Get a response stream and write the response to it.
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            // prints out response 
            output.Write(buffer, 0, buffer.Length);
            
            output.Close();
        }

    }
}