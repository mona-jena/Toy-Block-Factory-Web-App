using System;
using System.Linq;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryTests.TestDoubles;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class InvoiceReportTests
    {
        private readonly ToyBlockFactory _toyBlockFactory;

        public InvoiceReportTests()
        {
            _toyBlockFactory = new ToyBlockFactory(new LineItemsCalculatorStub());

            OrderWithAllShapesAndColoursIncluded();
            OrderWithNotAllVariationsIncluded();
        }


        [Fact]
        public void IsInvoice()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);

            Assert.Equal(ReportType.Invoice, invoice.ReportType);
        }


        private string _david = string.Empty;

        [Fact]
        public void ReportContainsCustomerName()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);

            Assert.Equal(_david, invoice.Name);
        }


        private string _davidAddress = string.Empty;
        
        [Fact]
        public void ReportContainsCustomerAddress()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);

            Assert.Equal(_davidAddress, invoice.Address);
        }

        private DateTime _davidOrderDueDate;
        [Fact]
        public void ReportContainsOrderDueDate()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);

            Assert.Equal(_davidOrderDueDate, invoice.DueDate);
        }


        [Fact]
        public void ReportContainsOrderId()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);

            Assert.Equal(orderId, invoice.OrderId);
        }


        [Theory]
        [InlineData("Square", 2, 1, 2)]
        [InlineData("Circle", 3, 3, 9)]
        [InlineData("Red colour surcharge", 1, 1, 1)]
        [InlineData("Triangle", 2, 2, 4)]
        public void LineItemsContainDetailsAboutEachOrderItem(string description, int quantity, decimal price, 
            decimal total)
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);
            var invoiceLine = ((InvoiceReport) invoice).LineItems.SingleOrDefault(l => l.Description == description);
            
            Assert.NotNull(invoiceLine);
            Assert.Equal(description, invoiceLine?.Description);
            Assert.Equal(quantity, invoiceLine?.Quantity);
            Assert.Equal(price, invoiceLine?.Price);
            Assert.Equal(total, invoiceLine?.Total);
        }

        [Theory]
        [InlineData("Square", 2, 1, 2)]
        [InlineData("Circle", 2, 3, 6)]
        public void OnlyItemsInOrderAreListedInLineItems(string description, int quantity, decimal price, decimal total)
        {
            const string orderId = "0002";
            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);

            var invoiceLine = ((InvoiceReport) invoice).LineItems.SingleOrDefault(l => l.Description == description);

            Assert.NotNull(invoiceLine);
            Assert.Equal(2, ((InvoiceReport) invoice).LineItems.Count); 
            Assert.Equal(description, invoiceLine?.Description);
            Assert.Equal(quantity, invoiceLine?.Quantity);
            Assert.Equal(price, invoiceLine?.Price);
            Assert.Equal(total, invoiceLine?.Total);
        }

        [Fact]
        public void ReportCalculatesOrderTotal()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);

            Assert.Equal(16.00m, ((InvoiceReport) invoice).Total);
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
            var invoice = _toyBlockFactory.GetReport(orderId, ReportType.Invoice);

            var tableRow = invoice.OrderTable.SingleOrDefault(l => l.Shape == shape);
            var tableColumn = tableRow?.TableColumn.SingleOrDefault(l => l.MeasuredItem == colour);

            Assert.NotNull(tableRow);
            Assert.NotNull(tableColumn);
            Assert.Equal(shape, tableRow?.Shape);
            Assert.Equal(colour, tableColumn?.MeasuredItem);
            Assert.Equal(quantity, tableColumn?.Quantity);
        }
        
        
        private void OrderWithAllShapesAndColoursIncluded()
        {
            _david = "David Rudd";
            _davidAddress = "1 Bob Avenue, Auckland";
            _davidOrderDueDate = new DateTime(2021, 1, 19);
            var davidOrder = _toyBlockFactory.CreateOrder(_david, _davidAddress, _davidOrderDueDate);
            davidOrder.AddBlock(Shape.Square, Colour.Red);
            davidOrder.AddBlock(Shape.Square, Colour.Yellow);
            davidOrder.AddBlock(Shape.Triangle, Colour.Blue);
            davidOrder.AddBlock(Shape.Triangle, Colour.Blue);
            davidOrder.AddBlock(Shape.Circle, Colour.Blue);
            davidOrder.AddBlock(Shape.Circle, Colour.Yellow);
            davidOrder.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(davidOrder);
        }

        private void OrderWithNotAllVariationsIncluded()
        {
            var steve = "Steve Richards";
            var steveAddress = "102 Robin Street, Auckland";
            var steveOrderDueDate = new DateTime(2021, 2, 15);
            var steveOrder = _toyBlockFactory.CreateOrder(steve, steveAddress, steveOrderDueDate);
            steveOrder.AddBlock(Shape.Square, Colour.Yellow);
            steveOrder.AddBlock(Shape.Square, Colour.Blue);
            steveOrder.AddBlock(Shape.Circle, Colour.Blue);
            steveOrder.AddBlock(Shape.Circle, Colour.Blue);
            _toyBlockFactory.SubmitOrder(steveOrder);
        }
        
    }
}







