using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryWebApp
{
    class OrderController
    {
        private static ToyBlockFactory _toyBlockFactory;

        public OrderController(ToyBlockFactory toyBlockFactory)
        {
            _toyBlockFactory = toyBlockFactory;
        }

        public record NewOrderDTO(string Name, string Address)
        {
        }
        
        public static void Post(HttpListenerContext context)
        {
            var httpRequest = new HttpRequest(context.Request);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            
            var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(httpRequest.Body);
            var order = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address);
            order.AddBlock(Shape.Square, Colour.Blue, 1);
            order.AddBlock(Shape.Square, Colour.Yellow, 1);
            order.AddBlock(Shape.Square, Colour.Blue, 1);
            
            var orderId = ToyServer._toyBlockFactory.SubmitOrder(order);
            Console.WriteLine("order submitted: " + orderId);
            if (order != null)
            {                                                   //RETURN REPORT STRAIGHT AWAY? //ASK TO CHOOSE?
                ToyServer.SendResponse(context.Response, ToyServer._toyBlockFactory.GetReport(orderId, ReportType.Invoice));
            }
            Console.WriteLine("End of client data:");
        }

        public void Get(HttpListenerContext context)
        {
            var httpRequest = new HttpRequest(context.Request);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            
            var orderId = context.Request.QueryString.Get("orderId");
            Console.WriteLine("Get " + orderId);
            var order = _toyBlockFactory.GetOrder(orderId); 
            if (order != null)
            {
                ToyServer.SendResponse(context.Response, ToyServer._toyBlockFactory.GetReport(order.OrderId, ReportType.Invoice));
            }
        }
        
        
        
        /*public Order ProcessRequest(HttpListenerRequest request, Order order)
        {
            var httpRequest = new HttpRequest(request);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            
            order = HandleRequest(request, httpRequest, order);
            
            Console.WriteLine("End of client data:");

            return order;
        }
        
        
        
        
        public Order HandleRequest(HttpListenerRequest listenerRequest, HttpRequest httpRequest, Order order)
        {
            var url = listenerRequest.Url.AbsolutePath;
            if (url == "/order" && httpRequest.Method == "POST")
            {
                var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(httpRequest.Body);
                order = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address);
                order.AddBlock(Shape.Square, Colour.Blue, 1);
                order.AddBlock(Shape.Square, Colour.Yellow, 1);
                order.AddBlock(Shape.Square, Colour.Blue, 1);
                return order;
            }
            if (url == "/order" && httpRequest.Method == "GET")
            {
                var orderId = listenerRequest.QueryString.Get("orderId");
                Console.WriteLine(orderId);
                order = _toyBlockFactory.GetOrder(orderId); 
                return order;
            }

            throw new ApplicationException("bad url");
        }

        public static void HandleRequest(HttpListenerContext context)
        {
            // desc the request - HttpMethod string, UserAgent string, and request body data 
            HttpListenerRequest request = context.Request;
            var orderCollector = new OrderController(ToyServer._toyBlockFactory);
            var order = orderCollector.ProcessRequest(request, ToyServer._order);

            if (request.HttpMethod == "POST")
            {
                var orderId = ToyServer._toyBlockFactory.SubmitOrder(order);
                Console.WriteLine("order submitted: " + orderId);
                if (order != null)
                {
                    ToyServer.SendResponse(context.Response, ToyServer._toyBlockFactory.GetReport(orderId, ReportType.Invoice));
                }
            }
            else if (request.HttpMethod == "GET")
            {
                if (order != null)
                {
                    ToyServer.SendResponse(context.Response, ToyServer._toyBlockFactory.GetReport(order.OrderId, ReportType.Invoice));
                }
            }
        }*/
    }
}