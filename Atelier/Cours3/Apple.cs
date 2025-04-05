using Grids;

namespace Snakes
{
    public class Apple
    {
        public Coordinates coordinates {get; private set;}
        Grid grid;

        public Apple(Grid grid)
        {
            this.grid = grid;
        }
    }
}