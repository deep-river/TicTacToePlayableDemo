using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// AI�㷨�ӿ�
public interface AIStrategy
{
    (int, int) GetNextMove(int[,] board);
}

// �򵥹����㷨
public class SimpleStrategy : AIStrategy
{
    private int[,] weights = new int[3, 3];

    public SimpleStrategy()
    {
        // ����ÿ��λ�õ�Ȩ��
        CalculateWeights();
    }

    private void CalculateWeights()
    {
        // ����Ȩ�����
        weights[1, 1] = 100;
        // ��Ȩ�ش�֮
        weights[0, 0] = weights[0, 2] = weights[2, 0] = weights[2, 2] = 10;
        // ��Ȩ����С
        weights[0, 1] = weights[1, 0] = weights[1, 2] = weights[2, 1] = 1;
    }

    public (int, int) GetNextMove(int[,] board)
    {
        // �������Ϊ��,��������
        if (board[1, 1] == 0)
            return (1, 1);
        // �ҵ�Ȩ�����Ŀո��������
        int bestWeight = -1000;
        (int, int) bestMove = (-1, -1);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (weights[i, j] > bestWeight && board[i, j] == 0)
                {
                    bestWeight = weights[i, j];
                    bestMove = (i, j);
                }
            }
        }
        return bestMove;
    }
}

/*
// ����С�㷨 
public class MinimaxStrategy : AIStrategy
{

    private const int AI = 1;
    private const int OPPONENT = -1;

    private int depthLimit = 5;

    public (int, int) GetNextMove(int[,] board)
    {

        int bestScore = -1000;
        (int, int) bestMove = (-1, -1);

        bestMove = minimax(board, depthLimit, true);

        return bestMove;

    }

    private (int, int) minimax(int[,] board, int depth, bool isMaximizing)
    {

        if (GameEnded(board))
        {
            return Evaluate(board);
        }

        if (depth == 0)
        {
            return Evaluate(board);
        }

        if (isMaximizing)
        {
            int bestScore = -1000;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == empty)
                    {
                        board[i, j] = AI;
                        int score = minimax(board, depth - 1, false);
                        board[i, j] = empty;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = 1000;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == empty)
                    {
                        board[i, j] = OPPONENT;
                        int score = minimax(board, depth - 1, true);
                        board[i, j] = empty;

                        if (score < bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }
                    }
                }
            }
            return bestScore;
        }

    }

    private int Evaluate(int[,] board)
    {
        if (AIWin(board)) return 10000;
        if (OpponentWin(board)) return -10000;
        return 0;
    }

    private bool GameEnded(Chessboard board)
    {
        return AIWin(board) || OpponentWin(board) || BoardFull(board);
    }

}

// ���ؿ����������㷨
public class MCTSStrategy : AIStrategy
{

    public (int, int) GetNextMove(int[,] board)
    {
        // ���ؿ���������ʵ��
    }

}

*/

public class AIManager : MonoBehaviour
{
    private AIStrategy strategy;

    public void SetStrategy(AIStrategy strategy)
    {
        this.strategy = strategy;
    }

    public (int, int) GetNextMove(int[,] board)
    {
        return this.strategy.GetNextMove(board);
    }
}
