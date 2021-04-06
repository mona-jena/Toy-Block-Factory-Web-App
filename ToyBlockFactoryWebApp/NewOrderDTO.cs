using System;

namespace ToyBlockFactoryWebApp
{
    internal record NewOrderDTO(string Name, string Address, DateTime? DueDate = null)
    {
    }
    
}