using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.ReportGenerators
{
    public interface IReportGenerator
    {
        IReport GenerateReport(Order requestedOrder);
    }
}