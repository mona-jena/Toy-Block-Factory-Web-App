using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class ReportManagementTests : IClassFixture<ToyBlockFactoryFixture>
    {
    
        private readonly ToyBlockFactoryFixture _toyBlockFactoryFixture;

        public ReportManagementTests(ToyBlockFactoryFixture toyBlockFactoryFixture)
        {
            _toyBlockFactoryFixture = toyBlockFactoryFixture;
            var customerOrder = _toyBlockFactoryFixture.Factory
                .CreateOrder("David Rudd", "1 Bob Avenue, Auckland");
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
        public void GenerateInvoiceShouldReturnAnInvoiceReportForAParticularOrder()
        {
            var order = _toyBlockFactoryFixture.GetOrder("0001"); 
            
            var invoiceReport = _toyBlockFactoryFixture.GetInvoiceReport(order);
            var expectedInvoiceReport = 
                "Your invoice report has been generated:" +
                "\n" +
                "Name: David Rudd Address: 1 Bob Avenue, Auckland Due Date: 19 Jan 2019 Order #: 0001" +
                "\n" +
                "|          | Red | Blue | Yellow |" +
                "|----------|-----|------|--------|" +
                "| Square   | 1   | -    | 1      |" +
                "| Triangle | -   | 2    | -      |" +
                "| Circle   | -   | 1    | 2      |" +
                "\n" +
                "Squares 		        2 @ $1 ppi = $2" +
                "Triangles		        2 @ $2 ppi = $4" +
                "Circles			    3 @ $3 ppi = $9" +
                "Red colour surcharge   1 @ $1 ppi = $1" +
                "\n" +
                "Total                  $16";
            
            Assert.Equal(expectedInvoiceReport, invoiceReport);

        }
    }
}