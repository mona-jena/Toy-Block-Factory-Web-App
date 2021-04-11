using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToyBlockFactoryKata;
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
            var customerDetails = JsonSerializer.Deserialize<NewOrderDTO>(requestBody) 
                                  ?? throw new InvalidDataException("Invalid order data");
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
            var orderDetails = JsonSerializer.Deserialize<BlockOrderDTO>(requestBody, options) 
                               ?? throw new InvalidDataException("Invalid block order data");
            var orderId = queryString.Get("orderId");
            if (orderId == null) 
                throw new ArgumentException("Must provide an Order ID!");
            
            var order = _toyBlockFactory.GetOrder(orderId, false);
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

            return _toyBlockFactory.OrderExists(orderId) && _toyBlockFactory.DeleteOrder(orderId);
        }
        
        public OrderDTO Put(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderId");
            var order = _toyBlockFactory.GetOrder(orderId, false);
            var submittedOrderId = _toyBlockFactory.SubmitOrder(order);
            Console.WriteLine("Submitted order: " + submittedOrderId);
            var submittedOrder = _toyBlockFactory.GetOrder(submittedOrderId, true);
            var returnOrder = new OrderDTO(
                submittedOrder.OrderId,
                submittedOrder.Name,
                submittedOrder.Address,
                submittedOrder.BlockList.Select(kvp => new BlockDTO(kvp.Key.Colour, kvp.Key.Shape, kvp.Value)),
                submittedOrder.DueDate
            );
            
            return returnOrder;
        }
        
        public IReport GetReport(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderId");
            var reportType = (ReportType) Enum.Parse(typeof(ReportType), queryString.Get("ReportType") 
                              ?? throw new InvalidDataException("Invalid report type"));
            Console.WriteLine("Get " + reportType + " Report for order: " + orderId);
            
            return _toyBlockFactory.GetReport(orderId, reportType);
        }
        
        public OrderDTO GetOrder(NameValueCollection queryString)
        {
            var orderId = queryString.Get("orderid");
            Console.WriteLine("Getting order: " + orderId);
            var submitted = _toyBlockFactory.IsOrderSubmitted(orderId);  
            var order = _toyBlockFactory.GetOrder(orderId, submitted);
            var returnOrder = new OrderDTO(
                order.OrderId,
                order.Name,
                order.Address,
                order.BlockList.Select(kvp => new BlockDTO(kvp.Key.Colour, kvp.Key.Shape, kvp.Value)),
                order.DueDate
            );
            
            return returnOrder;
        }
        
        public List<OrderDTO> GetAllOrders()
        {
            List<OrderDTO> returnOrder = new();
            Console.WriteLine("Getting all orders:");
            var orders = _toyBlockFactory.GetAllOrders();
            foreach (var order in orders)
            {
                returnOrder.Add(new OrderDTO(
                    order.OrderId,
                    order.Name,
                    order.Address,
                    order.BlockList.Select(kvp => new BlockDTO(kvp.Key.Colour, kvp.Key.Shape, kvp.Value)),
                    order.DueDate
                ));
                Console.WriteLine(returnOrder);
            }

            return returnOrder;
        }
        
    }
}