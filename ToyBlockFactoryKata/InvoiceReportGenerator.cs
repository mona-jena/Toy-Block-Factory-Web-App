using System.Collections.Generic;
using System.Linq;

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
            // GenerateTable();
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
        }

        private int CalculateItemQuantity(Shape shape)
        {
            return _requestedOrder.BlockList.Where(b => b.Key.Shape == shape).Sum(b => b.Value);
        }

        private int CalculateItemQuantity(Colour colour)
        {
            return _requestedOrder.BlockList.Where(b => b.Key.Colour == colour).Sum(b => b.Value); 
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
            switch (colourType)          // should I do switch statement even though there is only 1 case now
            {
                case "Red":
                    return _priceList.Red;
                default:
                    return 0;                          //is this ok?
            }
        }

        /*private void GenerateTable()
        {
            foreach (var shape in GetShapesUsedInOrder())
            {
                _report.OrderTable.Add(new TableRow(
                    shape.ToString(),
                    CalculateItemQuantity(r, shape),
                    CalculateItemQuantity(colour, shape),
                    CalculateItemQuantity(colour, shape)
                ));
                foreach (var colour in GetColoursUsedInOrder())
                {
                   
                }
            }
        }
        
        private int CalculateItemQuantity(Colour colour, Shape shape)
        {
            var noOfItems = 0;
            foreach (var (block, quantity) in _requestedOrder.BlockList)
            {
                
            }
        }*/
    
    }
}