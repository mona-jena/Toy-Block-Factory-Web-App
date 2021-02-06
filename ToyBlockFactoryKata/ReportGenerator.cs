using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    internal class ReportGenerator
    {
        private readonly IReportGenerator _invoiceReportGenerator;
        private readonly IReportGenerator _cuttingReportGenerator = new CuttingListReportGenerator();
        private readonly IReportGenerator _paintingReportGenerator = new PaintingReportGenerator();

        public ReportGenerator(IInvoiceCalculationStrategy priceCalculator)
        {
            _invoiceReportGenerator = new InvoiceReportGenerator(priceCalculator);
        }

        internal IReport GenerateInvoice(Order requestedOrder) 
        {
            return _invoiceReportGenerator.GenerateReport(requestedOrder);
        }

        internal IReport GenerateCuttingList(Order requestedOrder)
        {
            return _cuttingReportGenerator.GenerateReport(requestedOrder);
        }

        internal IReport GeneratePaintingReport(Order requestedOrder)
        {
            return _paintingReportGenerator.GenerateReport(requestedOrder);
        }
                                //GenerateCuttingReportsByDueDate????
        public List<IReport> FilterCuttingReportsByDate(DateTime date, Dictionary<string,Order> orderRecords)
        {
            List<IReport> filteredReports = new List<IReport>();
            foreach (var order in orderRecords.Values)
            {
                if (order.DueDate == date)
                    filteredReports.Add(GenerateCuttingList(order));
            }

            return filteredReports;
        }

        public List<IReport> FilterPaintingReportsByDate(DateTime date, Dictionary<string,Order> orderRecords)
        {
            List<IReport> filteredReports = new List<IReport>();
            foreach (var order in orderRecords.Values)
            {
                if (order.DueDate == date)
                    filteredReports.Add(GeneratePaintingReport(order));
            }

            return filteredReports;
        }
    }

    
}