using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    internal class InvoiceReportGenerator : IReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _priceList;
        private readonly Order _requestedOrder;
        private readonly Report _report = new();
        private int _redBlockQuantity;
        
        internal InvoiceReportGenerator(IInvoiceCalculationStrategy priceList, Order requestedOrder)
        {
            _priceList = priceList;
            _requestedOrder = requestedOrder;
        }

        public Report GenerateReport() //Should sep setup and getting part of report?
        {
            _report.ReportType = ReportType.Invoice;
            _report.Name = _requestedOrder.Name;
            _report.Address = _requestedOrder.Address;
            _report.DueDate = _requestedOrder.DueDate; //change to datetime
            _report.OrderId = _requestedOrder.OrderId;
            AddLineItems();
            return _report;
        }

        private void AddLineItems() 
        {
            foreach (var shape in (Shape[]) Enum.GetValues(typeof(Shape)))
            {
                var shapeQuantity = CalculateShapeQuantity(shape);
                var shapePrice = GetPrice(shape);
                _report.LineItems.Add(new InvoiceLine(
                    shape.ToString(),
                    shapeQuantity,
                    shapePrice
                ));
            }
            
            _report.LineItems.Add(new InvoiceLine(
                Colour.Red + " colour surcharge", 
                _redBlockQuantity,
                _priceList.Red
            ));
        }

        private int CalculateShapeQuantity(Shape shape)
        {
            var shapeQuantity = 0;
            foreach (var block in _requestedOrder.BlockList)
            {
                if (!block.Key.Shape.Equals(shape)) continue;
                shapeQuantity += block.Value;
                if (block.Key.Colour.Equals(Colour.Red))
                    _redBlockQuantity += block.Value;
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
            return 0;                          //is this ok?
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