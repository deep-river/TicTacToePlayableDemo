using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private bool gameEnd = true;

    private GridValue nextMove;
    private int[,] grid = new int[3,3];

    public BoardManager boardMgr;
    public UIManager uiMgr;
    private AIManager aiMgr;

    void Start()
    {
        boardMgr.BindGridClickEvent();
        uiMgr.ShowInitScreen();
    }

    public void InitGame(GameMode mode)
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
                grid[i, j] = 0;
        }

        aiMgr = new AIManager();
        AIStrategy simpleStrategy = new SimpleStrategy();
        AIStrategy minimaxStrategy = new MinimaxStrategy();
        if (mode == GameMode.Easy)
            aiMgr.SetStrategy(simpleStrategy);
        else
            aiMgr.SetStrategy(minimaxStrategy);

        boardMgr.ResetBoard();
        gameEnd = false;
        nextMove = GridValue.X;
    }

    public void PlayerMove(int index)
    {
        Debug.Log("ButtonClicked: " + index);
        if (!gameEnd)
        {
            PlaceMark(index);

            if (!gameEnd)
            {
                AIMove();
            }
        }
    }

    private void AIMove()
    {
        (int, int) targetCell = aiMgr.GetNextMove(grid);
        PlaceMark(targetCell.Item1 * 3 + targetCell.Item2);
    }

    private void PlaceMark(int index)
    {
        if (boardMgr.PlaceMark(index, nextMove))
        {
            grid[index / 3, index % 3] = (int)nextMove;
            if (nextMove == GridValue.X)
                nextMove = GridValue.O;
            else
                nextMove = GridValue.X;
            CheckWinner();
        }
    }

    private void CheckWinner()
    {
        //检查行
        for (int i = 0; i < 3; i++)
        {
            if (grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2] && grid[i, 0] != 0)
            {
                if (grid[i, 0] == 1)
                    uiMgr.SetResultUI(RoundResult.PlayerWin);
                else
                    uiMgr.SetResultUI(RoundResult.PlayerLost);
                EndRound();
                return;
            }
        }
        //检查列
        for (int i = 0; i < 3; i++)
        {
            if (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i] && grid[0, i] != 0)
            {
                if (grid[0, i] == 1)
                    uiMgr.SetResultUI(RoundResult.PlayerWin);
                else
                    uiMgr.SetResultUI(RoundResult.PlayerLost);
                EndRound();
                return;
            }
        }
        //检查对角线
        if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[0, 0] != 0)
        {
            if (grid[0, 0] == 1)
                uiMgr.SetResultUI(RoundResult.PlayerWin);
            else
                uiMgr.SetResultUI(RoundResult.PlayerLost);
            EndRound();
            return;
        }
        if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0] && grid[0, 2] != 0)
        {
            if (grid[0, 2] == 1)
                uiMgr.SetResultUI(RoundResult.PlayerWin);
            else
                uiMgr.SetResultUI(RoundResult.PlayerLost);
            EndRound();
            return;
        }
        //检查平局
        bool draw = true;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[i, j] == 0) draw = false;
            }
        }
        if (draw)
        {
            uiMgr.SetResultUI(RoundResult.Draw);
            EndRound();
        }
    }

    private void EndRound()
    {
        Debug.Log("EndRound");
        boardMgr.DisableButtons();
        gameEnd = true;
    }

    public void Restart(GameMode mode)
    {
        gameEnd = false;
        uiMgr.ResetResultUI();
        InitGame(mode);
    }
}
