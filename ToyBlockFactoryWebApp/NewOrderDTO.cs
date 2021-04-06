using System;

namespace ToyBlockFactoryWebApp
{
    public partial class OrderController
    {
        private record NewOrderDTO(string Name, string Address, DateTime? DueDate = null)
        {
        }
    }
}