using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;

namespace ToyBlockFactoryTests
{
    public class ToyBlockFactoryFixture
    {
        public static ToyBlockFactory create()
        {
            return createWithShape(Shape.Circle);
        }

        public static ToyBlockFactory createWithShape(Shape shape)
        {
            ToyBlockFactory factory = new ToyBlockFactory(new LineItemsCalculator());
            var order = factory.CreateOrder("some name", "some address");
            order.AddBlock(shape, Colour.Blue);
            factory.SubmitOrder(order);
            return factory;
        }
        
    }
}