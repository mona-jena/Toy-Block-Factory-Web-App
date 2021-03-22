using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public record BlockOrderDTO(List<BlockDTO> Order) 
        {
            
        }
        
        public record NewOrderDTO(string Name, string Address) 
        {
            
        }
        
        public IReport PostAddBlock(NameValueCollection queryString, string requestBody)
        {
            Console.WriteLine(requestBody);
            
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            
            var orderDetails = JsonSerializer.Deserialize<BlockOrderDTO>(requestBody, options);
            
            //if (context.Request.UrlReferrer.AbsolutePath == "/order")  //NECESSARY??
            
            var orderId = queryString.Get("orderId");
            var order = _toyBlockFactory.GetOrder(orderId);
            if (orderId != null)
            {
                foreach (var block in orderDetails.Order)
                {
                    order.AddBlock(block.Shape, block.Colour, block.Quantity);
                }
            }
            Console.WriteLine("End of client data:");
            
            return _toyBlockFactory.GetReport(orderId, ReportType.Invoice);
            
        }
        
        public string Post(string requestBody)
        {
            Console.WriteLine(requestBody);
            var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(requestBody);
            return _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address).OrderId;
        }

        public IReport Get(NameValueCollection queryString, string requestBody)
        {
            Console.WriteLine(requestBody);
            
            var orderId = queryString.Get("orderId");
            Console.WriteLine("Get " + orderId);
            var reportType = (ReportType) Enum.Parse(typeof(ReportType),queryString.Get("ReportType"));
            Console.WriteLine("Get " + reportType);
            var order = _toyBlockFactory.GetOrder(orderId); 
            return _toyBlockFactory.GetReport(order.OrderId, reportType);
        }


        public bool Put(NameValueCollection queryString, string requestBody)
        {
            Console.WriteLine(requestBody);
            var orderId = queryString.Get("orderId");
            Console.WriteLine("Put " + orderId);
            var order = _toyBlockFactory.GetOrder(orderId);
            var submittedOrderId = _toyBlockFactory.SubmitOrder(order);
            Console.WriteLine(submittedOrderId);
            if (submittedOrderId != orderId)
            {
                return false;
            }

            return true;

        }

        public bool Delete(NameValueCollection queryString, string requestBody)
        {
            Console.WriteLine(requestBody);
            var orderId = queryString.Get("orderId");
            Console.WriteLine("Delete " + orderId);

            if (!_toyBlockFactory.OrderExists(orderId))
            {
                return false;
            }
            _toyBlockFactory.DeleteOrder(orderId);
            return true;
        }


        
    }
}