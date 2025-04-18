using System.ComponentModel.Design;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class UIGrid
{
    static readonly IAssetsManager assets = Services.Get<IAssetsManager>();

    public static void DisplayBoardOutline(Character characterOrigin)
    {
        int indexZoom = (characterOrigin.GetZoom() < 4) ? (int) characterOrigin.GetZoom() : 3;
        float f_ZoomIn = RetrieveZoom(characterOrigin.GetZoom());
        float f_sizeSprite = 8 * indexZoom;
        float f_PX, f_PY;
        Rectangle sourceRec, sourceDes;                                                                                // Part of the spriteSheet we want
        Color colorBorder = characterOrigin.IsPlayer() ? Color.DarkBlue : Color.Red;

        // Display the border around the board
        #region Border
        // Border Top
        for (int col = 0; col < GameInfo.i_nbCol; col++)
        {
            f_PX = characterOrigin.GetPosition().X + f_sizeSprite * col * f_ZoomIn;
            f_PY = characterOrigin.GetPosition().Y - f_sizeSprite * f_ZoomIn;

            sourceRec = new Rectangle(f_sizeSprite * 8, 0, f_sizeSprite, f_sizeSprite);
            sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

            Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, colorBorder);
        }
        // Border Bottom
        for (int col = 0; col < GameInfo.i_nbCol; col++)
        {
            f_PX = characterOrigin.GetPosition().X + f_sizeSprite * col * f_ZoomIn;
            f_PY = characterOrigin.GetPosition().Y + f_sizeSprite * GameInfo.i_nbLin * f_ZoomIn;

            sourceRec = new Rectangle(f_sizeSprite * 5, 0, f_sizeSprite, f_sizeSprite);
            sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

            Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, colorBorder);
        }
        // Border Left
        for (int lin = 0; lin < GameInfo.i_nbCol; lin++)
        {
            f_PX = characterOrigin.GetPosition().X - f_sizeSprite * f_ZoomIn;
            f_PY = characterOrigin.GetPosition().Y + f_sizeSprite * lin * f_ZoomIn;

            sourceRec = new Rectangle(f_sizeSprite * 6, 0, f_sizeSprite, f_sizeSprite);
            sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

            Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, colorBorder);
        }
        // Border Right
        for (int lin = 0; lin < GameInfo.i_nbCol; lin++)
        {
            f_PX = characterOrigin.GetPosition().X + f_sizeSprite * GameInfo.i_nbCol * f_ZoomIn;
            f_PY = characterOrigin.GetPosition().Y + f_sizeSprite * lin * f_ZoomIn;

            sourceRec = new Rectangle(f_sizeSprite * 7, 0, f_sizeSprite, f_sizeSprite);
            sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

            Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, colorBorder);
        }
        #endregion

        #region Corner
        // Display corner
        // TOP-LEFT
        f_PX = characterOrigin.GetPosition().X - f_sizeSprite * f_ZoomIn;
        f_PY = characterOrigin.GetPosition().Y - f_sizeSprite * f_ZoomIn;

        sourceRec = new Rectangle(f_sizeSprite * 3, 0, f_sizeSprite, f_sizeSprite);
        sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

        Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, colorBorder);

        // TOP-RIGHT
        f_PX = characterOrigin.GetPosition().X + f_sizeSprite * GameInfo.i_nbCol * f_ZoomIn;
        f_PY = characterOrigin.GetPosition().Y - f_sizeSprite * f_ZoomIn;

        sourceRec = new Rectangle(f_sizeSprite * 4, 0, f_sizeSprite, f_sizeSprite);
        sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

        Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, colorBorder);

        // BOTTOM-RIGHT
        f_PX = characterOrigin.GetPosition().X + f_sizeSprite * GameInfo.i_nbCol * f_ZoomIn;
        f_PY = characterOrigin.GetPosition().Y + f_sizeSprite * GameInfo.i_nbLin * f_ZoomIn;

        sourceRec = new Rectangle(f_sizeSprite * 2, 0, f_sizeSprite, f_sizeSprite);
        sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

        Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, colorBorder);

        // BOTTOM-LEFT
        f_PX = characterOrigin.GetPosition().X - f_sizeSprite * f_ZoomIn;
        f_PY = characterOrigin.GetPosition().Y + f_sizeSprite * GameInfo.i_nbLin * f_ZoomIn;

        sourceRec = new Rectangle(f_sizeSprite * 1, 0, f_sizeSprite, f_sizeSprite);
        sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

        Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, colorBorder);
        #endregion

        // Display the floor of the board
        for (int col = 0; col < GameInfo.i_nbCol; col++)
        {
            for (int lin = 0; lin < GameInfo.i_nbLin; lin++)
            {
                int i_indexSprite;

                if (col == 0 && lin == 0)
                    i_indexSprite = 0;
                else if (col == GameInfo.i_nbCol-1 && lin == 0)
                    i_indexSprite = 1;
                else if (col == 0 && lin == GameInfo.i_nbLin-1)
                    i_indexSprite = 2;
                else if (col == GameInfo.i_nbCol-1 && lin == GameInfo.i_nbLin-1)
                    i_indexSprite = 3;
                else if (lin == 0)
                    i_indexSprite = 4;
                else if (lin == GameInfo.i_nbLin - 1)
                    i_indexSprite = 5;
                else if (col == 0)
                    i_indexSprite = 6;
                else if (col == GameInfo.i_nbCol - 1)
                    i_indexSprite = 7;
                else
                    i_indexSprite = 8;

                f_PX = characterOrigin.GetPosition().X + f_sizeSprite * col * f_ZoomIn;
                f_PY = characterOrigin.GetPosition().Y + f_sizeSprite * lin * f_ZoomIn;

                sourceRec = new Rectangle(f_sizeSprite * i_indexSprite, 0, f_sizeSprite, f_sizeSprite);
                sourceDes = new Rectangle(f_PX, f_PY, f_sizeSprite * f_ZoomIn, f_sizeSprite * f_ZoomIn);

                Raylib.DrawTexturePro(assets.GetTextureFromSet("FloorGrid", indexZoom), sourceRec, sourceDes, new (0,0), 0, Color.White);
            }
        }
    }

    public static void DisplayGrid(Character characterOrigin)
    {
        if (characterOrigin.IsAlive())
        {
            int indexZoom = (characterOrigin.GetZoom() < 4) ? (int) characterOrigin.GetZoom() : 3;
            float f_ZoomIn = RetrieveZoom(characterOrigin.GetZoom());
            float f_sizeSprite = 8 * indexZoom;

            for (int col = 0; col < GameInfo.i_nbCol; col++)
            {
                for (int lin = 0; lin < GameInfo.i_nbLin; lin++)
                {
                    // Display the floor of the board
                    Vector2 v2_PositionToWorld = characterOrigin.GetViewBoard().GetViewCell(new(col,lin)).GetPositionToWorld();

                    // int tmpX = (int) (characterOrigin.GetPosition().X + v2_PositionToWorld.X * f_ZoomIn);
                    // int tmpY = (int) (characterOrigin.GetPosition().Y + v2_PositionToWorld.Y * f_ZoomIn);

                    float tmpX = characterOrigin.GetPosition().X + f_sizeSprite * col * f_ZoomIn;
                    float tmpY = characterOrigin.GetPosition().Y + f_sizeSprite * lin * f_ZoomIn;

                    Vector2 v2_Pos = new(col, lin);
                    // Then display the Entity if the cell is above 4 (not the snake)
                    if ((int) characterOrigin.GetViewBoard().GetViewCell(v2_Pos).GetTypeCell() > 3)
                        DisplayGridPart(characterOrigin, v2_Pos , tmpX, tmpY, f_ZoomIn);
                }
            }

            DisplaySnakeBodyPart(characterOrigin);
        }
    }

    private static void DisplayGridPart(Character characterOrigin, Vector2 v2_CellCoordinate ,float f_PX, float f_PY, float f_ZoomIn)
    {
        TypeCell typeCell = characterOrigin.GetViewBoard().GetViewCell(v2_CellCoordinate).GetTypeCell();
        int i_IndexSprite = (characterOrigin.GetZoom() < 4) ? (int) characterOrigin.GetZoom() : 3;

        if (typeCell == TypeCell.Apple)
            Raylib.DrawTextureEx(assets.GetTextureFromSet("AppleGrid", i_IndexSprite), new (f_PX, f_PY), 0, f_ZoomIn, Color.White);
        else if (typeCell == TypeCell.Bonus || typeCell == TypeCell.Malus)
            RetrieveTextureBonus(characterOrigin, v2_CellCoordinate, f_PX, f_PY, f_ZoomIn);
        else if (typeCell == TypeCell.Bomb)
            RetrieveTextureBomb(characterOrigin, v2_CellCoordinate, f_PX, f_PY, f_ZoomIn);
        else if (typeCell == TypeCell.Border)
            RetrieveTextureBorder(characterOrigin, f_PX, f_PY, f_ZoomIn);
        else if (typeCell == TypeCell.Collision)
            RetrieveTextureCollision(characterOrigin, v2_CellCoordinate, f_PX, f_PY, f_ZoomIn);
    }

    private static void RetrieveTextureBonus(Character characterOrigin, Vector2 v2_CellCoordinate, float f_DisplayPX, float f_DisplayPY, float f_ZoomIn)
    {
        TypeCell typeCell = characterOrigin.GetViewBoard().GetViewCell(v2_CellCoordinate).GetTypeCell();
        int i_IndexSprite = (int) characterOrigin.GetZoom();
        int i_sizeSprite = 8 * i_IndexSprite;

        Texture2D textureCell = (typeCell == TypeCell.Bonus) ? assets.GetTextureFromSet("BonusGrid", i_IndexSprite) : assets.GetTextureFromSet("MalusGrid", i_IndexSprite);
        Color colorCell = (typeCell == TypeCell.Bonus) ? UI_Board_Sprite.GetColorCell(5) : UI_Board_Sprite.GetColorCell(6);

        int i_TimerCell = (int) Math.Floor(characterOrigin.GetViewBoard().GetViewCell(v2_CellCoordinate).GetCellTimer() / 0.25f) % 4;

        Rectangle sourceRec = new(i_sizeSprite * i_TimerCell, 0, i_sizeSprite, i_sizeSprite);
        Rectangle desRec = new(f_DisplayPX, f_DisplayPY, i_sizeSprite * f_ZoomIn, i_sizeSprite * f_ZoomIn);

        Raylib.DrawTexturePro(textureCell, sourceRec, desRec, new(0,0), 0, colorCell);
    }

    private static void RetrieveTextureBomb(Character characterOrigin, Vector2 v2_CellCoordinate, float f_DisplayPX, float f_DisplayPY, float f_ZoomIn)
    {
        int i_IndexSprite = (int) characterOrigin.GetZoom();
        int i_sizeSprite = 8 * i_IndexSprite;
        float f_Timer = characterOrigin.GetViewBoard().GetViewCell(v2_CellCoordinate).GetCellTimer();
        float PulseFrequency = 1f; // Nombre de pulsations complètes par seconde
        float MinAlpha = 0f;
        float MaxAlpha = 255f;
        int innerAlpha, outerAlpha;

        float normalizedTime = f_Timer % (1f / PulseFrequency) * PulseFrequency;
        float pulseFactor = (float)Math.Sin(normalizedTime * Math.PI);
        
        if (f_Timer < 4.9)
        {
            innerAlpha = (int)(MinAlpha + (MaxAlpha - MinAlpha) * (0.5f + 0.5f * pulseFactor));
            outerAlpha = (int)(MaxAlpha - (MaxAlpha - MinAlpha) * (0.5f + 0.5f * pulseFactor));
        }
        else 
        {
            innerAlpha = 50;
            outerAlpha = 255;
        }

        Color colorCellInner = new Color(230, 41, 55, innerAlpha);
        Color colorCellOuter = new Color (230, 41, 55, outerAlpha);

        Raylib.DrawCircleGradient((int) f_DisplayPX + (i_sizeSprite / 2),(int) f_DisplayPY + (i_sizeSprite / 2), i_sizeSprite * f_ZoomIn, colorCellInner, colorCellOuter);

        Raylib.DrawTextureEx(assets.GetTextureFromSet("BombGrid", i_IndexSprite), new (f_DisplayPX, f_DisplayPY), 0, f_ZoomIn, Color.White);
    }

    private static void RetrieveTextureBorder(Character characterOrigin, float f_DisplayPX, float f_DisplayPY, float f_ZoomIn)
    {
        int indexZoom = (characterOrigin.GetZoom() < 4) ? (int) characterOrigin.GetZoom() : 3;
        int i_sizeSprite = 8 * indexZoom;
        Rectangle sourceRec, destRec;                                                                               
            
        sourceRec = new Rectangle(0, 0, i_sizeSprite, i_sizeSprite);
        destRec = new Rectangle(f_DisplayPX, f_DisplayPY, i_sizeSprite * f_ZoomIn, i_sizeSprite * f_ZoomIn);

        Raylib.DrawTexturePro(assets.GetTextureFromSet("BorderGrid", indexZoom), sourceRec, destRec, new (0,0), 0, Color.White);
    }

    private static void RetrieveTextureCollision(Character characterOrigin, Vector2 v2_CellCoordinate, float f_DisplayPX, float f_DisplayPY, float f_ZoomIn)
    {
        // By using the Zoom of the character we will know if we have to display which asset
        int indexZoom = (characterOrigin.GetZoom() < 4) ? (int) characterOrigin.GetZoom() : 3;
        int i_sizeSprite = 8 * indexZoom;
        Rectangle sourceRec, desRec;                                                                                
        int indexImage = 0;
        int i_bitMasking = 0;

        // We check all cell around
        for (int col = -1; col <= 1; col++)
        {
            for (int lin = -1; lin <= 1; lin++)
            {
                if ( Math.Abs(lin) == Math.Abs(col) || lin == col) continue;

                int newCol = (int) v2_CellCoordinate.X + col;
                int newLin = (int) v2_CellCoordinate.Y + lin;

                // Vérifie que la cellule est bien dans la grille
                if (newLin >= 0 && newLin < GameInfo.i_nbLin && newCol >= 0 && newCol < GameInfo.i_nbCol)
                {
                    ViewCell cellToTest = characterOrigin.GetViewBoard().GetViewCell(new (newCol, newLin));

                    if (cellToTest.GetTypeCell() == TypeCell.Collision)
                    {
                        if (col == -1)
                            i_bitMasking += 8;
                        else if (col == 1)
                            i_bitMasking += 2;
                        else if (lin == -1)
                            i_bitMasking += 1;
                        else if (lin == 1)
                            i_bitMasking += 4;
                    }
                }
            }
        }

        switch (i_bitMasking)
        {
            case 0:
                indexImage = 0;
                break;
            case 1:
                indexImage = 1;
                break;
            case 2:
                indexImage = 2;
                break;
            case 4:
                indexImage = 3;
                break;
            case 8:
                indexImage = 4;
                break;
            case 5:
                indexImage = 5;
                break;
            case 10:
                indexImage = 6;
                break;
            case 15:
                indexImage = 7;
                break;
        }
            
        sourceRec = new Rectangle(indexImage * i_sizeSprite, 0, i_sizeSprite, i_sizeSprite);
        desRec = new Rectangle(f_DisplayPX, f_DisplayPY, i_sizeSprite * f_ZoomIn, i_sizeSprite * f_ZoomIn);

        Raylib.DrawTexturePro(assets.GetTextureFromSet("ObstacleGrid", indexZoom), sourceRec, desRec, new(0,0), 0, characterOrigin.GetCharacterColor());
    }

    private static void DisplaySnakeBodyPart(Character characterOrigin)
    {   
        // By using the Zoom of the character we will know if we have to display which asset
        int indexZoom = (characterOrigin.GetZoom() < 4) ? (int) characterOrigin.GetZoom() : 3;
        float f_ZoomIn = RetrieveZoom(characterOrigin.GetZoom());
        int i_sizeSprite = 8 * indexZoom;
        Rectangle sourceRec;                                                                                
        Vector2 v2_PositionToWorld;
        TypeCell typeCell;
        int indexImage = -1;
        bool b_TailDrawn = false;

        // We will retrieve the Snake Body (Coordinate) from the Character and we display thanks to it
        var snakeBody = characterOrigin.GetSnake().GetSnakeBody().ToArray();
        for (int i = 0; i < snakeBody.Length; i++ )
        {
            // First we check the type of the cell (Be careful with the Tail because we have to check the cell around to find it)
            typeCell = characterOrigin.GetViewBoard().GetViewCell(snakeBody[i]).GetTypeCell();
            v2_PositionToWorld = characterOrigin.GetViewBoard().GetViewCell(snakeBody[i]).GetPositionToWorld();

            int tmpX = (int) (characterOrigin.GetPosition().X + v2_PositionToWorld.X * characterOrigin.GetZoom());
            int tmpY = (int) (characterOrigin.GetPosition().Y + v2_PositionToWorld.Y * characterOrigin.GetZoom());
            
            if (typeCell == TypeCell.OwnBodyHead)
            {
                // If it's the head then we have to check the direction of the snake to have the correct rotation of the head
                indexImage = characterOrigin.GetSnake().GetDirection() - 1;
            }
            else if (typeCell == TypeCell.OwnBodyBody)
            {
                if (i > 0)
                    indexImage = RetrieveIndexBodyImage(snakeBody[i], snakeBody[i-1], snakeBody[i+1]);
            }
            else if (typeCell == TypeCell.OwnBodyTail)
            {
                indexImage = RetrieveIndexTailImage(snakeBody[i], snakeBody[i+1]);
                b_TailDrawn = true;
            }

            // This condition is here when the tail is not at the place expected due to the animation on the View Board
            if (!b_TailDrawn && i == 0 && snakeBody.Length > 1)
            {
                // First we have to retrieve the cell of the tail on the View Board
                Vector2 v2_PositionTail = RetrieveTailOnViewBoard(snakeBody[i], characterOrigin.GetViewBoard());
                Vector2 v2_PositionToWorldTail = characterOrigin.GetViewBoard().GetViewCell(v2_PositionTail).GetPositionToWorld();
                
                int tmpTailX = (int) (characterOrigin.GetPosition().X + v2_PositionToWorldTail.X * characterOrigin.GetZoom());
                int tmpTailY = (int) (characterOrigin.GetPosition().Y + v2_PositionToWorldTail.Y * characterOrigin.GetZoom());
                int indexImageTail = RetrieveIndexTailImage(v2_PositionTail, snakeBody[i]);

                Rectangle sourceRecTail = new Rectangle(indexImageTail * i_sizeSprite, 0, i_sizeSprite, i_sizeSprite);
                Rectangle recDes = new Rectangle(tmpTailX, tmpTailY, i_sizeSprite * f_ZoomIn, i_sizeSprite * f_ZoomIn);

                Raylib.DrawTexturePro(assets.GetTextureFromSet("Snake", indexZoom), sourceRecTail, recDes, new (0,0), 0, characterOrigin.GetCharacterColor());

                // Now we have to display the cell of the body of the View Board
                indexImage = RetrieveIndexBodyImage(snakeBody[i], v2_PositionTail ,snakeBody[i+1]);
            }

            if (indexImage >= 0)
            {
                sourceRec = new Rectangle(indexImage * i_sizeSprite, 0, i_sizeSprite, i_sizeSprite);
                Rectangle recDes = new Rectangle(tmpX, tmpY, i_sizeSprite * f_ZoomIn, i_sizeSprite * f_ZoomIn);

                Raylib.DrawTexturePro(assets.GetTextureFromSet("Snake", indexZoom), sourceRec, recDes, new (0,0), 0, characterOrigin.GetCharacterColor());
            }
        }
    }

    private static int RetrieveIndexBodyImage(Vector2 snakeBodyToCheck, Vector2 snakeBodyBefore, Vector2 snakeBodyAfter)
    {
        int result = 0;
        int indexImage = 4;
        Vector2 diff;
        
        // First we compare the part to Check with the previous one
        diff = snakeBodyToCheck - snakeBodyBefore;
        result += ComputeBitmask(diff);

        // Then we compare the part to check with the next one
        diff =  snakeBodyToCheck - snakeBodyAfter;
        result += ComputeBitmask(diff);

        switch(result)
        {
            case 3:
                indexImage = 5;
                break;
            case 5:
                indexImage = 8;
                break;
            case 6:
                indexImage = 4;
                break;
            case 9:
                indexImage = 6;
                break;
            case 10:
                indexImage = 9;
                break;
            case 12:
                indexImage = 7;
                break;
        }

        return indexImage;
    }

    private static int ComputeBitmask(Vector2 coordinateToCheck)
    {
        int result = 0;

        if(coordinateToCheck.Y == 1)
            result = 1;
        else if(coordinateToCheck.X == -1)
            result = 2;
        else if (coordinateToCheck.Y == -1)
            result = 4;
        else if (coordinateToCheck.X == 1)
            result = 8;

        return result;
    }

    private static int RetrieveIndexTailImage(Vector2 snakeBodyToCheck, Vector2 snakeBodyAfter)
    {
        int indexImage = 10;
        Vector2 diff;
        
        // First we compare the part to Check with the previous one
        diff = snakeBodyAfter - snakeBodyToCheck;

        if(diff.Y == -1)
            indexImage = 10;
        else if(diff.X == -1)
            indexImage = 11;
        else if (diff.Y == 1)
            indexImage = 12;
        else if (diff.X == 1)
            indexImage = 13;

        return indexImage;
    }


    private static float RetrieveZoom(float f_Zoom)
    {
        float f_resultZoom = 1;
        
        if (f_Zoom == 1.5f)
            f_resultZoom = 1.5f;
        else if (f_Zoom == 2.5f)
            f_resultZoom = 1.25f;

        return f_resultZoom;
    }

    // Method to check all neighboor cell of the snakeBody part to find the Tail
    private static Vector2 RetrieveTailOnViewBoard(Vector2 snakeBodyToCheck, ViewBoard characterViewBoard)
    {
        Vector2 v2_CoordinateTail = new();

        for (int col = -1; col <= 1; col++)
        {
            for (int lin = -1; lin <= 1; lin++)
            {
                if ( Math.Abs(lin) == Math.Abs(col) || lin == col) continue;

                int newCol = (int) snakeBodyToCheck.X + col;
                int newLin = (int) snakeBodyToCheck.Y + lin;

                // Vérifie que la cellule est bien dans la grille
                if (newLin >= 0 && newLin < GameInfo.i_nbLin && newCol >= 0 && newCol < GameInfo.i_nbCol)
                {
                    ViewCell cellToTest = characterViewBoard.GetViewCell(new (newCol, newLin));

                    if (cellToTest.GetTypeCell() == TypeCell.OwnBodyTail)
                        v2_CoordinateTail = cellToTest.GetCellPosition();
                }
            }
        }

        return v2_CoordinateTail;
    }
}