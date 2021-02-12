using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sudoku
{
    public class Game
    {
        private List<List<int>> table = new List<List<int>>
        {
            new List<int>{0,0,0,0,0,0,0,0,0},
            new List<int>{0,0,0,0,0,0,0,0,0},
            new List<int>{0,0,0,0,0,0,0,0,0},
            new List<int>{0,0,0,0,0,0,0,0,0},
            new List<int>{0,0,0,0,0,0,0,0,0},
            new List<int>{0,0,0,0,0,0,0,0,0},
            new List<int>{0,0,0,0,0,0,0,0,0},
            new List<int>{0,0,0,0,0,0,0,0,0},
            new List<int>{0,0,0,0,0,0,0,0,0}
        };

        public void ImportGameFile(string filePath)
        {
            table = new List<List<int>>();

            var lines = File.ReadAllLines(filePath).ToList();
            foreach (var line in lines)
            {
                var lineNumbers = line.Split(",").Select(word => int.Parse(word));
                var tableLine = new List<int>();

                foreach(var number in lineNumbers)
                {
                    tableLine.Add(number);
                }

                while(tableLine.Count < 9)
                {
                    tableLine.Add(0);
                }

                table.Add(tableLine);
            }

            while (table.Count < 9)
            {
                var line = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                table.Add(line);
            }
        }

        public void EmptyUp(int line, int column)
        {
            table[line][column] = 0;
        }

        public void Set(int line, int column, int value)
        {
            table[line][column] = value;
        }

        public bool SetAndValidate(int line, int column, int value)
        {
            var result = Extensions.Validate(this, line, column, value);

            if (result)
            {
                table[line][column] = value;
            }

            return result;
        }

        public int Get(int line, int column)
        {
            return table[line][column];
        }

        public bool IsEmpty(int line, int column)
        {
            return table[line][column] == 0;
        }

        public void Display()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine();
            }


            for (int i = 0; i < 9; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < 9; j++)
                {                  
                    Console.Write(table[i][j] + " | ");
                }
                Console.WriteLine();

                for (int k = 0; k < 19; k++)
                {
                    Console.Write("-" + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public Game Clone()
        {
            var game = new Game();
            game.table = new List<List<int>>();

            table.ForEach(line =>
            {
                game.table.Add(new List<int>(line));
            });

            return game;
        }

        public bool IsComplete()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (IsEmpty(i, j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
