using System;
using System.Reflection.Metadata.Ecma335;

namespace ToyBlockFactoryConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Toy Block Factory!\n");
            
            Console.Write("Please input your Name: ");              
            var name = Console.ReadLine();
            name = CheckForEmptyInput(name);
            
            Console.Write("Please input your Address: ");   
            var address = Console.ReadLine();
            address = CheckForEmptyInput(address);
           
            Console.Write("Please input your Due Date: ");       
            var dateInput = Console.ReadLine();
            DateTime dueDate;
            if (!string.IsNullOrEmpty(dateInput))
                dueDate = ConvertToDateTime(dateInput);

            Console.WriteLine();
            
            Console.Write("Please input the number of Red Squares 🟥: ");     
            var redSquareInput = Console.ReadLine();
            int redSquares;
            if (!string.IsNullOrEmpty(redSquareInput))
                redSquares = IfValidInteger(redSquareInput);

            Console.Write("Please input the number of Blue Squares 🟦: ");   
            var blueSquareInput = Console.ReadLine();
            int blueSquares;
            if (!string.IsNullOrEmpty(blueSquareInput))
                blueSquares = IfValidInteger(blueSquareInput);
                
            Console.Write("Please input the number of Yellow Squares 🟨: ");     
            var yellowSquareInput = Console.ReadLine();
            int yellowSquares;
            if (!string.IsNullOrEmpty(yellowSquareInput))
                yellowSquares = IfValidInteger(yellowSquareInput);
            
            Console.WriteLine();

            Console.Write("Please input the number of Red Triangles 🔺: ");      
            var redTriangleInput = Console.ReadLine();
            int redTriangles;
            if (!string.IsNullOrEmpty(redTriangleInput))
                redTriangles = IfValidInteger(redTriangleInput);
            
            Console.Write("Please input the number of Blue Triangles : ");      
            var blueTriangleInput = Console.ReadLine();
            int blueTriangles;
            if (!string.IsNullOrEmpty(blueTriangleInput))
                blueTriangles = IfValidInteger(blueTriangleInput);
            
            Console.Write("Please input the number of Yellow Triangles: ");    
            var yellowTriangleInput = Console.ReadLine();
            int yellowTriangles;
            if (!string.IsNullOrEmpty(yellowTriangleInput))
                yellowTriangles = IfValidInteger(yellowTriangleInput);
            
            Console.WriteLine();
            
            Console.Write("Please input the number of Red Circles 🔴: ");         
            var redCircleInput = Console.ReadLine();
            int redCircles;
            if (!string.IsNullOrEmpty(redCircleInput))
                redCircles = IfValidInteger(redCircleInput);
            
            Console.Write("Please input the number of Blue Circles: 🔵");       
            var blueCircleInput = Console.ReadLine();
            int blueCircles;
            if (!string.IsNullOrEmpty(blueCircleInput))
                blueCircles = IfValidInteger(blueCircleInput);
            
            Console.Write("Please input the number of Yellow Circles: 🟡");     
            var yellowCircleInput = Console.ReadLine();
            int yellowCircles;
            if (!string.IsNullOrEmpty(yellowCircleInput))
                yellowCircles = IfValidInteger(yellowCircleInput);

        }

        private static string CheckForEmptyInput(string input)
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

        private static DateTime ConvertToDateTime(string dateInput)
        {
            if(DateTime.TryParse(dateInput, out var dueDate))
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

        private static int IfValidInteger(string input)
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