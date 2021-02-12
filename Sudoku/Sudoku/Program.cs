using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var robot = new Robot();

            Console.WriteLine("Import a game file? (Y/N)");
            var importFile = Console.ReadLine().ToLower().Equals("y");

            if (importFile)
            {
                Console.WriteLine("Give the file path!!");
                var filePath = Console.ReadLine();

                game.ImportGameFile(filePath);
            }

            game.Display();

            while (!game.IsComplete())
            {
                var result = false;

                while (!result)
                {
                    var userInput = Console.ReadLine().Split(" ").Select(word => int.Parse(word)).ToList();
                    var (line, column, value) = (userInput[0], userInput[1], userInput[2]);
                    result = game.SetAndValidate(line, column, value);
                    if (!result)
                    {
                        Console.WriteLine("Try again");
                    }
                }

                game.Display();

                robot.Move(game);

                game.Display();
            }
        }
    }
}
