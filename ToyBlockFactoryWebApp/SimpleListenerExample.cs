using System;
using System.Net;
using System.Text.Json;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryWebApp
{
    class SimpleListenerExample
    {
        private static readonly ToyBlockFactory _toyBlockFactory = new (new LineItemsCalculator());
        private static Order _order;
        private readonly HttpListener _httpListener;
        private readonly string[] _uri;

        public SimpleListenerExample(string[] prefixes)
        {
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("prefixes");
            }
            _uri = prefixes;
            _httpListener = new HttpListener();
            Start();
        }

        public void Start()
        {
            foreach (string s in _uri)
            {
                _httpListener.Prefixes.Add(s);
            }
            _httpListener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                HttpListenerContext context = _httpListener.GetContext(); //HTTP REQUEST
                HandleRequest(context);
            }

            _httpListener.Stop();
        }

        private static void HandleRequest(HttpListenerContext context)
        {
            // desc the request - HttpMethod string, UserAgent string, and request body data 
            HttpListenerRequest request = context.Request;
            var orderCollector = new OrderDetailsCollector(_order, _toyBlockFactory);
            var order = orderCollector.ProcessRequest(request);
            
            if (request.HttpMethod == "POST")
            {
                var orderId = _toyBlockFactory.SubmitOrder(order);
                Console.WriteLine("order submitted: " + orderId);
                if (order != null)
                {
                    SendResponse(context.Response, _toyBlockFactory.GetReport(orderId, ReportType.Invoice));
                }
            }
            else if (request.HttpMethod == "GET")
            {
                SendResponse(context.Response, _toyBlockFactory.GetReport(order.OrderId, ReportType.Invoice));
            }
            
        }

        private static void SendResponse(HttpListenerResponse response, Object order)
        {
            //var httpResponse = new HttpResponse(response, order);
            response.Headers.Set("Server", "mona's-server");
            response.StatusCode = 201;

            string responseString = JsonSerializer.Serialize(order);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}
