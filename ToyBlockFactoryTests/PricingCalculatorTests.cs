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
        private Order _customerOrder;
        private readonly PricingCalculator _pricingCalculator;

        public PricingCalculatorTests()
        {
            _pricingCalculator = new PricingCalculator();
            var toyBlockFactory = new ToyBlockFactory(_pricingCalculator);

            _customerOrder = CustomerOrder(toyBlockFactory);
        }

        [Fact]
        public void ContainsLineItemDescription()
        {
            var invoiceLine = _pricingCalculator.GenerateLineItems(_customerOrder).ToList();

            Assert.Equal("Square", invoiceLine[0].Description);
            Assert.Equal("Triangle", invoiceLine[1].Description);
            Assert.Equal("Circle", invoiceLine[2].Description);
            Assert.Equal("Red colour surcharge", invoiceLine[3].Description);
        }
        
        [Fact]
        public void ContainsLineItemQuantity()
        {
            var invoiceLine = _pricingCalculator.GenerateLineItems(_customerOrder).ToList();

            Assert.Equal(2, invoiceLine[0].Quantity);
            Assert.Equal(2, invoiceLine[1].Quantity);
            Assert.Equal(3, invoiceLine[2].Quantity);
            Assert.Equal(1, invoiceLine[3].Quantity);
        }
        
        [Fact]
        public void ContainsLineItemPrice()
        {
            var invoiceLine = _pricingCalculator.GenerateLineItems(_customerOrder).ToList();

            Assert.Equal(1, invoiceLine[0].Price);
            Assert.Equal(2, invoiceLine[1].Price);
            Assert.Equal(3, invoiceLine[2].Price);
            Assert.Equal(1, invoiceLine[3].Price);
        }
        
        [Fact]
        public void CalculatesLineItemTotal()
        {
            var invoiceLine = _pricingCalculator.GenerateLineItems(_customerOrder).ToList();

            Assert.Equal(2, invoiceLine[0].Total);
            Assert.Equal(4, invoiceLine[1].Total);
            Assert.Equal(9, invoiceLine[2].Total);
            Assert.Equal(1, invoiceLine[3].Total);
        }
       
        
        private Order CustomerOrder(ToyBlockFactory toyBlockFactory)
        {
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
            return _customerOrder;
        }
    }
}