using System;
using System.Collections.Generic;
using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class ReportManagementTests 
    {
        private readonly string _customerName;
        private readonly string _customerAddress;
        private readonly ToyBlockFactory _toyBlockFactory;

        public ReportManagementTests()
        {
            _toyBlockFactory = new ToyBlockFactory();
            _customerName = "David Rudd";                                                                                                                             
            _customerAddress = "1 Bob Avenue, Auckland";
            var customerOrder = _toyBlockFactory.CreateOrder(_customerName, _customerAddress);
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.DueDate = new DateTime(2019, 1, 19);
            _toyBlockFactory.SubmitOrder(customerOrder);
        }
        
        
        [Fact]
        public void GenerateInvoiceShouldReturnInvoiceDataForAParticularOrder()
        {
            const string orderId = "0001";
            var invoiceLines = new List<InvoiceLine>
            {
                new("Square", 2, 1),
                new("Triangle", 2, 2),
                new("Circle", 3, 3),
                new("Red colour surcharge", 1, 1)
            };
            
            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            Assert.Equal(ReportType.Invoice, invoice.ReportType); 
            Assert.Equal(_customerName, invoice.Name);
            Assert.Equal(_customerAddress, invoice.Address);
            Assert.Equal(new DateTime(2019, 1, 19), invoice.DueDate); 
            Assert.Equal(orderId, invoice.OrderId);
            Assert.Equal(invoiceLines[0], invoice.LineItems[0]);
            Assert.Equal(invoiceLines[1], invoice.LineItems[1]);
            Assert.Equal(invoiceLines[2], invoice.LineItems[2]);
            Assert.Equal(invoiceLines[3], invoice.LineItems[3]);
        }


        /*[Fact]
        public void GenerateReportShouldReturnOrderTable()
        {
            string[,] orderTable = new string[4, 4];
            orderTable[0, 0] = "";
            orderTable[1, 0] = "Red";
            orderTable[2, 0] = "Blue";
            orderTable[3, 0] = "Yellow";
            orderTable[0, 1] = "Square";
            orderTable[1, 1] = "1";
            orderTable[2, 1] = "-";
            orderTable[3, 1] = "1";
            orderTable[0, 2] = "Triangle";
            orderTable[1, 2] = "-";
            orderTable[2, 2] = "2";
            orderTable[3, 2] = "-";
            orderTable[0, 3] = "Circle";
            orderTable[1, 3] = "-";
            orderTable[2, 3] = "1";
            orderTable[3, 3] = "2";
                
            var invoice = _toyBlockFactory.GetInvoiceReport("0001");

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    Assert.Equal(orderTable[i, j], invoice.OrderTable[i, j]);
                }
            }
        }*/
    }
   
    
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