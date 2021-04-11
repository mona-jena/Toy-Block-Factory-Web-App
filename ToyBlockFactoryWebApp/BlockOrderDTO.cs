using System.Collections.Generic;

namespace ToyBlockFactoryWebApp
{
    internal record BlockOrderDTO(IEnumerable<BlockDTO> Order)
    {
    }
    
}