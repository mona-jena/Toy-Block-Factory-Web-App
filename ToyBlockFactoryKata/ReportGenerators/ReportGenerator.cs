using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class ReportGenerator
    {
        private readonly IReportGenerator _cuttingReportGenerator = new TableReportGenerator(new QuantityTableFactory());
        private readonly IReportGenerator _invoiceReportGenerator;
        private readonly IReportGenerator _paintingReportGenerator = new TableReportGenerator(new ColourTableFactory());

        internal ReportGenerator(IInvoiceCalculationStrategy pricingCalculator)
        {
            _invoiceReportGenerator = new InvoiceReportGenerator(pricingCalculator, new ColourTableFactory());
        }

        internal IReport GenerateInvoice(Order requestedOrder)
        {
            return _invoiceReportGenerator.GenerateReport(ReportType.Invoice, requestedOrder);
        }

        internal IReport GenerateCuttingList(Order requestedOrder)
        {
            return _cuttingReportGenerator.GenerateReport(ReportType.CuttingList, requestedOrder);
        }

        internal IReport GeneratePaintingReport(Order requestedOrder)
        {
            return _paintingReportGenerator.GenerateReport(ReportType.Painting, requestedOrder);
        }
        
        internal IEnumerable<IReport> FilterCuttingReportsByDate(DateTime date, Dictionary<string, Order> orderRecords)
        {
            return orderRecords.Where(o => o.Value.DueDate == date).Select(o => GenerateCuttingList(o.Value));
        }

        internal IEnumerable<IReport> FilterPaintingReportsByDate(DateTime date, Dictionary<string, Order> orderRecords)
        {
            return orderRecords.Where(o => o.Value.DueDate == date).Select(o => GeneratePaintingReport(o.Value));
        }
        
    }
}