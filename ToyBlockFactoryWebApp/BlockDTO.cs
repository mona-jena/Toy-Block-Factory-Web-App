using ToyBlockFactoryKata.Orders;

namespace ToyBlockFactoryWebApp
{
    public partial class OrderController
    {
        private record BlockDTO(Colour Colour, Shape Shape, int Quantity)
        {
        }
    }
}