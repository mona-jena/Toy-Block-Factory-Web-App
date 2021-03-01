using System;
using System.Globalization;

namespace ToyBlockFactoryConsole
{
    internal static class InputValidator
    {
        
        internal static DateTime ConvertToDateTime(string dateInput)
        {
            if (DateTime.TryParse(dateInput, new CultureInfo("en-NZ"), DateTimeStyles.AssumeLocal, out var dueDate))
                return dueDate;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid date! Please enter again (eg 19 Jan 2021): ");
            Console.ResetColor();

            dateInput = Console.ReadLine();
            return ConvertToDateTime(dateInput);
        }

        internal static int IfValidInteger(string input)
        {
            if (int.TryParse(input, out var num)) 
                return num;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid integer! Please enter again: ");
            Console.ResetColor();

            input = Console.ReadLine();
            IfValidInteger(input);

            return num;
        }
    }
}