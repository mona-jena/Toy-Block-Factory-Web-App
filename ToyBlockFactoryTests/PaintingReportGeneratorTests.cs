using System;
using System.Linq;
using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class PaintingReportGeneratorTests
    {
        private readonly ToyBlockFactory _toyBlockFactory;
        private readonly string _customerName;
        private readonly string _customerAddress;

        public PaintingReportGeneratorTests()
        {
            _toyBlockFactory = new ToyBlockFactory();
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
        
    }
}