using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryWebApp
{
    public class OrderController : IController
    {
        private static ToyBlockFactory _toyBlockFactory;

        public OrderController(ToyBlockFactory toyBlockFactory)
        {
            _toyBlockFactory = toyBlockFactory;
        }

        public string Post(string requestBody)
        {
            var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(requestBody) ?? throw new InvalidDataException("Invalid order data");
            var orderId = customerDetails.DueDate.HasValue
                ? _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address, customerDetails.DueDate.Value).OrderId
                : _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address).OrderId;
            
            Console.WriteLine("Created order: " + orderId);
            return orderId;
        }

        public void PostAddBlock(NameValueCollection queryString, string requestBody)
        {
            var options = new JsonSerializerOptions
            {
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
            };
            
            var orderDetails = JsonSerializer.Deserialize<BlockOrderDTO>(requestBody, options) ?? throw new InvalidDataException("Invalid block order data");
            var orderId = queryString.Get("orderId");
            var order = _toyBlockFactory.GetOrder(orderId);
            if (orderId == null) return;
            
            Console.WriteLine("Blocks added to order " + orderId + ": ");
            foreach (var block in orderDetails.Order)
            {
                order.AddBlock(block.Shape, block.Colour, block.Quantity);
                Console.WriteLine("{0} {1} {2}", block.Quantity, block.Colour, block.Shape);
            }
        }

        public bool Delete(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderId");
            Console.WriteLine("Delete order: " + orderId);

            if (!_toyBlockFactory.OrderExists(orderId)) return false;
            _toyBlockFactory.DeleteOrder(orderId);
            return true;
        }
        
        public bool Put(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderId");
            var order = _toyBlockFactory.GetOrder(orderId);
            var submittedOrderId = _toyBlockFactory.SubmitOrder(order);
            Console.WriteLine("Submitted order: " + submittedOrderId);
            
            if (submittedOrderId != orderId) return false;
            return true;
        }
        
        public IReport Get(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderId");
            var reportType = (ReportType) Enum.Parse(typeof(ReportType), queryString.Get("ReportType") ?? throw new InvalidDataException("Invalid report type"));
            Console.WriteLine("Get " + reportType + " Report for order: " + orderId);
            //var order = _toyBlockFactory.GetReport(orderId, reportType);
            
            return _toyBlockFactory.GetReport(orderId, reportType);
        }

    }
}