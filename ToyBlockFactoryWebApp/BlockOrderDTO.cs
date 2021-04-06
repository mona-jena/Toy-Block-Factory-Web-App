using System.Collections.Generic;

namespace ToyBlockFactoryWebApp
{
    public partial class OrderController
    {
        private record BlockOrderDTO(List<BlockDTO> Order)
        {
        }
    }
}