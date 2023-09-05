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
        // �ҵ�Ȩ�����Ŀո�
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


// ����С�㷨 
public class MinimaxStrategy : AIStrategy
{
    private const int MAX = 1;
    private const int MIN = -1;
    private int depthLimit = 3;

    public (int, int) GetNextMove(int[,] board)
    {
        return MiniMax(board, depthLimit, true);
    }

    private (int, int) MiniMax(int[,] board, int depth, bool isMax)
    {
        if (IsTerminal(board))
            return Evaluate(board);

        if (depth == 0)
            return Evaluate(board);

        if (isMax)
        {
            int bestScore = int.MinValue;
            (int, int) bestMove = (-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = MAX;
                        int score = MiniMax(board, depth - 1, false).Item1;
                        board[i, j] = 0;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }
                    }
                }
            }
            return bestMove;
        }
        else
        {
            int bestScore = int.MaxValue;
            (int, int) bestMove = (-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = MIN;
                        int score = MiniMax(board, depth - 1, true).Item1;
                        board[i, j] = 0;
                        if (score < bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }
                    }
                }
            }
            return bestMove;
        }
    }

    private bool IsTerminal(int[,] board)
    {
        return HasXWon(board) || HasOWon(board) || IsDraw(board);
    }

    private (int, int) Evaluate(int[,] board)
    {
        if (HasXWon(board)) return (+1, 0); // +1 for player win
        if (HasOWon(board)) return (-1, 0); // -1 for ai win
        return (0, 0);
    }

    private bool HasXWon(int[,] board)
    {
        // ������
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == MAX && board[i, 1] == MAX && board[i, 2] == MAX) return true;
        }
        // �������
        for (int j = 0; j < 3; j++)
        {
            if (board[0, j] == MAX && board[1, j] == MAX && board[2, j] == MAX) return true;
        }
        // ���Խ���
        if (board[0, 0] == MAX && board[1, 1] == MAX && board[2, 2] == MAX) return true;
        if (board[0, 2] == MAX && board[1, 1] == MAX && board[2, 0] == MAX) return true;
        return false;
    }

    private bool HasOWon(int[,] board)
    {
        // ������
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == MIN && board[i, 1] == MIN && board[i, 2] == MIN) return true;
        }
        // �������
        for (int j = 0; j < 3; j++)
        {
            if (board[0, j] == MIN && board[1, j] == MIN && board[2, j] == MIN) return true;
        }
        // ���Խ���
        if (board[0, 0] == MIN && board[1, 1] == MIN && board[2, 2] == MIN) return true;
        if (board[0, 2] == MIN && board[1, 1] == MIN && board[2, 0] == MIN) return true;
        return false;
    }

    private bool IsDraw(int[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 0)
                    return false;
            }
        }
        return true; // ����������Ϊƽ��
    }
}

/*
// ���ؿ����������㷨
public class MCTSStrategy : AIStrategy
{

    public (int, int) GetNextMove(int[,] board)
    {
        // ���ؿ���������ʵ��
    }
}
*/

public class AIManager
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
