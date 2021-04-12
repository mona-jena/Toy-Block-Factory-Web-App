using System.Collections.Generic;

namespace ToyBlockFactoryWebApp.DTOs
{
    public record BlockOrderDTO(IEnumerable<BlockDTO> Order)
    {
    }
    
}