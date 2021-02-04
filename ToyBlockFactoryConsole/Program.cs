using System;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Toy Block Factory!\n");
            
            Console.Write("Please input your Name: ");              
            var name = Console.ReadLine();
            name = InputValidator.IfEmptyInput(name);
            
            Console.Write("Please input your Address: ");   
            var address = Console.ReadLine();
            address = InputValidator.IfEmptyInput(address);
           
            Console.Write("Please input your Due Date: ");       
            var dateInput = Console.ReadLine();
            DateTime dueDate;
            if (!string.IsNullOrEmpty(dateInput))
                dueDate = InputValidator.ConvertToDateTime(dateInput);

            Console.WriteLine();
            
            Console.Write("Please input the number of Red Squares 🟥: ");     
            var redSquareInput = Console.ReadLine();
            int redSquares;
            if (!string.IsNullOrEmpty(redSquareInput))
                redSquares = InputValidator.IfValidInteger(redSquareInput);

            Console.Write("Please input the number of Blue Squares 🟦: ");   
            var blueSquareInput = Console.ReadLine();
            int blueSquares;
            if (!string.IsNullOrEmpty(blueSquareInput))
                blueSquares = InputValidator.IfValidInteger(blueSquareInput);
                
            Console.Write("Please input the number of Yellow Squares 🟨: ");     
            var yellowSquareInput = Console.ReadLine();
            int yellowSquares;
            if (!string.IsNullOrEmpty(yellowSquareInput))
                yellowSquares = InputValidator.IfValidInteger(yellowSquareInput);
            
            Console.WriteLine();

            Console.Write("Please input the number of Red Triangles 🔺: ");      
            var redTriangleInput = Console.ReadLine();
            int redTriangles;
            if (!string.IsNullOrEmpty(redTriangleInput))
                redTriangles = InputValidator.IfValidInteger(redTriangleInput);
            
            Console.Write("Please input the number of Blue Triangles : ");      
            var blueTriangleInput = Console.ReadLine();
            int blueTriangles;
            if (!string.IsNullOrEmpty(blueTriangleInput))
                blueTriangles = InputValidator.IfValidInteger(blueTriangleInput);
            
            Console.Write("Please input the number of Yellow Triangles: ");    
            var yellowTriangleInput = Console.ReadLine();
            int yellowTriangles;
            if (!string.IsNullOrEmpty(yellowTriangleInput))
                yellowTriangles = InputValidator.IfValidInteger(yellowTriangleInput);
            
            Console.WriteLine();
            
            Console.Write("Please input the number of Red Circles 🔴: ");         
            var redCircleInput = Console.ReadLine();
            int redCircles;
            if (!string.IsNullOrEmpty(redCircleInput))
                redCircles = InputValidator.IfValidInteger(redCircleInput);
            
            Console.Write("Please input the number of Blue Circles: 🔵");       
            var blueCircleInput = Console.ReadLine();
            int blueCircles;
            if (!string.IsNullOrEmpty(blueCircleInput))
                blueCircles = InputValidator.IfValidInteger(blueCircleInput);
            
            Console.Write("Please input the number of Yellow Circles: 🟡");     
            var yellowCircleInput = Console.ReadLine();
            int yellowCircles;
            if (!string.IsNullOrEmpty(yellowCircleInput))
                yellowCircles = InputValidator.IfValidInteger(yellowCircleInput);


            var toyBlockFactory = new ToyBlockFactory();
            if (string.IsNullOrEmpty(dueDate))
            {
                toyBlockFactory.CreateOrder(name, address);
            }
            
            Console.Write("Which report would you like: ");

        }
    }
}