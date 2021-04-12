using System;
using System.Collections.Generic;

namespace ToyBlockFactoryWebApp.DTOs
{
    public record OrderDTO(string OrderId, string Name, string Address, IEnumerable<BlockDTO> BlockList, DateTime DueDate)
    {
    }

}