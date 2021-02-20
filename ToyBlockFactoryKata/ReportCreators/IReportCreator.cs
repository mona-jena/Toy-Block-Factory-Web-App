using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.ReportCreators
{
    internal interface IReportCreator
    {
        IReport GenerateReport(ReportType reportType, Order requestedOrder);
    }
}