using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    internal class InvoiceReportGenerator : IReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _priceList;
        private readonly Report _report = new(); //when does this get created in the program??
        private Order _requestedOrder;

        internal InvoiceReportGenerator(IInvoiceCalculationStrategy priceList)
        {
            _priceList = priceList;
        }

        public Report InputOrderDetails(Order requestedOrder) //Should sep setup and getting part of report?
        {
            _requestedOrder = requestedOrder;
            _report.ReportType = ReportType.Invoice;
            _report.Name = requestedOrder.Name;
            _report.Address = requestedOrder.Address;
            _report.DueDate = requestedOrder.DueDate;
            _report.OrderId = requestedOrder.OrderId;
            GenerateTable();
            _priceList.AddLineItems(_report, _requestedOrder);
            return _report;
        }
        
        private void GenerateTable()
        {
            // how to write in LINQ??
            foreach (Shape shape in Enum.GetValues(typeof(Shape)))
                _report.OrderTable.Add(new TableRow(shape, RowItems(shape)));
        }

        private List<TableColumn> RowItems(Shape shape)
        {
            var rowItemQuantities = new List<TableColumn>();
            foreach (Colour colour in Enum.GetValues(typeof(Colour)))
            {
                var block = new Block(shape, colour);
                if (_requestedOrder.BlockList.ContainsKey(block))
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), _requestedOrder.BlockList[block]));
            }

            return rowItemQuantities;
        }
    }
}