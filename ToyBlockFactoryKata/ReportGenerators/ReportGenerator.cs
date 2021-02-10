using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class ReportGenerator
    {
        private readonly IReportGenerator _cuttingReportGenerator = new CuttingListReportGenerator();
        private readonly IReportGenerator _invoiceReportGenerator;
        private readonly IReportGenerator _paintingReportGenerator = new PaintingReportGenerator();

        public ReportGenerator(IInvoiceCalculationStrategy pricingCalculator)
        {
            _invoiceReportGenerator = new InvoiceReportGenerator(pricingCalculator);
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
        public IEnumerable<IReport> FilterCuttingReportsByDate(DateTime date, Dictionary<string, Order> orderRecords)
        {
            return orderRecords.Where(o => o.Value.DueDate == date).Select(o => GenerateCuttingList(o.Value));
        }

        public IEnumerable<IReport> FilterPaintingReportsByDate(DateTime date, Dictionary<string, Order> orderRecords)
        {
            return orderRecords.Where(o => o.Value.DueDate == date).Select(o => GeneratePaintingReport(o.Value));
        }
    }
}