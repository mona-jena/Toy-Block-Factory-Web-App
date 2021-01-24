using System;
using System.Collections.Generic;
using System.Diagnostics;
using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class ReportManagementTests : IClassFixture<ToyBlockFactoryFixture>
    {
        private readonly ToyBlockFactoryFixture _toyBlockFactoryFixture;
        private readonly string _customerName;
        private readonly string _customerAddress;
        public ReportManagementTests(ToyBlockFactoryFixture toyBlockFactoryFixture)
        {
            _customerName = "David Rudd";
            _customerAddress = "1 Bob Avenue, Auckland";
            _toyBlockFactoryFixture = toyBlockFactoryFixture;
            var customerOrder = _toyBlockFactoryFixture.Factory
                .CreateOrder(_customerName, _customerAddress);
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.DueDate = "19 Jan 2019";
            _toyBlockFactoryFixture.Factory.SubmitOrder(customerOrder);
        }
        
        [Fact]
        public void GenerateInvoiceShouldReturnInvoiceDataForAParticularOrder()
        {
            const string orderId = "0001";
            var invoiceLine = new List<object>
            {
                new List<object>() {"Squares", 2, 1, 2},
                new List<object>() {"Triangles", 2, 2, 4},
                new List<object>() {"Circles", 3, 3, 9},
                new List<object>() {"Red colour surcharge", 1, 1, 1}
            };
            
            var invoice = _toyBlockFactoryFixture.Factory.GetInvoiceReport(orderId);
            
            Assert.Equal(ReportType.Invoice, invoice.ReportType); 
            Assert.Equal(_customerName, invoice.Name);
            Assert.Equal(_customerAddress, invoice.Address);
            Assert.Equal(new DateTime(2019, 1, 19, invoice.DueDate)); 
            Assert.Equal(orderId, invoice.OrderId);
            Assert.Equal(invoiceLine[0], invoice._report.LineItems[0]);
            Assert.Equal(invoiceLine[1], invoice._report.LineItems[1]);
            Assert.Equal(invoiceLine[2], invoice._report.LineItems[2]);
            Assert.Equal(invoiceLine[3], invoice._report.LineItems[3]);
        }
    }
    /*
     * WRITE TESTS FOR:
     * test if each item in order is in invoice 
     */
}

/*var expectedInvoiceReport = new List<string>()
{
    "Your invoice report has been generated:",
    "\n",
    "Name: David Rudd ",
    "Address: 1 Bob Avenue, Auckland ",
    "Due Date: 19 Jan 2019 ",
    "Order #: 0001,",
    "\n",
    "|          | Red | Blue | Yellow |",
    "|----------|-----|------|--------|",
    "| Square   | 1   | -    | 1      |",
    "| Triangle | -   | 2    | -      |",
    "| Circle   | -   | 1    | 2      |",
    "\n",
    "Squares 		        2 @ $1 ppi = $2",
    "Triangles		        2 @ $2 ppi = $4",
    "Circles			    3 @ $3 ppi = $9",
    "Red colour surcharge   1 @ $1 ppi = $1",
    "\n",
    "Total                  $16"
};*/



//string[,] invoiceLine2 = new string[Enum.GetNames(typeof(Colour)).Length,Enum.GetNames(typeof(Shape)).Length];
/*string[,] invoiceLine = new string[4, 4];
invoiceLine[1, 0] = "Red";
invoiceLine[2, 0] = "Blue";
invoiceLine[3, 0] = "Yellow";
invoiceLine[0, 1] = "Square";
invoiceLine[1, 1] = "1";
invoiceLine[2, 1] = "-";
invoiceLine[3, 1] = "1";
invoiceLine[0, 2] = "Triangle";
invoiceLine[1, 2] = "-";
invoiceLine[2, 2] = "2";
invoiceLine[3, 2] = "-";
invoiceLine[0, 3] = "Circle";
invoiceLine[1, 3] = "-";
invoiceLine[2, 3] = "1";
invoiceLine[3, 3] = "2";*/