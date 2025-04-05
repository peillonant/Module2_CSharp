using System.Numerics;
using Grids;
using Snakes;

class SceneGame : Scene
{
    Grid grid;
    Snake snake;

    public SceneGame()
    {
        grid = new Grid();
        snake = new Snake(grid, new (5, 5));
    }

    public override void Load()
    {
        grid.position = new Vector2(200,200);
    }

    public override void Unload()
    {
    }

    public override void Update()
    {
        
    }

    public override void Draw()
    {
        grid.Draw();
    }

}