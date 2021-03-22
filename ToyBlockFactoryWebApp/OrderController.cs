using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryWebApp
{
    public class OrderController
    {
        private static ToyBlockFactory _toyBlockFactory;

        public OrderController(ToyBlockFactory toyBlockFactory)
        {
            _toyBlockFactory = toyBlockFactory;
        }

        public record BlockDTO(Colour Colour, Shape Shape, int Quantity)
        {
            
        }
        
        /*public record NewOrderDTO(string Name, string Address, List<BlockDTO> Order) 
        {
            
        }*/
        
        public record BlockOrderDTO(List<BlockDTO> Order) 
        {
            
        }
        
        public record NewOrderDTO(string Name, string Address) 
        {
            
        }
        
        public void PostAddBlock(HttpListenerContext context)
        {
            var httpRequest = new HttpRequest(context.Request);
            //var previousUrl = context.Request.UrlReferrer.AbsolutePath;
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            
            var orderDetails = JsonSerializer.Deserialize<BlockOrderDTO>(httpRequest.Body, options);
            
            //if (context.Request.UrlReferrer.AbsolutePath == "/order")  //NECESSARY??
            
            var orderId = context.Request.QueryString.Get("orderId");
            var order = _toyBlockFactory.GetOrder(orderId);
            if (orderId != null)
            {
                foreach (var block in orderDetails.Order)
                {
                    order.AddBlock(block.Shape, block.Colour, block.Quantity);
                }
            }
                
            ToyServer.SendResponse(context.Response, _toyBlockFactory.GetReport(orderId, ReportType.Invoice));
        
            // var orderId = _toyBlockFactory.SubmitOrder(order);
            // Console.WriteLine("order submitted: " + orderId);
            Console.WriteLine("End of client data:");
            /*if (orderId != null)
            {                                                   //RETURN REPORT STRAIGHT AWAY?    //ASK TO CHOOSE?
                ToyServer.SendResponse(context.Response, _toyBlockFactory.GetReport(orderId, ReportType.Invoice));
            }*/
            
        }
        
        public void Post(HttpListenerContext context)
        {
            var httpRequest = new HttpRequest(context.Request);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(httpRequest.Body);
            var orderId = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address);
            Console.WriteLine("order created: " + orderId);
            Console.WriteLine("End of client data:");
            ToyServer.SendResponse(context.Response, orderId);
        }

        public void Get(HttpListenerContext context)
        {
            var httpRequest = new HttpRequest(context.Request);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            
            var orderId = context.Request.QueryString.Get("orderId");
            Console.WriteLine("Get " + orderId);
            var reportType = (ReportType) Enum.Parse(typeof(ReportType),context.Request.QueryString.Get("ReportType"));
            Console.WriteLine("Get " + reportType);
            var order = _toyBlockFactory.GetOrder(orderId); 
            if (order != null)
            {
                ToyServer.SendResponse(context.Response, _toyBlockFactory.GetReport(order.OrderId, reportType));
            }
            Console.WriteLine("End of client data:");
        }


        public void Put(HttpListenerContext context)
        {
            var httpRequest = new HttpRequest(context.Request);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            var orderId = context.Request.QueryString.Get("orderId");
            Console.WriteLine("Put " + orderId);
            var order = _toyBlockFactory.GetOrder(orderId);
            var submittedOrderId = _toyBlockFactory.SubmitOrder(order);
            Console.WriteLine(submittedOrderId);
            if (submittedOrderId == orderId)
            {
                ToyServer.SendResponse(context.Response, submittedOrderId);
            }
            Console.WriteLine("End of client data:");
            
        }

        public void Delete(HttpListenerContext context)
        {
            var httpRequest = new HttpRequest(context.Request);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            var orderId = context.Request.QueryString.Get("orderId");
            Console.WriteLine("Delete " + orderId);
            _toyBlockFactory.DeleteOrder(orderId);
            if (_toyBlockFactory.OrderExists(orderId) == false)
            {
                ToyServer.SendResponse(context.Response, orderId);      //WHAT SHOULD IT RETURN??
            }
        }
    }
}