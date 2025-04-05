using Grids;
using Raylib_cs;

namespace Snakes
{
    class Snake
    {
        Grid grid;

        Queue<Coordinates> body = new Queue<Coordinates>();
        Coordinates direction = Coordinates.right;

        public Snake(Grid grid, Coordinates start, int startSize = 3)
        {
            this.grid = grid;
            for (int i = startSize - 1; i >= 0; i--)
            {
                body.Enqueue(start - direction * i);
            }
        }

        public void Draw()
        {
            foreach ( var segment in body)
            {
                var pos = grid.GridToWorld(segment);
                Raylib.DrawRectangle((int) pos.X, (int) pos.Y, grid.cellSizes, grid.cellSizes, Color.Green);
            }
        }
    }
}