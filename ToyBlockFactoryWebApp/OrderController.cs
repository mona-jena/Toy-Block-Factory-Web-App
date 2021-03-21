using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
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
        
        public record NewOrderDTO(string Name, string Address, List<BlockDTO> BlockList) 
        {
            
        }
        
        public void Post(HttpListenerContext context)
        {
            var httpRequest = new HttpRequest(context.Request);
            Console.WriteLine("Start of client data:");
            Console.WriteLine(httpRequest.Body);
            
            //var blockList = JsonSerializer.Deserialize<List<BlockDTO>>(httpRequest.Body.Substring(2));
            //var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(httpRequest.Body.Substring(0,2));
            var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(httpRequest.Body);
            var order = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address);
            foreach (var block in customerDetails.BlockList)
            {
                order.AddBlock(block.Shape, block.Colour, block.Quantity);
            }
            
            var orderId = _toyBlockFactory.SubmitOrder(order);
            Console.WriteLine("order submitted: " + orderId);
            if (orderId != null)
            {                                                   //RETURN REPORT STRAIGHT AWAY?    //ASK TO CHOOSE?
                ToyServer.SendResponse(context.Response, _toyBlockFactory.GetReport(orderId, ReportType.Invoice));
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
                ToyServer.SendResponse(context.Response, _toyBlockFactory.GetReport(order.OrderId, ReportType.Invoice));
            }
            Console.WriteLine("End of client data:");
        }
        
    }
}