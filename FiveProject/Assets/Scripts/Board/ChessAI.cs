/**
*Copyright(C) 2015 by 面试
*All rights reserved.
*FileName: ChessAI.cs
*Author: 刘成
*Version: 1.5
*UnityVersion：2017.4.3f1
*Date: 2019-02-17
*Description:
*History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiveChess
{
    public struct ShapeScore
    {
        public int Score;//权重分数
        public int[] FiveMode;//模型央视
        public ShapeScore(int score, params int[] arg)
        {
            Score = score;
            FiveMode = arg;
        }
    }

    public class ChessAI : MonoBehaviour
    {
        //        //评价当前局面的函数
        //成五，100000
        //        //活四, 10000
        //        //活三 1000
        //        //活二 100
        //        //活一 10
        //        死四, 1000
        //        死三 100
        //        死二 10

       

        #region 算法
        public int Evaluate(int[,] board, int playerFlag)
        {
            //这里用4代表已经搜索过的节点
            //得到对手的标记
            int opponent = playerFlag == 1 ? 2 : 1;
            int score = 0;
            //对每个点进行搜索s
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] != playerFlag)
                    {
                        continue;
                    }
                    else
                    {
                        //如果碰到自己的棋就往四面八方搜索
                        //上下
                        board[i, j] = 4;
                        //增量
                        int m = 1;
                        int Kill = 1;
                        int onescore = 10;
                        while (true)
                        {

                            if (i + m >= 15 || (i + m < 15 && board[i + m, j] == opponent))
                            {

                                onescore /= 10;
                                m = 1;
                                break;
                            }
                            else if (board[i + m, j] == 0)
                            {
                                m = 1;
                                break;
                            }
                            else if (board[i + m, j] == playerFlag || board[i + m, j] == 4)
                            {
                                onescore *= 10;
                                board[i + m, j] = 4;
                                m++;
                                Kill++;
                            }

                        }//上
                        while (true)
                        {

                            if (i - m < 0 || (i - m >= 0 && board[i - m, j] == opponent))
                            {
                                onescore /= 10;
                                m = 1;
                                break;
                            }
                            else if (board[i - m, j] == 0)
                            {
                                m = 1;
                                break;
                            }
                            else if (board[i - m, j] == playerFlag || board[i - m, j] == 4)
                            {
                                onescore *= 10;
                                board[i - m, j] = 4;
                                m++;
                                Kill++;
                            }

                        }//下  
                        if (Kill >= 5) score += 1000000;
                        Kill = 1;
                        score += onescore;
                        onescore = 10;
                        while (true)
                        {

                            if (j - m < 0 || (j - m >= 0 && board[i, j - m] == opponent))
                            {
                                onescore /= 10;
                                m = 1;
                                break;
                            }
                            else if (board[i, j - m] == 0)
                            {
                                m = 1;
                                break;
                            }
                            else if (board[i, j - m] == playerFlag || board[i, j - m] == 4)
                            {
                                onescore *= 10;
                                board[i, j - m] = 4;
                                m++;
                                Kill++;
                            }

                        }//左
                        while (true)
                        {

                            if (j + m >= 15 || (j + m < 15 && board[i, j + m] == opponent))
                            {
                                onescore /= 10;
                                m = 1;
                                break;
                            }
                            else if (board[i, j + m] == 0)
                            {
                                m = 1;
                                break;
                            }
                            else if (board[i, j + m] == playerFlag || board[i, j + m] == 4)
                            {
                                onescore *= 10;
                                board[i, j + m] = 4;
                                m++;
                                Kill++;
                            }

                        }//右
                        if (Kill >= 5) score += 1000000;
                        Kill = 1;
                        score += onescore;
                        onescore = 10;
                        while (true)
                        {

                            if ((i + m >= 15 || j + m >= 15) || ((i + m < 15 && j + m < 15) && board[i + m, j + m] == opponent))
                            {
                                onescore /= 10;
                                m = 1;
                                break;
                            }
                            else if ((i + m < 15 && j + m < 15) && board[i + m, j + m] == 0)
                            {
                                m = 1;
                                break;
                            }
                            else if ((i + m < 15 && j + m < 15) && (board[i + m, j + m] == playerFlag || board[i + m, j + m] == 4))
                            {
                                onescore *= 10;
                                board[i + m, j + m] = 4;
                                m++;
                                Kill++;
                            }

                        }//右上
                        while (true)
                        {

                            if ((i - m < 0 || j - m < 0) || ((i - m >= 0 && j - m >= 0) && board[i - m, j - m] == opponent))
                            {
                                onescore /= 10;
                                m = 1;
                                break;
                            }
                            else if ((i - m >= 0 && j - m >= 0) && board[i - m, j - m] == 0)
                            {
                                m = 1;
                                break;
                            }
                            else if ((i - m >= 0 && j - m >= 0) && (board[i - m, j - m] == playerFlag || board[i - m, j - m] == 4))
                            {
                                onescore *= 10;
                                board[i - m, j - m] = 4;
                                m++;
                                Kill++;
                            }

                        }//左下
                        if (Kill >= 5) score += 1000000;
                        Kill = 1;
                        score += onescore;
                        onescore = 10;
                        while (true)
                        {

                            if ((i + m >= 15 || j - m < 0) || ((i + m < 15 && j - m >= 0) && board[i + m, j - m] == opponent))
                            {
                                onescore /= 10;
                                m = 1;
                                break;
                            }
                            else if ((i + m < 15 && j - m >= 0) && board[i + m, j - m] == 0)
                            {
                                m = 1;
                                break;
                            }
                            else if ((i + m < 15 && j - m >= 0) && (board[i + m, j - m] == playerFlag || board[i + m, j - m] == 4))
                            {
                                onescore *= 10;
                                board[i + m, j - m] = 4;
                                m++;
                                Kill++;
                            }

                        }//
                        while (true)
                        {

                            if ((i - m < 0 || j + m >= 15) || ((i - m >= 0 && j + m < 15) && board[i - m, j + m] == opponent))
                            {
                                onescore /= 10;
                                m = 1;
                                break;
                            }
                            else if ((i - m >= 0 && j + m < 15) && board[i - m, j + m] == 0)
                            {
                                m = 1;
                                break;
                            }
                            else if ((i - m >= 0 && j + m < 15) && (board[i - m, j + m] == playerFlag || board[i - m, j + m] == 4))
                            {
                                onescore *= 10;
                                board[i - m, j + m] = 4;
                                m++;
                                Kill++;
                            }

                        }
                        if (Kill >= 5) score += 1000000;
                        Kill = 1;
                        score += onescore;
                        onescore = 10;




                    }
                }
            }
            return score;
        }
        //ab剪枝
        private int Alpha_Beta_Cut(int playerFlag, int[,] board, int Fbestscore, out int X, out int Y, int Layer = 1)//bestScore max层最大奇数层，min层为最小偶数层
        {
            int m, n;//凑数的
            int i, j;
            X = 0; Y = 0;
            int opppnentFlag = playerFlag == 1 ? 2 : 1;
            if (Layer < maxLevel)
            {
                //max搜索
                if (Layer % 2 == 1)
                {
                    int Alpha = int.MinValue;
                    for (i = 0; i < 15; i++)
                    {
                        for (j = 0; j < 15; j++)
                        {
                            if (board[i, j] == 0 && ifCalculate(i, j, board))
                            {
                                //新的棋局
                                int[,] newBoard = Copy(board);
                                newBoard[i, j] = playerFlag;
                                //算杀
                                if (Evaluate(Copy(board), playerFlag) >= 1000000)
                                {
                                    X = i;
                                    Y = j;
                                    return int.MaxValue;
                                }
                                //DFS
                                int thisScore = Alpha_Beta_Cut(playerFlag, newBoard, Alpha, out m, out n, Layer + 1);
                                //A剪枝
                                if (thisScore > Fbestscore)
                                {
                                    return thisScore;
                                }

                                if (thisScore >= Alpha)
                                {
                                    Alpha = thisScore;
                                    X = i;
                                    Y = j;
                                }


                            }
                        }
                        //剪枝退出
                    }
                    return Alpha;
                }
                //min搜索
                else
                {
                    int Beta = int.MaxValue;
                    for (i = 0; i < 15; i++)
                    {
                        for (j = 0; j < 15; j++)
                        {
                            if (board[i, j] == 0 && ifCalculate(i, j, board))
                            {

                                int[,] newBoard = Copy(board);
                                newBoard[i, j] = opppnentFlag;
                                //算杀
                                if (Evaluate(Copy(board), opppnentFlag) >= 1000000)
                                {
                                    X = i;
                                    Y = j;
                                    return int.MinValue;
                                }
                                //DFS
                                int thisScore = Alpha_Beta_Cut(playerFlag, newBoard, Beta, out m, out n, Layer + 1);
                                //B剪枝
                                if (thisScore < Fbestscore)
                                {
                                    return thisScore;
                                }

                                if (thisScore < Beta)
                                {
                                    Beta = thisScore;
                                    X = i;
                                    Y = j;
                                }


                            }
                        }
                    }
                    return Beta;
                }
            }
            else
            {
                int Beta = int.MaxValue;
                for (i = 0; i < 15; i++)
                {
                    for (j = 0; j < 15; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            int[,] newBoard = Copy(board);
                            newBoard[i, j] = opppnentFlag;
                            if (Evaluate(Copy(board), opppnentFlag) > 1000000)
                            {
                                return int.MinValue;
                            }

                            int thisScore = Evaluate(Copy(board), playerFlag) - Evaluate(Copy(board), opppnentFlag);
                            if (thisScore < Fbestscore)
                            {
                                return thisScore;
                            }

                            if (thisScore < Beta)
                            {
                                Beta = thisScore;
                            }
                        }
                    }
                }
                return Beta;
            }

        }


        public bool ifCalculate(int X, int Y, int[,] board)
        {
            bool flag = false;
            for (int i = X - 1; i <= X + 1; i++)
            {
                for (int j = Y - 1; j <= Y + 1; j++)
                {

                    if (i >= 0 && i < 15 && j >= 0 && j < 15 && board[i, j] != 0)
                    {
                        flag = true;
                        break;
                    }
                }

            }
            return flag;
        }
        public int[,] Copy(ChessGird[,] old)
        {
            int[,] grid = new int[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    grid[i, j] = old[i, j].weightedValues;
                }
            }
            return grid;
        }
        public int[,] Copy(int[,] old)
        {
            int[,] grid = new int[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int a = old[i, j];
                    grid[i, j] = a;
                }
            }
            return grid;
        }


        #endregion

        #region 外部接口
        /// <summary>
        /// AI外部接口
        /// </summary>
        /// <param name="board"></param>
        /// <param name="PlayerFlag"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void ALPerform(ChessGird[,] board, int PlayerFlag, out int X, out int Y)
        {
            int[,] temp = Copy(board);
            int Score = Alpha_Beta_Cut(playerFlag, temp, int.MaxValue, out X, out Y);
        }
        #endregion

        #region Unity回调
        private int aiX, aiY;
        public int maxLevel=2;
        public ChessBoard chessBoard;
        public ChessState chessState;
        private int playerFlag;
        private void Start()
        {
            chessBoard.OnTurn += ChessBoard_OnTurn;
            playerFlag = chessState == ChessState.WhiteChess ? 1 : 2; //白色棋为0 黑色棋为2
        }

        private void ChessBoard_OnTurn(int playerIndex)
        {

            if (chessBoard.playerIndex % 2 != 0)
            {
                ALPerform(chessBoard.grids, 2, out aiX, out aiY);
                chessBoard.grids[aiX, aiY].chessState = ChessState.BlackChess;
                chessBoard.grids[aiX, aiY].weightedValues = 2;
                chessBoard.grids[aiX, aiY].isPlaced = true;
                chessBoard.grids[aiX, aiY].DrawSpere();
                chessBoard.OnPlayerPlayChess(new Vector2Int(aiX, aiY), chessState);
            }
        }



        #endregion
    }
}


