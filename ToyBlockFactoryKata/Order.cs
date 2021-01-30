using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class Order
    {
        public string Name { get; init; }
        public string Address { get; init; }

        //public DateTime DueDate { get => _dueDate; set => _dueDate = Convert.ToDateTime(value); }
        //private string _date;

        /*private static DateTime _dueDate;
        public string DueDate
        {
            get => _dueDate;
            set
            {
                var validDate = DateTime.TryParse(value, out string date);
                if (validDate)
                {
                    _dueDate = date;
                }
            }
        }*/

        public DateTime DueDate { get; set; }
        public string OrderId { get; set; }
        public Dictionary<Block, int> BlockList { get; } = new();

        public void SetDueDate(string dueDate)
        {
            var validDate = DateTime.TryParse(dueDate, out var date);
            if (validDate) DueDate = date;
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