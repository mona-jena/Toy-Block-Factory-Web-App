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
        private readonly ToyBlockFactory _toyBlockFactory;

        public OrderDetailsCollector(ToyBlockFactory toyBlockFactory)
        {
            _toyBlockFactory = toyBlockFactory;
        }

        public Order ProcessRequest(HttpListenerRequest request, Order order)
        {
            var httpRequest = new HttpRequest(request);
            
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            try
            {
                HandleRequest(request, httpRequest, order);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("catch an error"); //THROW AN ERROR CODE
            }
            Console.WriteLine("End of client data:");

            return order;
        }
        
        public record NewOrderDTO(string Name, string Address)
        {
        }
        
        
        public void HandleRequest(HttpListenerRequest listenerRequest, HttpRequest httpRequest, Order order)
        {
            var url = listenerRequest.Url.AbsolutePath;
            if (url == "/order" && httpRequest.Method == "POST")
            {
                var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(httpRequest.Body);
                order = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address);
                order.AddBlock(Shape.Square, Colour.Blue);
                order.AddBlock(Shape.Square, Colour.Yellow);
                order.AddBlock(Shape.Square, Colour.Blue);
            }
            else if (url == "/order" && httpRequest.Method == "GET")
            {
                var orderId = listenerRequest.QueryString.Get("orderId");
                Console.WriteLine(orderId);
                order = _toyBlockFactory.GetOrder(orderId); //RETURN ORDER!!!
            }
        }
        
    }
}