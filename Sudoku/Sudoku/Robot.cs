namespace Sudoku
{
    public class Robot
    {
        public void Move(Game game)
        {
            var clone = game.Clone();

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (game.IsEmpty(i, j))
                    {
                        var result = Extensions.Backtrack(clone);

                        if (result)
                        {
                            game.Set(i, j, clone.Get(i, j));
                        }

                        return;
                    }
                }
            }
        }
    }
}
