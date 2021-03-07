using System;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryWebApp
{
    class OrderDetailsCollector
    {
        private Order _order;
        private readonly ToyBlockFactory _toyBlockFactory;

        public OrderDetailsCollector(Order order, ToyBlockFactory toyBlockFactory)
        {
            _order = order;
            _toyBlockFactory = toyBlockFactory;
        }

        public Order ProcessRequest(HttpListenerRequest request)
        {
            var httpRequest = new HttpRequest(request);
            if (!request.HasEntityBody)
            {
                Console.WriteLine("No client data was sent with the request.");
                return null;
            }
            
            if (request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", request.ContentType);
            }

            Console.WriteLine("Client data content length {0}", request.ContentLength64);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            
            HandleRequest(request, httpRequest);
            
            Console.WriteLine("End of client data:");

            return _order;
        }
        
        public record NewOrderDTO(string Name, string Address)
        {
        }
        
        
        public void HandleRequest(HttpListenerRequest listenerRequest, HttpRequest httpRequest)
        {
            var url = listenerRequest.Url.AbsolutePath;
            if (url == "/order" && httpRequest.Method == "POST")
            {
                var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(httpRequest.Body);
                _order = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address);
                _order.AddBlock(Shape.Square, Colour.Blue);
                _order.AddBlock(Shape.Square, Colour.Yellow);
                _order.AddBlock(Shape.Square, Colour.Blue);
            }
            else if (url == "/order" && httpRequest.Method == "GET")
            {
                var orderId = listenerRequest.QueryString.Get("orderId");
                Console.WriteLine(orderId);
                _order = _toyBlockFactory.GetOrder(orderId);
            }
        }
        
    }
}