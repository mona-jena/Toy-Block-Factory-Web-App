using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    internal class InvoiceReportGenerator : IReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _priceList; //change name 

        internal InvoiceReportGenerator(IInvoiceCalculationStrategy priceList) 
        {
            _priceList = priceList;
        }

        public IReport GenerateReport(Order requestedOrder) //Should sep setup and getting part of report?
        {
            var report = new InvoiceReport()
            {
                ReportType = ReportType.Invoice,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId
            };
            GenerateTable(report, requestedOrder);
            var lineItems = _priceList.GenerateLineItems(requestedOrder);  //RENAME
            report.LineItems.AddRange(lineItems);                  
            
            return report;
        }

        
        private void GenerateTable(InvoiceReport report, Order requestedOrder)
        {
            foreach (Shape shape in ShapesUsedInOrder(requestedOrder))
                report.OrderTable.Add(new TableRow(shape, RowItems(shape, requestedOrder)));
        }

        private IEnumerable<Shape> ShapesUsedInOrder(Order requestedOrder)
        {
            var shapesUsed = requestedOrder.BlockList.Keys.ToList();
            return shapesUsed.Select(item => item.Shape).Distinct();
        }

        private List<TableColumn> RowItems(Shape shape, Order requestedOrder)
        {
            var rowItemQuantities = new List<TableColumn>();
            foreach (Colour colour in requestedOrder.BlockList.Select(b=>b.Key.Colour).Distinct())
            {
                var block = new Block(shape, colour);
                if (requestedOrder.BlockList.ContainsKey(block))
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), requestedOrder.BlockList[block]));
                /*else
                {
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), 0));
                }*/
            }

            return rowItemQuantities;
        }

        
    }
}