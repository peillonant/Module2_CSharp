using System.Numerics;

public class Deprecated
{
    private int GenerateNbCellSeen()
    {
        int i_EnemyLevel = 0;
        int result = 3;
        if (i_EnemyLevel == 1) return result;

        for (int i = 2; i <= i_EnemyLevel; i++)
        {
            result += 12 + (4 * (i - 3));
        }

        return result;
    }

    private void DrawPathfinding()
    {
        // if (!characterOrigin.IsPlayer())
        // {
        //     for (int x = 0; x < characterOrigin.GetMovementAlgorithm().GetPathfinder().GetCurrentPath().Count; x++)
        //     {
        //         if (characterOrigin.GetMovementAlgorithm().GetPathfinder().GetCurrentPath()[x].GetCellPosition() == v2_Pos)
        //         {
        //             Raylib.DrawRectangleLinesEx(recCell, 1f, Color.Green);
        //         }
        //     }
        // }
    }

    // Version before using Queue (Snake Script)
    private void UpdateBoardShrink(int i_NbRemove)
    {
        // // Remove Tail first
        // localBoard.RemoveObject(v2_Tail);

        // if (i_NbRemove == 1)
        // {
        //     // Now check if we add a new tail
        //     if (v2_Bodys.Count > 0)
        //     {
        //         v2_Tail = v2_Bodys.Last();
        //         v2_Bodys.Remove(v2_Tail);
        //         localBoard.AddObject(v2_Tail, TypeCell.OwnBodyTail);
        //     }
        //     else
        //         v2_Tail = new(-1,-1);
        // }
        // else if (i_NbRemove == 2)
        // {
        //     if (v2_Bodys.Count == 1)
        //     {
        //         // First we remove the first last element of body from the list and from the board
        //         localBoard.RemoveObject(v2_Bodys.Last());
        //         v2_Bodys.Remove(v2_Bodys.Last());
                
        //         // Then we remove the Tail
        //         v2_Tail = new(-1,-1);
        //     }
        //     else
        //     {
        //         // First we remove the first last element of body from the list and from the board
        //         localBoard.RemoveObject(v2_Bodys.Last());
        //         v2_Bodys.Remove(v2_Bodys.Last());

        //         // Second, we pass the last element of the body to the tail
        //         v2_Tail = v2_Bodys.Last();
        //         v2_Bodys.Remove(v2_Tail);
        //         localBoard.AddObject(v2_Tail, TypeCell.OwnBodyTail);
        //     }
            
        // }
    }

    private void UpdateBoardExtend(int i_NbExtend)
    {
        // // first we add the Extend on the body
        // for (int i = 0; i < i_NbExtend; i++)
        // {
        //     Vector2 v2_newPosBody;
        //     int i_DirectionOpposite;

        //     for (int j = 0; j < 3; j++ )
        //     {
        //         // take the opposite of the current Direction to put the extend behind the head
        //         i_DirectionOpposite = ((i_Direction + 2) % 4 == 0) ? 4 : (i_Direction + 2) % 4;
                
        //         if (j == 1)
        //             i_DirectionOpposite = ((i_Direction + 1) % 4 == 0) ? 4 : (i_Direction + 2) % 4;
        //         else if (j == 2)
        //             i_DirectionOpposite = ((i_Direction - 1) % 4 == 0) ? 4 : (i_Direction + 2) % 4;

        //         // We compute the position of the new body part
        //         if (v2_Tail != new Vector2(-1, -1))
        //         {
        //             v2_newPosBody = v2_Tail;
        //             v2_Bodys.Add(v2_Tail);
        //             localBoard.AddObject(v2_Tail, TypeCell.OwnBodyBody);
        //         }
        //         else
        //             v2_newPosBody = v2_Head;
                
        //         GenericFunction.ChangePosition(ref v2_newPosBody, i_DirectionOpposite);

        //         // Check if this new Position is avalaible on the board
        //         if (localBoard.CheckCollision(v2_newPosBody, false))
        //         {
        //             v2_Tail = v2_newPosBody;
        //             localBoard.AddObject(v2_Tail, TypeCell.OwnBodyTail);
        //             break;
        //         }
        //         else if (j == 2)
        //             CollisionExtend?.Invoke();
        //     }
        // }
    } 

    private void UpdateSnakePosition(Vector2 v2_newHeadPosition)
    {
        // // First we update the previous position
        // localBoard.UpdateSnakePosition(v2_newHeadPosition, i_Size);
        // localBoard.UpdateSnakePosition(v2_Head, i_Size);

        // if (i_Size == 2)
        // {
        //     // Condition to avoid having a bad behavior the first time we pass from a Snake size of 1 to 2
        //     if (v2_Tail != new Vector2(-1, -1))
        //         localBoard.UpdateSnakePosition(v2_Tail, i_Size);

        //     v2_Tail = v2_Head;
        // }
        // else if (i_Size > 2)
        // {
        //     if (v2_Bodys.Count < (i_Size - 2))
        //     {
        //         v2_Bodys.Add(v2_Head);
        //     }
        //     else
        //     {
        //         v2_Bodys.Add(v2_Head);

        //         // Now we update the Tail position on the board
        //         localBoard.UpdateSnakePosition(v2_Bodys[0], i_Size);
        //         localBoard.UpdateSnakePosition(v2_Tail, i_Size);

        //         // Now we update the Tail Vector we the new position of the last Body part
        //         v2_Tail = v2_Bodys[0];

        //         // Finally, we remove the part of the body that has been added on the tail
        //         v2_Bodys.RemoveAt(0);
        //     }
        // }

        // // New position become the Head
        // v2_Head = v2_newHeadPosition;
    }


    // Method create to update the viewBoard to have an animation regarding the head and the tail of the snake
    public void UpdateViewBoard(Action? callBack = null)
    {
        // // Tab that will contin the temporary of the CurrentViewBoards
        // ResetTMPViewBoards();

        // ViewCell currentCellTargetBoard;

        // for (int col = 0; col < GameInfo.i_nbCol; col++)
        // {
        //     for (int lin = 0; lin < GameInfo.i_nbLin; lin++)
        //     {  
        //         // Retrieve the cell at the position on the TargetBoard
        //         currentCellTargetBoard = new ViewCell( new Vector2(col, lin), targetBoards.GetCellFromBoard(new (col, lin)).GetTypeCell());

        //         // First check if the currentViewBoards typeCell is equal to the TargetBoard
        //         if (currentViewBoards[col, lin].GetTypeCell() != currentCellTargetBoard.GetTypeCell())
        //         {
        //             // We have to compute the transition of the Head from the currentViewBoard to the TargetBoard
        //             if (currentViewBoards[col, lin].GetTypeCell() == TypeCell.OwnBodyHead)
        //             {
        //                 Vector2 newCoordinate = RetrievePositionFromTypeCell(TypeCell.OwnBodyHead, col, lin);
                        
        //                 // Replace the cell of the Head by a Body
        //                 tmpViewBoards[col, lin].UpdateCell(TypeCell.OwnBodyBody);

        //                 // Retrieve the direction between the currentViewBoard cell and the Target One
        //                 Vector2 v2_Direction = newCoordinate - new Vector2(col, lin);

        //                 Vector2 newPositionToWorld;
        //                 newPositionToWorld = tmpViewBoards[(int) newCoordinate.X, (int) newCoordinate.Y].GetPositionToWorld() - (v2_Direction * GameInfo.i_SizeCell);

        //                 tmpViewBoards[(int) newCoordinate.X, (int) newCoordinate.Y].UpdateCell(TypeCell.OwnBodyHead);
        //                 tmpViewBoards[(int) newCoordinate.X, (int) newCoordinate.Y].SetPositionToWorld(newPositionToWorld);
                    
        //             }
        //             // else if (currentViewBoards[col, lin].GetTypeCell() == TypeCell.OwnBodyTail)
        //             // {
        //             //     Vector2 newCoordinate = RetrievePositionFromTypeCell(TypeCell.OwnBodyTail, col, lin);
        //             // }
        //             else if (currentViewBoards[col, lin].GetTypeCell() == TypeCell.Bomb)
        //             {
        //                 // Apply the Effect around the bomb
        //             }
        //             else
        //             {
        //                 if (tmpViewBoards[col, lin].GetTypeCell() == TypeCell.None)
        //                     tmpViewBoards[col, lin] = new ViewCell( currentCellTargetBoard.GetCellPosition(), currentCellTargetBoard.GetTypeCell());
        //             }
        //         }
        //         else
        //         {               
        //             if (currentViewBoards[col, lin].GetTypeCell() == TypeCell.OwnBodyHead)
        //             {   
        //                 // Check the Position To world, if different then we apply the Lerp
        //                 if (currentViewBoards[col, lin].GetPositionToWorld() != currentCellTargetBoard.GetPositionToWorld())
        //                 {
        //                     tmpViewBoards[col, lin] = new ViewCell( currentViewBoards[col,lin].GetCellPosition(), currentViewBoards[col,lin].GetTypeCell());
                            
        //                     Vector2 v2_newPositiontoWorld = Tweening.LerpVector2(tmpViewBoards[col, lin].GetPositionToWorld(), currentCellTargetBoard.GetPositionToWorld(), 0.25f);

        //                     if (Vector2.Distance(v2_newPositiontoWorld, currentCellTargetBoard.GetPositionToWorld()) < 0.01f)
        //                         v2_newPositiontoWorld = currentCellTargetBoard.GetPositionToWorld();

        //                     tmpViewBoards[col, lin].SetPositionToWorld(v2_newPositiontoWorld);

        //                 }
        //             }
        //             // else if (currentViewBoards[col, lin].GetTypeCell() == TypeCell.OwnBodyTail)
        //             // {

        //             // }
        //             else
        //             {
        //                 if (tmpViewBoards[col, lin].GetTypeCell() == TypeCell.None)
        //                     tmpViewBoards[col, lin] = new ViewCell( currentCellTargetBoard.GetCellPosition(), currentCellTargetBoard.GetTypeCell());
        //             }
        //         }
        //     }
        // }
        
        // TransferTMPtoCurrent();

        // callBack?.Invoke();
    }

    private Vector2 RetrievePositionFromTypeCell(TypeCell typeCell, int colum, int line)
    {
        Vector2 v2_Position = new (colum, line);

        // for (int lin = -1; lin <= 1; lin++)
        // {
        //     for (int col = -1; col <= 1; col++)
        //     {
        //         if (lin == col ) continue; // we are looking just on the 

        //         int newCol = colum + col;
        //         int newLin = line + lin;

        //         // Vérifie que la cellule est bien dans la grille
        //         if (newLin >= 0 && newLin < GameInfo.i_nbLin && newCol >= 0 && newCol < GameInfo.i_nbCol)
        //         {
        //             if (targetBoards.GetCellFromBoard(new (newCol, newLin)).GetTypeCell() == typeCell)
        //                 return new (newCol, newLin);
        //         }
        //     }
        // }

        return v2_Position;
    }


}
