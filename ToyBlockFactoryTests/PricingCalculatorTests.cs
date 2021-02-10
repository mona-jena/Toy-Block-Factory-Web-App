using System;
using System.Linq;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class PricingCalculatorTests
    {
        private readonly Order _customerOrder;
        private readonly PricingCalculator _pricingCalculator;

        public PricingCalculatorTests()
        {
            _pricingCalculator = new PricingCalculator();
            var toyBlockFactory = new ToyBlockFactory(_pricingCalculator);
            var orderDueDate = new DateTime(2021, 1, 19);
            _customerOrder = toyBlockFactory
                .CreateOrder("David Rudd", "1 Bob Avenue, Auckland", orderDueDate);
            _customerOrder.AddBlock(Shape.Square, Colour.Red);
            _customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            _customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            _customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            _customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            _customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            _customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            toyBlockFactory.SubmitOrder(_customerOrder);
        }


        [Theory]
        [InlineData("Square", 2, 1, 2)]
        [InlineData("Triangle", 2, 2, 4)]
        [InlineData("Circle", 3, 3, 9)]
        [InlineData("Red colour surcharge", 1, 1, 1)]
        public void CanCalculateAndGenerateInvoiceLines(string description, int quantity, decimal price, decimal total)
        {
            var invoiceLine = _pricingCalculator.GenerateLineItems(_customerOrder)
                .SingleOrDefault(l => l.Description == description);

            Assert.NotNull(invoiceLine);
            Assert.Equal(description, invoiceLine.Description);
            Assert.Equal(quantity, invoiceLine.Quantity);
            Assert.Equal(price, invoiceLine.Price);
            Assert.Equal(total, invoiceLine.Total);
        }
    }
}