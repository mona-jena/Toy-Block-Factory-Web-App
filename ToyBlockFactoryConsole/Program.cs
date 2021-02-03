using System;

namespace ToyBlockFactoryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Toy Block Factory!\n");
            
            Console.Write("Please input your Name: ");              //check if not null
            var name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name can't be empty! Input your name once more");
                name = Console.ReadLine();
            }
            
            Console.Write("Please input your Address: ");            //check if not null
            var address = Console.ReadLine();
            while (string.IsNullOrEmpty(address))
            {
                Console.WriteLine("Address can't be empty! Input your name once more");
                name = Console.ReadLine();
            }

            Console.Write("Please input your Due Date: ");          //check if valid
            var dueDate = Console.ReadLine();

            Console.WriteLine();
            
            Console.Write("Please input the number of Red Squares: ");      //check if int
            var redSquares = Console.ReadLine();
            
            Console.Write("Please input the number of Blue Squares: ");      //check if int 
            var blueSquares = Console.ReadLine();
                
            Console.Write("Please input the number of Yellow Squares: ");      //check if int 
            var yellowSquares = Console.ReadLine();
            
            Console.WriteLine();

            Console.Write("Please input the number of Red Triangles: ");        //check if int
            var redTriangles = Console.ReadLine();
            
            Console.Write("Please input the number of Blue Triangles: ");       //check if int
            var blueTriangles = Console.ReadLine();
            
            Console.Write("Please input the number of Yellow Triangles: ");     //check if int
            var yellowTriangles = Console.ReadLine();
            
            Console.WriteLine();
            
            Console.Write("Please input the number of Red Circles: ");          //check if int
            var redCircles = Console.ReadLine();
            
            Console.Write("Please input the number of Blue Circles: ");         //check if int
            var blueCircles = Console.ReadLine();
            
            Console.Write("Please input the number of Yellow Circles: ");       //check if int
            var yellowCircles = Console.ReadLine();

        }
    }
}