using System;

namespace ToyBlockFactoryConsole
{
    static class InputValidator
    {
        internal static string IfEmptyInput(string input)
        {
            while (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("This field can't be empty! Please enter again: ");
                Console.ResetColor();

                input = Console.ReadLine();
            }

            return input;
        }

        internal static DateTime ConvertToDateTime(string dateInput)
        {
            if (DateTime.TryParse(dateInput, out var dueDate))
            {
                return dueDate;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid date! Please enter again (eg 19 Jan 2021): ");
            Console.ResetColor();

            dateInput = Console.ReadLine();
            ConvertToDateTime(dateInput);

            return dueDate;
        }

        internal static int IfValidInteger(string input)
        {
            if (int.TryParse(input, out var num)) return num;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid integer! Please enter again: ");
            Console.ResetColor();

            input = Console.ReadLine();
            IfValidInteger(input);

            return num;
        }
    }
}