using System;
using System.Linq;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryTests.TestDoubles;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class PaintingReportTests
    {
        private readonly ToyBlockFactory _toyBlockFactory;

        public PaintingReportTests()
        {
            _toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());

            CreateListOfOrders();
        }


        [Fact]
        public void IsPaintingReport()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetReport(orderId, ReportType.Painting);

            Assert.Equal(ReportType.Painting, paintingReport.ReportType);
        }

        private string _david = string.Empty;
        
        [Fact]
        public void ReportContainsCustomerName()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetReport(orderId, ReportType.Painting);

            Assert.Equal(_david, paintingReport.Name);
        }

        private string _davidAddress = string.Empty;
        
        [Fact]
        public void ReportContainsCustomerAddress()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetReport(orderId, ReportType.Painting);

            Assert.Equal(_davidAddress, paintingReport.Address);
        }

        [Fact]
        public void ReportContainsOrderDueDate()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetReport(orderId, ReportType.Painting);

            Assert.Equal(new DateTime(2021, 1, 19), paintingReport.DueDate);
        }

        [Fact]
        public void ReportContainsOrderId()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetReport(orderId, ReportType.Painting);

            Assert.Equal(orderId, paintingReport.OrderId);
        }

        [Theory]
        [InlineData(Shape.Square, "Red", 1)]
        [InlineData(Shape.Square, "Yellow", 1)]
        [InlineData(Shape.Triangle, "Blue", 2)]
        [InlineData(Shape.Circle, "Blue", 1)]
        [InlineData(Shape.Circle, "Yellow", 2)]
        public void ReportGeneratesOrderTable(Shape shape, string colour, int quantity)
        {
            const string orderId = "0001";
            var cuttingList = _toyBlockFactory.GetReport(orderId, ReportType.Painting);

            var tableRow = cuttingList.OrderTable.SingleOrDefault(l => l.Shape == shape);
            var tableColumn = tableRow?.TableColumn.SingleOrDefault(l => l.MeasuredItem == colour);

            Assert.NotNull(tableRow);
            Assert.NotNull(tableColumn);
            Assert.Equal(shape, tableRow?.Shape);
            Assert.Equal(colour, tableColumn?.MeasuredItem);
            Assert.Equal(quantity, tableColumn?.Quantity);
        }

        [Fact]
        public void CanFilterReportsByDueDate()
        {
            var filteredReports = _toyBlockFactory.GetReportsByDate(new DateTime(2021, 1, 19), ReportType.Painting).ToList();

            Assert.Equal(3, filteredReports.Count);
            Assert.Equal("0001", filteredReports[0].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[0].DueDate);
            Assert.Equal("0002", filteredReports[1].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[1].DueDate);
            Assert.Equal("0004", filteredReports[2].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[2].DueDate);
        }
        
        
        private void CreateListOfOrders()
        {
            _david = "David Rudd";
            _davidAddress = "1 Bob Avenue, Auckland";
            var davidOrderDueDate = new DateTime(2021, 1, 19);
            var davidOrder = _toyBlockFactory.CreateOrder(_david, _davidAddress, davidOrderDueDate);
            davidOrder.AddBlock(Shape.Square, Colour.Red, 1);
            davidOrder.AddBlock(Shape.Square, Colour.Yellow, 1);
            davidOrder.AddBlock(Shape.Triangle, Colour.Blue, 1);
            davidOrder.AddBlock(Shape.Triangle, Colour.Blue, 1);
            davidOrder.AddBlock(Shape.Circle, Colour.Blue, 1);
            davidOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            davidOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            _toyBlockFactory.SubmitOrder(davidOrder);

            var james = "James Soho"; 
            var jamesAddress = "34 Anzac Avenue, Auckland"; 
            var jamesOrderDueDate = new DateTime(2021, 1, 19);
            var jamesOrder = _toyBlockFactory.CreateOrder(james, jamesAddress, jamesOrderDueDate);
            jamesOrder.AddBlock(Shape.Triangle, Colour.Red, 1);
            jamesOrder.AddBlock(Shape.Square, Colour.Yellow, 1);
            jamesOrder.AddBlock(Shape.Triangle, Colour.Blue, 1);
            _toyBlockFactory.SubmitOrder(jamesOrder);

            var alex = "Alex Wright";
            var alexAddress = "101 South Road, Auckland";
            var alexOrderDueDate = new DateTime(2021, 4, 21);
            var alexOrder = _toyBlockFactory.CreateOrder(alex, alexAddress, alexOrderDueDate);
            alexOrder.AddBlock(Shape.Triangle, Colour.Blue, 1);
            alexOrder.AddBlock(Shape.Square, Colour.Yellow, 1);
            alexOrder.AddBlock(Shape.Circle, Colour.Blue, 1);
            alexOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            _toyBlockFactory.SubmitOrder(alexOrder);

            var tom = "Tom Night";
            var tomAddress = "23 Country Avenue, Hamilton";
            var tomOrderDueDate = new DateTime(2021, 1, 19);
            var tomOrder = _toyBlockFactory.CreateOrder(tom, tomAddress, tomOrderDueDate);
            tomOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            tomOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            tomOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            _toyBlockFactory.SubmitOrder(tomOrder);
        }
    }
}