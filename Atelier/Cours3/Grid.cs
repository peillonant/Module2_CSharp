using System.Numerics;

namespace Grids
{
    public struct Coordinates           // Different d'une class car lors d'une utilisation de cette struct c'est une copie
    {
        public readonly int columns;
        public readonly int row;

        public Coordinates(int columns, int row)
        {
            this.columns = columns;
            this.row = row;
        }

        public static Coordinates zero => new Coordinates(0,0);
        public static Coordinates one => new Coordinates(1,1);
        public static Coordinates left => new Coordinates(-1, 0);
        public static Coordinates right => new Coordinates(1, 0);
        public static Coordinates up => new Coordinates(0,-1);
        public static Coordinates down => new Coordinates(0,1);

        public Vector2 ToVector => new Vector2(columns, row);

        public static Coordinates operator *(Coordinates coord, int scalar)             // On surchage l'opérateur de multiplication
        {
            return new Coordinates(coord.columns * scalar, coord.row * scalar);
        }

        public static Coordinates operator *(int scalar, Coordinates coord)             // On surchage l'opérateur de multiplication
        {
            return new Coordinates(coord.columns * scalar, coord.row * scalar);
        }

        public static Coordinates operator - (Coordinates coord1, Coordinates coord2)
        {
            return new Coordinates(coord1.columns - coord2.columns, coord1.row - coord2.row);
        }

        public static Coordinates operator + (Coordinates coord1, Coordinates coord2)
        {
            return new Coordinates(coord1.columns + coord2.columns, coord1.row + coord2.row);
        }

        // public static bool operator == (Coordinates coord1, Coordinates coord2)
        // {
        //     return coord1.columns == coord2.columns && coord1.row == coord2.row;
        // }

        // public static bool operator != (Coordinates coord1, Coordinates coord2)
        // {
        //     return !(coord1.columns == coord2.columns && coord1.row == coord2.row);
        // }

     }


    public class Grid
    {
        public Vector2 position = Vector2.Zero;

        public int columns { get; private set;}
        public int rows { get; private set;}
        public int cellSizes { get; private set;}


        public Grid(int columns = 10, int rows = 10, int cellSizes = 64)
        {
            this.columns = columns;
            this.rows = rows;
            this.cellSizes = cellSizes;
        }

        public Coordinates WorldToGrid(Vector2 pos)
        {
            pos -= position;
            pos /= cellSizes;
            return new Coordinates((int)pos.X, (int)pos.Y);
        }

        public Vector2 GridToWorld(Coordinates coords)
        {
            coords *= cellSizes;
            return coords.ToVector + position;
        }

        public void Draw()
        {
            for (int column = 0; column < columns; column++)
            {
                for (int row = 0; row < rows ; row++)
                {
                    //Vector2 pos = GridToWorld(new Coordinates())
                }
            }
        }

    }
}