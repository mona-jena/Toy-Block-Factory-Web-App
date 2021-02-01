using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    internal class InvoiceReportGenerator 
    {
        private readonly IInvoiceCalculationStrategy _priceList;
        private readonly Report _report = new(); //when does this get created in the program??

        internal InvoiceReportGenerator(IInvoiceCalculationStrategy priceList)
        {
            _priceList = priceList;
        }

        public Report InputOrderDetails(Order requestedOrder) //Should sep setup and getting part of report?
        {
            _report.ReportType = ReportType.Invoice;
            _report.Name = requestedOrder.Name;
            _report.Address = requestedOrder.Address;
            _report.DueDate = requestedOrder.DueDate;
            _report.OrderId = requestedOrder.OrderId;
            GenerateTable(requestedOrder);
            AddLineItems(requestedOrder);
            return _report;
        }

        private void AddLineItems(Order requestedOrder)
        {
            foreach (var shape in GetShapesUsedInOrder(requestedOrder))
            {
                var shapeQuantity = CalculateItemQuantity(shape, requestedOrder);
                var shapePrice = _priceList.GetPrice(shape.ToString());
                _report.LineItems.Add(new InvoiceLine(
                    shape.ToString(),
                    shapeQuantity,
                    shapePrice
                ));
            }

            foreach (var colour in GetSurchargeItems(requestedOrder)) //item or colour??
            {
                var itemQuantity = CalculateItemQuantity(colour, requestedOrder);
                var colourPrice = _priceList.GetPrice(colour.ToString());
                _report.LineItems.Add(new InvoiceLine(
                    colour + " colour surcharge",
                    itemQuantity,
                    colourPrice
                ));
            }
        }

        private IEnumerable<Shape> GetShapesUsedInOrder(Order requestedOrder)
        {
            var shapesUsed = requestedOrder.BlockList.Keys;
            return shapesUsed.Select(item => item.Shape).Distinct();
        }

        private IEnumerable<Colour> GetSurchargeItems(Order requestedOrder)
        {
            var coloursUsed = requestedOrder.BlockList.Keys;
            return coloursUsed.Select(item => item.Colour).Where(item => item == Colour.Red).Distinct();
            
        }

        private int CalculateItemQuantity(Shape shape, Order requestedOrder)
        {
            return requestedOrder.BlockList.Where(b => b.Key.Shape == shape).Sum(b => b.Value);
        }

        private int CalculateItemQuantity(Colour colour, Order requestedOrder)
        {
            return requestedOrder.BlockList.Where(b => b.Key.Colour == colour).Sum(b => b.Value);
        }

        private void GenerateTable(Order requestedOrder)
        {
            // how to write in LINQ??
            foreach (Shape shape in Enum.GetValues(typeof(Shape)))
                _report.OrderTable.Add(new TableRow(shape, RowItems(shape, requestedOrder)));
        }

        private List<TableColumn> RowItems(Shape shape, Order requestedOrder)
        {
            var rowItemQuantities = new List<TableColumn>();
            foreach (Colour colour in Enum.GetValues(typeof(Colour)))
            {
                var block = new Block(shape, colour);
                if (requestedOrder.BlockList.ContainsKey(block))
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), requestedOrder.BlockList[block]));
            }

            return rowItemQuantities;
        }
    }
}