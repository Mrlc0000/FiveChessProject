/**
*Copyright(C) 2015 by 面试
*All rights reserved.
*FileName: Board.cs
*Author: 刘成
*Version: 1.5
*UnityVersion：2017.4.3f1
*Date: 2019-02-16
*Description:
*History:
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiveChess
{



    public class ChessBoard : MonoBehaviour
    {

        #region Editor
        //行列数
        public Vector2Int RanksCounts
        {
            set
            {
                ranksCounts = value;
            }
            get { return ranksCounts; }
        }
        [HideInInspector]
        public Vector2Int ranksCounts;

        ////格子大小
        public Vector2 GridSize
        {

            set
            {
                gridSize = value;
                ChangeGridSize();
            }
            get { return gridSize; }
        }
        [HideInInspector]
        public Vector2 gridSize;

        //间隔 x,y轴
        public Vector2 Spaceing
        {
            set
            {
                spaceing = value;
                ChangeSpaceing();
            }
            get { return spaceing; }
        }
        [HideInInspector]
        public Vector2 spaceing;
        public ChessGird[,] grids;
        public List<ChessGird> chessGirds;
        public bool isShowLine;
        /// <summary>
        /// 设置棋盘
        /// </summary>
        public void SetBoard()
        {

            if (grids != null)
            {
                Debug.Log(string.Concat('行', grids.GetLength(0), "列", grids.GetLength(1)));
                for (int x = 0; x < grids.GetLength(0); x++)
                {
                    for (int y = 0; y < grids.GetLength(1); y++)
                    {
                        DestroyImmediate(grids[x, y].gameObject);

                    }
                }
                grids = null;
            }
            grids = new ChessGird[RanksCounts.x, RanksCounts.y];
            chessGirds = new List<ChessGird>();
            for (int x = 0; x < RanksCounts.x; x++)
            {
                for (int y = 0; y < RanksCounts.y; y++)
                {
                    grids[x, y] = NewGridGameObject(string.Concat("grid", x, '_', y));
                    Debug.Log(grids[x, y].gameObject.name);
                    grids[x, y].transform.SetParent(GameObject.Find("Board").transform);
                    grids[x, y].gameObject.transform.localPosition = new Vector3(x * GridSize.x, y * GridSize.y);
                    grids[x, y].index.x = x;
                    grids[x, y].index.y = y;
                    grids[x, y].board = this;
                    chessGirds.Add(grids[x, y]);
                }
            }
        }
        /// <summary>
        /// 清除棋盘
        /// </summary>
        public void ClearBoard()
        {
            if (chessGirds == null)
                return;
            for (int i = 0; i < chessGirds.Count; i++)
            {
                DestroyImmediate(chessGirds[i].gameObject);
            }
            grids = null;
            chessGirds.Clear();
            chessGirds = null;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i >= transform.childCount)
                    break;

                if (transform.GetChild(i) == null)
                    break;
                DestroyImmediate(transform.GetChild(i));
            }
        }
        /// <summary>
        /// 更改gridSize自动更新 
        /// </summary>
        public void ChangeGridSize()
        {
            if (grids == null)
                return;
            for (int x = 0; x < grids.GetLength(0); x++)
            {
                for (int y = 0; y < grids.GetLength(1); y++)
                {
                    if (grids[x, y] != null)
                    {
                        grids[x, y].GetComponent<BoxCollider>().size = GridSize;
                    }

                }
            }
        }

        public void ChangeSpaceing()
        {
            if (grids == null)
                return;
            for (int x = 0; x < grids.GetLength(0); x++)
            {
                for (int y = 0; y < grids.GetLength(1); y++)
                {
                    if (grids[x, y] != null)
                    {
                        grids[x, y].gameObject.transform.localPosition = new Vector3(x * Spaceing.x, y * Spaceing.y);


                    }
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (!isShowLine)
                return;
            if (grids == null)
                return;
            Gizmos.color = Color.red;
            for (int x = 0; x < grids.GetLength(0); x++)
            {
                for (int y = 0; y < grids.GetLength(1); y++)
                {
                    if (y + 1 < grids.GetLength(1))
                    {
                        Gizmos.DrawLine(grids[x, y].gameObject.transform.localPosition, grids[x, y + 1].gameObject.transform.localPosition);
                    }
                    if (x + 1 < grids.GetLength(0))
                    {
                        Gizmos.DrawLine(grids[x, y].gameObject.transform.localPosition, grids[x + 1, y].gameObject.transform.localPosition);
                    }
                }
            }
        }
        /// <summary>
        ///生成新的grid
        /// </summary>
        private ChessGird NewGridGameObject(string name)
        {
            GameObject obj = new GameObject(name);

            ChessGird grid = obj.AddComponent<FiveChess.ChessGird>();
            obj.AddComponent<BoxCollider>().isTrigger = true;
            return grid;
        }


        #endregion

        #region 事件库


        public Action<Vector2Int, ChessState> OnPlayerPlayChess;//触发玩家棋的位置
        public event Action OnWin;//触发玩家棋的位置
        public event Action<Vector2Int> OnPlayerRegret;//触发悔棋的事件 因面试题暂不开发
        public Stack<Vector2Int> pastedStep;

        /// <summary>
        /// OnPlayerPlayChess回调
        /// </summary>
        /// <param name="obj"></param>
        private void ChessBoard_OnPlayerPlayChess(Vector2Int index, ChessState chessState)
        {
            //TODO判定是否是否胜利
            if (JudeWinOrContinue(index, chessState) == true)
            {

                Debug.Log(chessState + "Win");
                if (OnWin != null)
                {
                    OnWin.Invoke();//执行结束的事件 剔除棋盘格子的射线事件
                }
                UIManager.Instance.chessState = chessState;
                UIManager.Instance.gameStatus = GameStatus.Over;
                return;
            }

            playerIndex++;//增加回合 


            //TODO增加栈 来储存10步内的悔棋。

        }
        /// <summary>
        /// 判定是否胜利
        /// </summary>
        private bool JudeWinOrContinue(Vector2Int index, ChessState chess)
        {

            int x = index.x;
            int y = index.y;
            int i = index.x, j = index.y;
            int count = 0; //棋子计数器
                           /*计算水平方向连续棋子个数*/
            while (i > -1 && grids[i, j].chessState == chess)
            {
                i--;
                count++; //累加左侧
            }
            i = x + 1;
            while (i < 15 && grids[i, j].chessState == chess)
            {
                i++;
                count++; //累加右侧
            }
            if (count >= ConstKey.WinNum)
                return true; //获胜

            /*计算竖直方向连续棋子个数*/
            i = x;
            count = 0;
            while (j > -1 && grids[i, j].chessState == chess)
            {
                j--;
                count++; //累加上方
            }
            j = y + 1;
            while (j < 15 && grids[i, j].chessState == chess)
            {
                j++;
                count++; //累加下方
            }
            if (count >= ConstKey.WinNum)
                return true; //获胜

            /*计算左上右下方向连续棋子个数*/
            j = y;
            count = 0;
            while (i > -1 && j > -1 && grids[i, j].chessState == chess)
            {
                i--;
                j--;
                count++; //累加左上
            }
            i = x + 1;
            j = y + 1;
            while (i < 15 && j < 15 && grids[i, j].chessState == chess)
            {
                i++;
                j++;
                count++; //累加右下
            }
            if (count >= ConstKey.WinNum)
                return true; //获胜

            /*计算右上左下方向连续棋子个数*/
            i = x;
            j = y;
            count = 0;
            while (i < 15 && j > -1 && grids[i, j].chessState == chess)
            {
                i++;
                j--;
                count++; //累加右上
            }
            i = x - 1;
            j = y + 1;
            while (i > -1 && j < 15 && grids[i, j].chessState == chess)
            {
                i--;
                j++;
                count++; //累加左下
            }
            if (count >= ConstKey.WinNum)
                return true; //获胜

            return false; //该步没有取胜
        }
        #endregion

        #region Unity回调

        public int playerIndex;//回合制

        private void Start()
        {
            //防止二维数组 editor模式下 不能保存的问题
            grids = new ChessGird[RanksCounts.x, RanksCounts.y];
            //初始化
            for (int i = 0; i < chessGirds.Count; i++)
            {
                grids[chessGirds[i].index.x, chessGirds[i].index.y] = chessGirds[i];
            }
            OnPlayerPlayChess += ChessBoard_OnPlayerPlayChess;


        }


        #region 棋盘渲染
        public Material mat;
        void OnPostRender()
        {
            if (!mat)
            {
                Debug.LogError("Please Assign a material on the inspector");
                return;
            }
            GL.PushMatrix(); //保存当前Matirx
            mat.SetPass(0); //刷新当前材质
            GL.LoadPixelMatrix();//设置pixelMatrix
            GL.Color(Color.white);
            GL.Begin(GL.LINES);
            for (int x = 0; x < grids.GetLength(0); x++)
            {
                for (int y = 0; y < grids.GetLength(1); y++)
                {
                    if (y + 1 < grids.GetLength(1))
                    {

                        Vector3 f = Camera.main.WorldToScreenPoint(grids[x, y].gameObject.transform.position);
                        Vector3 t = Camera.main.WorldToScreenPoint(grids[x, y + 1].gameObject.transform.position);

                        GL.Vertex3(f.x, f.y, 0);
                        GL.Vertex3(t.x, t.y, 0);
                    }
                    if (x + 1 < grids.GetLength(0))
                    {
                        Vector3 f = Camera.main.WorldToScreenPoint(grids[x, y].gameObject.transform.position);
                        Vector3 t = Camera.main.WorldToScreenPoint(grids[x + 1, y].gameObject.transform.position);
                        GL.Vertex3(f.x, f.y, 0);
                        GL.Vertex3(t.x, t.y, 0);
                    }
                }
            }
            GL.End();
            GL.PopMatrix();//读取之前的Matrix
        }
        #endregion


        #endregion


    }
}
