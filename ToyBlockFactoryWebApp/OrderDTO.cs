using System;
using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;

namespace ToyBlockFactoryWebApp
{
    public record OrderDTO(string OrderId, string Name, string Address, IEnumerable<BlockDTO> BlockList, DateTime DueDate)
    {
    }

}