using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public static class Extensions
    {
        public static bool Backtrack(Game game)
        {
            if (game.IsComplete())
            {
                return true;
            }

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (game.IsEmpty(i, j))
                    {
                        var possibilities = GetLinePossibilities(game, i)
                            .Intersect(GetColumnPossibilities(game, j))
                            .Intersect(GetSquarePossibilities(game, i, j));

                        foreach (var possibility in possibilities)
                        {
                            game.Set(i, j, possibility);
                            var result = Backtrack(game);

                            if (result == true)
                            {
                                return true;
                            }
                        }

                        game.EmptyUp(i, j);

                        return false;
                    }
                }
            }

            return false;
        }

        public static bool Validate(Game game, int line, int column, int value)
        {
            if (value < 1 || value > 9)
            {
                return false;
            }

            if (!game.IsEmpty(line, column))
            {
                return false;
            }

            var possibilities = GetLinePossibilities(game, line)
                .Intersect(GetColumnPossibilities(game, column))
                .Intersect(GetSquarePossibilities(game, line, column));

            return possibilities.Contains(value);
        }

        private static List<int> GetLinePossibilities(Game game, int line)
        {
            var possibilities = Enumerable.Range(1, 9).ToList();

            for (int i = 0; i < 9; i++)
            {
                possibilities.Remove(game.Get(line, i));
            }

            return possibilities;
        }

        private static List<int> GetColumnPossibilities(Game game, int column)
        {
            var possibilities = Enumerable.Range(1, 9).ToList();

            for (int i = 0; i < 9; i++)
            {
                possibilities.Remove(game.Get(i, column));
            }

            return possibilities;
        }

        private static List<int> GetSquarePossibilities(Game game, int line, int column)
        {
            var possibilities = Enumerable.Range(1, 9).ToList();
            var squareStartLine = (line / 3) * 3;
            var squareStartColumn = (column / 3) * 3;

            for (int i = squareStartLine; i < squareStartLine + 3; i++)
            {
                for (int j = squareStartColumn; j < squareStartColumn + 3; j++)
                {
                    possibilities.Remove(game.Get(i, j));
                }
            }

            return possibilities;
        }
    }
}
