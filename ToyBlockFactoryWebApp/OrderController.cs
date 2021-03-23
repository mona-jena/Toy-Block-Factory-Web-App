using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public string Post(string requestBody)
        {
            var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(requestBody);
            var orderId = _toyBlockFactory.CreateOrder(customerDetails.Name, customerDetails.Address).OrderId;
            Console.WriteLine("Created order: " + orderId);
            return orderId;
        }

        private record NewOrderDTO(string Name, string Address)
        {
        }

        private record BlockOrderDTO(List<BlockDTO> Order)
        {
        }

        private record BlockDTO(Colour Colour, Shape Shape, int Quantity)
        {
        }
        
        public void PostAddBlock(NameValueCollection queryString, string requestBody)
        {
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };

            var orderDetails = JsonSerializer.Deserialize<BlockOrderDTO>(requestBody, options);

            var orderId = queryString.Get("orderId");
            var order = _toyBlockFactory.GetOrder(orderId);
            Console.WriteLine("Blocks added to order " + orderId + ": ");
            if (orderId != null)
                foreach (var block in orderDetails.Order)
                {
                    order.AddBlock(block.Shape, block.Colour, block.Quantity);
                    Console.WriteLine("{0} {1} {2}", block.Quantity, block.Colour, block.Shape);
                }
        }

        public IReport Get(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderId");
            var reportType = (ReportType) Enum.Parse(typeof(ReportType), queryString.Get("ReportType"));
            Console.WriteLine("Get " + reportType + "for order " + orderId);
            var order = _toyBlockFactory.GetOrder(orderId);
            return _toyBlockFactory.GetReport(order.OrderId, reportType);
        }

        public bool Put(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderId");
            var order = _toyBlockFactory.GetOrder(orderId);
            var submittedOrderId = _toyBlockFactory.SubmitOrder(order);
            Console.WriteLine("Submit " + submittedOrderId);
            if (submittedOrderId != orderId) return false;
            return true;
        }

        public bool Delete(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderId");
            Console.WriteLine("Delete " + orderId);

            if (!_toyBlockFactory.OrderExists(orderId)) return false;
            _toyBlockFactory.DeleteOrder(orderId);
            return true;
        }

    }
}