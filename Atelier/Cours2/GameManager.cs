public class GameManager
{
    public Board board;
    public UI_Board ui_Board;
    public InputManager inputManager;

    public bool b_IsPaused;
    public int i_SpeedIncrement = 1;

    public GameManager()
    {
        board = new Board(this);
        ui_Board = new UI_Board(board, this);
        inputManager = new(this);
    }

    public void UpdateGame()
    {
        inputManager.UpdateInput();

        if (!b_IsPaused)
        {
            board.UpdateBoard();
        }

    }

    public void DrawGame()
    {
        ui_Board.DrawUI();
    }
}