/**
*Copyright(C) 2015 by 面试
*All rights reserved.
*FileName: FiveBoardEditor.cs
*Author: 刘成
*Version: 1.5
*UnityVersion：2017.4.3f1
*Date: 2019-02-16
*Description:
*History:
*/

using UnityEngine;
using FiveChess;
using UnityEditor;

[CustomEditor(typeof(ChessBoard))]
[CanEditMultipleObjects]
public class FiveBoardEditor : Editor
{
    [SerializeField]
    ChessBoard script;

    // Use this for initialization
    private void Awake()
    {
        script = (ChessBoard)target;
    }
    public override void OnInspectorGUI()
    {


        base.OnInspectorGUI();

        GUILayout.BeginVertical();
        //ranks 行列数
        //size 尺寸大小
        //Speace 空间更改

        script.RanksCounts = EditorGUILayout.Vector2IntField("RanksCounts", script.RanksCounts);
        script.GridSize = EditorGUILayout.Vector2Field("GridSize", script.GridSize);
        script.Spaceing = EditorGUILayout.Vector2Field("Spaceing", script.Spaceing);

       
 





        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("生成棋盘", GUILayout.MaxWidth(60), GUILayout.Height(20)))
        {

            script.SetBoard();
        }
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("清除棋盘", GUILayout.Width(60), GUILayout.Height(20)))
        {

            script.ClearBoard();
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("显示线条", GUILayout.Width(60), GUILayout.Height(20)))
        {

            script.isShowLine = !script.isShowLine;
        }


        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        if (script.grids != null)
        {
            for (int x = 0; x < script.grids.GetLength(0); x++)
            {
                GUILayout.BeginVertical();
                for (int y = script.grids.GetLength(1)-1; y >= 0; y--)
                {
                    EditorGUILayout.LabelField(string.Concat(x, "_",y),GUILayout.Width(40));
                    EditorGUILayout.ObjectField(script.grids[x, y], typeof(GameObject), true);
                }
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndHorizontal();




        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }


    }

   
}
