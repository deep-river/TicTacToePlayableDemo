using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool gameEnd = true;

    private GridValue nextMove;
    private int[,] grid = new int[3,3];

    public BoardManager boardMgr;
    public UIManager uiMgr;
    private AIManager aiMgr;

    private int roundCounter = 0;

    void Start()
    {
        boardMgr.BindGridClickEvent();
        uiMgr.ShowInitScreen();
    }

    public void InitGame(GameMode mode)
    {
        InitGrid();
        aiMgr = new AIManager();
        if (mode == GameMode.Easy)
            aiMgr.SetStrategy(new MinimaxStrategy(3));
        if (mode == GameMode.Medium)
            aiMgr.SetStrategy(new MinimaxStrategy(5));
        if (mode == GameMode.Hard)
            aiMgr.SetStrategy(new MinimaxStrategy(9));

        roundCounter += 1;
        boardMgr.ResetBoard();
        gameEnd = false;
        nextMove = GridValue.X;
    }

    private void InitGrid()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
                grid[i, j] = 0;
        }
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
        Debug.Log("AIMove: " + targetCell);
        PlaceMark(targetCell.Item1 * 3 + targetCell.Item2);
    }

    private void PlaceMark(int index)
    {
        if (boardMgr.PlaceMark(index, nextMove))
        {
            grid[index / 3, index % 3] = (int)nextMove;
            SwitchTurn();
            CheckWinner();
        }
    }

    private void SwitchTurn()
    {
        if (nextMove == GridValue.X)
            nextMove = GridValue.O;
        else
            nextMove = GridValue.X;
    }

    private void CheckWinner()
    {
        //检查行
        for (int i = 0; i < 3; i++)
        {
            if (grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2] && grid[i, 0] != 0)
            {
                if (grid[i, 0] == 1)
                    uiMgr.UpdateResultUI(RoundResult.PlayerWin);
                else
                    uiMgr.UpdateResultUI(RoundResult.PlayerLost);
                StartNewRound(1.2f);
                return;
            }
        }
        //检查列
        for (int i = 0; i < 3; i++)
        {
            if (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i] && grid[0, i] != 0)
            {
                if (grid[0, i] == 1)
                    uiMgr.UpdateResultUI(RoundResult.PlayerWin);
                else
                    uiMgr.UpdateResultUI(RoundResult.PlayerLost);
                StartNewRound(1.2f);
                return;
            }
        }
        //检查对角线
        if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[0, 0] != 0)
        {
            if (grid[0, 0] == 1)
                uiMgr.UpdateResultUI(RoundResult.PlayerWin);
            else
                uiMgr.UpdateResultUI(RoundResult.PlayerLost);
            StartNewRound(1.2f);
            return;
        }
        if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0] && grid[0, 2] != 0)
        {
            if (grid[0, 2] == 1)
                uiMgr.UpdateResultUI(RoundResult.PlayerWin);
            else
                uiMgr.UpdateResultUI(RoundResult.PlayerLost);
            StartNewRound(1.2f);
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
            uiMgr.UpdateResultUI(RoundResult.Draw);
            StartNewRound(1.2f);
        }
    }

    public void RestartGame(GameMode mode)
    {
        gameEnd = false;
        uiMgr.ResetResultUI();
        InitGame(mode);
    }

    public void StartNewRound(float waitTime)
    {
        StartCoroutine(WaitAndStartNewRound(waitTime));
    }

    private IEnumerator WaitAndStartNewRound(float waitTime)
    {
        gameEnd = true;
        boardMgr.DisableButtons();
        yield return new WaitForSeconds(waitTime);
        uiMgr.HideResultText();
        InitGrid();
        boardMgr.ResetBoard();
        gameEnd = false;

        roundCounter += 1;
        if (roundCounter % 2 == 0)
        {
            nextMove = GridValue.O;
            AIMove();
        }
        else
            nextMove = GridValue.X;
    }
}
