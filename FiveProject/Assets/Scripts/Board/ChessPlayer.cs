/**
*Copyright(C) 2015 by 面试
*All rights reserved.
*FileName: ChessPlayer.cs
*Author: 刘成
*Version: 1.5
*UnityVersion：2017.4.3f1
*Date: 2019-02-17
*Description:
*History:
*/

using UnityEngine;

namespace FiveChess {
    public class ChessPlayer : MonoBehaviour {
        public GameObject EventObject;
        public ChessBoard chessBoard;
        public ChessState chessState;
        
        private void Start()
        {
            chessBoard.OnTurn += ChessBoard_OnTurn;
        }

        private void ChessBoard_OnTurn(int obj)
        {
               EventObject.SetActive(false);
            //白棋时候执行
            if (chessBoard.playerIndex % 2 == 0 && chessState == ChessState.WhiteChess)
                {
                    EventObject.SetActive(true);
                }
                if (chessBoard.playerIndex % 2 != 0 && chessState == ChessState.BlackChess)
                {
                    EventObject.SetActive(true);
                }
            }
        }
    
}
