using System;
using System.Linq;
using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class PaintingReportGeneratorTests
    {
        private readonly string _customerAddress;
        private readonly string _customerName;
        private readonly ToyBlockFactory _toyBlockFactory;

        public PaintingReportGeneratorTests()
        {
            _toyBlockFactory = new ToyBlockFactory(new TestPricingCalculator());

            _customerName = "David Rudd";
            _customerAddress = "1 Bob Avenue, Auckland";
            var dueDate = new DateTime(2019, 1, 19);
            var customerOrder = _toyBlockFactory.CreateOrder(_customerName, _customerAddress, dueDate);
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(customerOrder);

            var customer2Name = "James Sopo"; 
            var customer2Address = "34 Anzac Avenue, Auckland"; 
            var order2DueDate = new DateTime(2019, 1, 19);
            var customer2Order = _toyBlockFactory.CreateOrder(customer2Name, customer2Address, order2DueDate);
            customer2Order.AddBlock(Shape.Triangle, Colour.Red);
            customer2Order.AddBlock(Shape.Square, Colour.Yellow);
            customer2Order.AddBlock(Shape.Triangle, Colour.Blue);
            _toyBlockFactory.SubmitOrder(customer2Order);

            var customer3Name = "Alex Wright";
            var customer3Address = "101 South Road, Auckland";
            var order3DueDate = new DateTime(2020, 4, 19);
            var customer3Order = _toyBlockFactory.CreateOrder(customer3Name, customer3Address, order3DueDate);
            customer3Order.AddBlock(Shape.Triangle, Colour.Blue);
            customer3Order.AddBlock(Shape.Square, Colour.Yellow);
            customer3Order.AddBlock(Shape.Circle, Colour.Blue);
            customer3Order.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(customer3Order);

            var customer4Name = "Tom Night";
            var customer4Address = "23 Country Avenue, Hamilton";
            var order4DueDate = new DateTime(2019, 1, 19);
            var customer4Order = _toyBlockFactory.CreateOrder(customer4Name, customer4Address, order4DueDate);
            customer4Order.AddBlock(Shape.Circle, Colour.Yellow);
            customer4Order.AddBlock(Shape.Circle, Colour.Yellow);
            customer4Order.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(customer4Order);
        }

        [Fact]
        public void IsPaintingReport()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetPaintingReport(orderId);

            Assert.Equal(ReportType.Painting, paintingReport.ReportType);
        }

        [Fact]
        public void ReportContainsCustomerName()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetPaintingReport(orderId);

            Assert.Equal(_customerName, paintingReport.Name);
        }

        [Fact]
        public void ReportContainsCustomerAddress()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetPaintingReport(orderId);

            Assert.Equal(_customerAddress, paintingReport.Address);
        }

        [Fact]
        public void ReportContainsOrderDueDate()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetPaintingReport(orderId);

            Assert.Equal(new DateTime(2019, 1, 19), paintingReport.DueDate);
        }

        [Fact]
        public void ReportContainsOrderId()
        {
            const string orderId = "0001";

            var paintingReport = _toyBlockFactory.GetPaintingReport(orderId);

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
            var cuttingList = _toyBlockFactory.GetPaintingReport(orderId);

            var tableRow = cuttingList.OrderTable.SingleOrDefault(l => l.Shape == shape);
            var tableColumn = tableRow.TableColumn.SingleOrDefault(l => l.MeasuredItem == colour);

            Assert.NotNull(tableRow);
            Assert.NotNull(tableColumn);
            Assert.Equal(shape, tableRow.Shape);
            Assert.Equal(colour, tableColumn.MeasuredItem);
            Assert.Equal(quantity, tableColumn.Quantity);
        }

        [Fact]
        public void CanFilterReportsByDueDate()
        {
            var filteredReports = _toyBlockFactory.GetPaintingReportsByDate(new DateTime(2019, 1, 19)).ToList();

            Assert.Equal(3, filteredReports.Count);
            Assert.Equal("0001", filteredReports[0].OrderId);
            Assert.Equal(new DateTime(2019, 1, 19), filteredReports[0].DueDate);
            Assert.Equal("0002", filteredReports[1].OrderId);
            Assert.Equal(new DateTime(2019, 1, 19), filteredReports[1].DueDate);
            Assert.Equal("0004", filteredReports[2].OrderId);
            Assert.Equal(new DateTime(2019, 1, 19), filteredReports[2].DueDate);
        }
    }
}