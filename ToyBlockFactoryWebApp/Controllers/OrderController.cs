using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text.Json;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryWebApp.DTOs;

namespace ToyBlockFactoryWebApp.Controllers
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

        public OrderDTO PostAddBlock(string orderId, BlockOrderDTO orderDetails)
        {
            if (orderId == null) 
                throw new ArgumentException("Must provide an Order ID!");
            
            var order = _toyBlockFactory.GetOrder(orderId, false);
            Console.WriteLine("Blocks added to order " + orderId + ": ");
            foreach (var block in orderDetails.Order)
            {
                order.AddBlock(block.Shape, block.Colour, block.Quantity);
                Console.WriteLine("{0} {1} {2}", block.Quantity, block.Colour, block.Shape);
            }
            
            return CreateOrderDTO(order);
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
           
            return CreateOrderDTO(submittedOrder);
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

            return CreateOrderDTO(order);
        }
        
        public List<OrderDTO> GetAllOrders()
        {
            List<OrderDTO> returnOrder = new();
            var orders = _toyBlockFactory.GetAllOrders();
            Console.WriteLine("Getting all orders:");
            foreach (var order in orders)
            {
                returnOrder.Add(CreateOrderDTO(order));
                Console.WriteLine(returnOrder);
            }

            return returnOrder;
        }

        private OrderDTO CreateOrderDTO(Order order)
        {
            return new OrderDTO(
                order.OrderId,
                order.Name,
                order.Address,
                order.BlockList.Select(kvp => new BlockDTO(kvp.Key.Colour, kvp.Key.Shape, kvp.Value)),
                order.DueDate
            );
        }
        
    }
}