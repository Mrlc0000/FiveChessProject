/**
*Copyright(C) 2015 by 面试
*All rights reserved.
*FileName: UIManager.cs
*Author: 刘成
*Version: 1.5
*UnityVersion：2017.4.3f1
*Date: 2019-02-17
*Description:
*History:
*/

using UnityEngine;
using UnityEngine.SceneManagement;
namespace FiveChess
{

    public class UIManager : SingeltonDontDestory<UIManager>
    {

        public ChessState chessState;
        public GameStatus gameStatus;
        public bool isOver;
        private void OnGUI()
        {

            GUIStyle gUIStyle = new GUIStyle();

            gUIStyle.alignment = TextAnchor.MiddleCenter;
            switch (gameStatus)
            {
                case GameStatus.Start:
                    if (GUI.Button(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 50, 100, 50), "人机大战"))
                    {
                        SceneManager.LoadScene("FiveChessProject");
                        gameStatus = GameStatus.Game;
                    }
                    break;
                case GameStatus.Game:
      
                    if (GUI.Button(new Rect(0, 50, 50, 25), "返回"))
                    {
                        SceneManager.LoadScene("Start");
                        gameStatus = GameStatus.Start;
                    }
                    break;
                case GameStatus.Over:
                    string label = chessState == ChessState.WhiteChess ? "白旗获胜" : "黑棋获胜";
                    if (GUI.Button(new Rect((Screen.width / 2) - 50, (Screen.height / 2), 300, 50), label+"重新开始"))
                    {
                        SceneManager.LoadScene("Start");
                        gameStatus = GameStatus.Start;
                    }
                    break;
                default:
                    break;
            }

       


        }
    }
}
