using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.VisualBasic;

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

        public Report GenerateReport() //date collator 
        {
            _report.ReportType = ReportType.Invoice;
            _report.Name = _requestedOrder.Name;
            _report.Address = _requestedOrder.Address;
            _report.DueDate = _requestedOrder.DueDate; //change to datetime
            _report.OrderId = _requestedOrder.OrderId;
            AddLineItem();
            return _report;
        }

        public void AddLineItem()
        {
            foreach (var shape in (Shape[]) Enum.GetValues(typeof(Shape)))
            {
                var shapeQuantity = CalculateShapeQuantity(shape);
                var shapePrice = GetPrice(shape);
                _report.LineItems.Add(new List<object>
                {
                    shape, 
                    shapeQuantity,
                    shapePrice,
                    _priceList.CalculateInvoiceLine(shapeQuantity, shapePrice)
                });
            }
        }

        private int CalculateShapeQuantity(Shape shape)
        {
            var shapeQuantity = 0;
            foreach (var block in _requestedOrder.BlockList)
            {
                if (block.Key.Shape.Equals(shape))
                    shapeQuantity++;
            }

            return shapeQuantity;
        }

        private int GetPrice(Shape shape)
        {
            var SGFJJJR = shape.ToString();
            if (SGFJJJR == "Square")
                return _priceList.Square;
            if (SGFJJJR == "Triangle")
                return _priceList.Triangle;
            if (SGFJJJR == "Circle")
                return _priceList.Circle;
            return 0;                           //is this ok?
        }


        /*private void CalculateShapeQuantity()
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
        }*/
    }
    
    
    /*
     * for each line for the length of number of enums:
     * calc total quan for each shape
     * insert price (shape)
     * calc tolal 
     */
}