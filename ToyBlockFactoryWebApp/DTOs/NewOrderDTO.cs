using System;

namespace ToyBlockFactoryWebApp.DTOs
{
    internal record NewOrderDTO(string Name, string Address, DateTime? DueDate = null)
    {
    }
    
}