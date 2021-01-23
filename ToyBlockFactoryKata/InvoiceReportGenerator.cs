using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class InvoiceReportGenerator : IReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _priceList;
        private readonly Order _requestedOrder;
        private readonly Report _report = new();
        
        internal InvoiceReportGenerator(IInvoiceCalculationStrategy priceList, Order requestedOrder)
        {
            _priceList = priceList;
            _requestedOrder = requestedOrder;
        }

        public void GenerateReport() //date collator 
        {
            _report.ReportType = ReportType.Invoice;
            _report.Name = _requestedOrder.Name;
            _report.Address = _requestedOrder.Address;
            _report.DueDate = Convert.ToDateTime(_requestedOrder.DueDate); //change to datetime
            _report.OrderId = _requestedOrder.OrderId;
        }

        public void AddLineItem()
        {
            _report.LineItems.Add(new List<object>
            {
                block.Key.Name, 
                // CalculateShapeQuantity(shape)
                _priceList
                total
            });
            
            
            CalculateShapeQuantity();


            foreach (var block in _requestedOrder.BlockList)
            {
                _report.LineItems.Add(new List<object>
                {
                    block.Key.Name, 
                    block.Key
                });
            }
            
        }

        private void CalculateShapeQuantity()
        {
            var squareCount = 0;
            var triangleCount = 0;
            var circleCount = 0;

            foreach (var block in _requestedOrder.BlockList)
            {
                if (block.Key.Shape.Equals(Shape.Square))
                    squareCount++;
                else if (block.Key.Shape.Equals(Shape.Triangle))
                    triangleCount++;
                else if (block.Key.Shape.Equals(Shape.Circle))
                    circleCount++;
            }
        }
    }
    
}