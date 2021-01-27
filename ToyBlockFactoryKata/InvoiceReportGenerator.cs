using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace ToyBlockFactoryKata
{
    internal class InvoiceReportGenerator : IReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _priceList;
        private readonly Order _requestedOrder;
        private readonly Report _report = new(); //when does this get created in the program??
        
        internal InvoiceReportGenerator(IInvoiceCalculationStrategy priceList, Order requestedOrder)
        {
            _priceList = priceList;
            _requestedOrder = requestedOrder;
        }

        public Report InputOrderDetails() //Should sep setup and getting part of report?
        {
            _report.ReportType = ReportType.Invoice;
            _report.Name = _requestedOrder.Name;
            _report.Address = _requestedOrder.Address;
            _report.DueDate = _requestedOrder.DueDate; 
            _report.OrderId = _requestedOrder.OrderId;
            AddLineItems();
            return _report;
        }

        private void AddLineItems() 
        {
            foreach (var shape in GetShapesUsedInOrder())
            {
                var shapeQuantity = CalculateItemQuantity(shape);
                var shapePrice = GetPrice(shape);
                _report.LineItems.Add(new InvoiceLine(
                    shape.ToString(),
                    shapeQuantity,
                    shapePrice
                ));
            }

            foreach (var colour in GetSurchargeItems())    //item or colour??
            {
                var itemQuantity = CalculateItemQuantity(colour);
                var colourPrice = GetPrice(colour);
                _report.LineItems.Add(new InvoiceLine(
                    colour + " colour surcharge",
                    itemQuantity,
                    colourPrice
                ));
            }
        }
        
        private IEnumerable<Shape> GetShapesUsedInOrder()
        {
            var itemsUsed = _requestedOrder.BlockList.Keys.ToList();
            return itemsUsed.Select(item => item.Shape).Distinct();
        }

        private IEnumerable<Colour> GetSurchargeItems()
        {
            // var properties = typeof(IInvoiceCalculationStrategy).GetProperties().ToList();
            // var propertiesAsStrings = properties.Select(property => property.ToString()).ToList();
            var coloursUsed = _requestedOrder.BlockList.Keys.ToList();
            var distinctColours = coloursUsed.Select(item => item.Colour).Distinct();
           
            var surchargeItems = new List<Colour>();
            foreach (var colour in distinctColours)
            {
                if (colour == Colour.Red)
                {
                    surchargeItems.Add(Colour.Red);
                }
                
                //add new colour charge conditions here
            }

            return surchargeItems;
            //return distinctColours.Where(colour => propertiesAsStrings.Contains(colour.ToString())).ToList();
        }

        private int CalculateItemQuantity(Shape shape)
        {
            var shapeQuantity = 0;
            foreach (var (block, quantity) in _requestedOrder.BlockList)
            {
                if (block.Shape.Equals(shape))
                    shapeQuantity += quantity;
            }

            return shapeQuantity;
        }

        private int CalculateItemQuantity(Colour colour)
        {
            var noOfRedItems = 0;
            foreach (var (block, quantity) in _requestedOrder.BlockList)
            {
                if (block.Colour.Equals(colour))
                    noOfRedItems += quantity;
            }

            return noOfRedItems;
        }
        

        private int GetPrice(Shape shape)
        {
            var shapeType = shape.ToString();
            switch (shapeType)
            {
                case "Square":
                    return _priceList.Square;
                case "Triangle":
                    return _priceList.Triangle;
                case "Circle":
                    return _priceList.Circle;
                default:
                    return 0;                          //is this ok?
            }
        }
        
        private int GetPrice(Colour colour)
        {
            var colourType = colour.ToString();
            switch (colourType)
            {
                case "Red":
                    return _priceList.Red;
                default:
                    return 0;                          //is this ok?
            }
        }
    
    }
}