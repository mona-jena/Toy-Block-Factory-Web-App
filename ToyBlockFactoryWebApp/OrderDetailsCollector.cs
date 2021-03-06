using System;
using System.Net;
using System.Text.Json;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;

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

        public Order CreateOrder(HttpListenerRequest request)
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
            
            HandleRequest(request, httpRequest.Body);
            _order.AddBlock(Shape.Square, Colour.Blue);
            _order.AddBlock(Shape.Square, Colour.Yellow);
            _order.AddBlock(Shape.Square, Colour.Blue);
            
            Console.WriteLine("End of client data:");

            return _order;
        }
        
        public record NewOrderDTO(string Name, string Address)
        {
        }
        
        
        public void HandleRequest(HttpListenerRequest request, string requestBody)
        {
            if (request.RawUrl == "/order" && request.HttpMethod == "POST")
            {
                var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(requestBody);
                _order = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address);
            }
            else if (request.RawUrl == "/order" && request.HttpMethod == "GET")
            {
                var orderId = request.QueryString.Get("orderId");
                Console.WriteLine(orderId);
                _order = _toyBlockFactory.GetOrder(orderId);
            }
        }
        
    }
}