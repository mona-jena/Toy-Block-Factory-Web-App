using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class Order
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string OrderId { get; set; }  //if i make this init, can't set in OrderGenerator
        public Dictionary<Block, int> BlockList { get; } = new();
        public DateTime DueDate { get; private set; } = DateTime.Now;

        public void SetDueDate(DateTime dueDate)
        {
            //DueDate = DueDate.AddDays(7);
            DueDate = dueDate;
        }


        public void AddBlock(Shape shape, Colour colour)
        {
            var block = new Block(shape, colour);
            if (BlockList.TryGetValue(block, out var blockQuantity))
                BlockList[block] = ++blockQuantity;
            else
                BlockList.Add(block, 1);
        }
    }
}