/**
*Copyright(C) 2015 by 面试
*All rights reserved.
*FileName: Gird.cs
*Author: 刘成
*Version: 1.5
*UnityVersion：2017.4.3f1
*Date: 2019-02-16
*Description: 棋盘内的格子 用于判定是存储位置和防止棋盘
*History:
*/
using UnityEngine;
using UnityEngine.EventSystems;
namespace FiveChess
{



    public class ChessGird : MonoBehaviour, IPointerClickHandler
    {
        public Vector2Int index;//二维索引值
        public bool isPlaced;
        public ChessBoard board;//棋盘用于管理
        public ChessState chessState=ChessState.None;//判定 0 none 1白棋 2黑棋 该棋盘的子
        public int weightedValues = 0;//权重值
        private bool isOver=false;


        public void Start()
        {
            board.OnWin += Board_OnWin;
        }


        private void Board_OnWin()
        {
            isOver = true;
        }

        private void OnDisable()
        {
            board.OnWin -= Board_OnWin; //切换场景或者摧毁的时候删除该事件
        }

        /// <summary>
        /// 用于放置棋
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (isOver)
                return;

            //TODO 将点击事件触发给board做下子的触发
            if (isPlaced)
            {
                Debug.Log(string.Concat("该位置已下", index));
                return;
            }
          //  Debug.Log(string.Concat("下棋点击事件", index));

            if (board.playerIndex % 2 == 0)
            {
                //白棋
                chessState = ChessState.WhiteChess;
                weightedValues = 1;
                //该位置绘制一个白棋
            }
            else
            {
                //黑棋
                chessState = ChessState.BlackChess;
                weightedValues = 2;
                //该位置绘制一个黑棋
            }
            if (board != null)
            {
                board.OnPlayerPlayChess(index, chessState);
            }
            DrawSpere();//绘制棋牌
            isPlaced = true;
        }
        /// <summary>
        /// 悔棋 如果是该步骤实现的则执行此方法
        /// </summary>
        public void Regret()
        {
            isPlaced = false;
            chessState = ChessState.None;
            //清除绘制
        }
        /// <summary>
        /// 绘制黑白棋
        /// </summary>
        public void DrawSpere()
        {

            SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
            {
                spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            }
              transform.localScale = Vector2.one * 0.3f;
             string path= chessState == ChessState.WhiteChess ? "White" : "Black";
             Sprite sprite=  Resources.Load<Sprite>(path);
             spriteRenderer.sprite = sprite;

            //防止内存泄漏
            sprite = null;
            spriteRenderer = null;
        }
    }
}
