/**
*Copyright(C) 2015 by 面试
*All rights reserved.
*FileName: ChessEnum.cs
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
    public enum WhoTurn                //先手顺序
    {
        PlayerGo = 0,
        AiGo = 1,
    }


    public enum ChessState          //落子情况
    {
        None = 0,
        BlackChess,
        WhiteChess,
    }

    public enum GameStatus {
        Start,
        Game,
        Over

    }

    public class ConstKey
    {
        public const int WinNum = 5;
    }
}
