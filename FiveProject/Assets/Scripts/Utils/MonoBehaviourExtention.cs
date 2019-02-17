
/** 
 *Copyright(C) 2018 by 蓝智纵横科技有限公司 
 *All rights reserved. 
 *FileName:     MonoBehaviourExtention.cs 
 *Author:       刘成 
 *Version:      1.5 
 *UnityVersion：2017.2.0f3 
 *Date:         2018-07-04 
 *Description:    mono的一些静态拓展类 和一些通用方法的集合
 *History: 
*/


using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;


public static class MonoBehaviourExtention
{

    /// <summary>
    /// 倒计时 mono拓展协程
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="timeInt"></param>
    /// <param name="timeText"></param>
    /// <returns></returns>
    public static IEnumerator IETimeCountDown(this MonoBehaviour mono,int timeInt,Text timeText) {
        for (int i = timeInt; i >=0; i--)
        {
            yield return new WaitForSeconds(1);
            timeText.text = TimeCountStrText(i);
        }
    }
    /// <summary>
    /// 时间倒计时  00转 00:00格式变换
    /// </summary>
    /// <param name="timeInt"></param>
    /// <returns></returns>
    public static string TimeCountStrText(int timeInt)
    {

        string second = (timeInt % 60) <= 9 ? "0" + (timeInt % 60).ToString() : (timeInt % 60).ToString();
        string minute = (timeInt / 60) <= 9 ? "0" + (timeInt / 60).ToString() : (timeInt / 60).ToString();
        string TimeText = minute + ":" + second;
        return TimeText;

    }


    /// <summary>
    /// 加载在线的是图片
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="url"></param>
    /// <param name="image"></param>
    public static void LoadImageFromWWW(this MonoBehaviour mono, string url, RawImage image)
    {
        mono.StartCoroutine(LoadTexture(url, image));
    }
    private static IEnumerator LoadTexture(string url, RawImage image)
    {
        WWW www = new WWW(url);

        while (!www.isDone)
        {
            yield return www;
        }
        Texture2D texture = null;
        if (www.texture != null)
        {
            texture = www.texture;
        }
        image.texture = texture;
        image.color = Color.white;
    }

    /// <summary>
    /// mono拓展 延时方法
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="time">时间</param>
    /// <param name="func">委托方法</param>
    public static void Wait(this MonoBehaviour mono, float time, Action func)
    {

        mono.StartCoroutine(Func(time, func));
    }
    private static IEnumerator Func(float time, Action func)
    {
        yield return new WaitForSeconds(time);
        if (func != null)
        {
            func.Invoke();
        }
    }

    /// <summary>
    /// 设置所有子物体的可见
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="isActive"></param>
    public static void SetAllChildActive(Transform obj, bool isActive)

    {
        foreach (var item in obj)
        {
            obj.gameObject.SetActive(isActive);

        }
    }

    /// <summary>
    /// 设置所有子物体的脚本可见
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="isEnable"></param>
    public static void SetComponentEnableAllChild<T>(Transform trans, bool isEnable)
        where T : MonoBehaviour
    {
        foreach (Transform item in trans)
        {
            if (!item.GetComponent<T>())
                continue;

            item.GetComponent<T>().enabled = isEnable;


        }

    }
    /// <summary>
    /// 拓展类 字典获根据索引获取值
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="Tvalue"></typeparam>
    /// <param name="dic"></param>
    /// <param name="tkey"></param>
    /// <returns></returns>
    public static Tvalue DictionaryGetValue<Tkey, Tvalue>(Dictionary<Tkey, Tvalue> dic, Tkey tkey)
   where Tvalue : UnityEngine.Object
    {
        Tvalue obj;
        dic.TryGetValue(tkey, out obj);
        //as一定要有约束
        return obj as Tvalue;
    }

    /// <summary>
    /// 特效延时播放
    /// </summary>
    /// <param name="monobehaciour"></param>
    /// <param name="obj"></param>
    /// <param name="time"></param>
    public static void SetActiveFalse(MonoBehaviour monobehaciour, GameObject obj, float time)
    {
        obj.SetActive(true);
        Wait(monobehaciour, time, () => { obj.SetActive(false); });
    }


    /// <summary>
    /// 数组拓展类
    /// </summary>
    public static void SetActiveTrueOtherFalse(GameObject[] list, int index)

    {

        for (int i = 0; i < list.Length; i++)
        {
            if (i.Equals(index))
            {
                list[i].SetActive(true);

            }
            else
            {
                list[i].SetActive(false);
            }

        }
    }

    /// <summary>
    /// 设置所有list<GameObject> 是否可见
    /// </summary>
    /// <param name="list"></param>
    /// <param name="isActive"></param>
    public static void SetListAllActive(List<GameObject> list, bool isActive)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(isActive);
        }
    }
    public static void SetListAllActive<T>(List<T> list, bool isActive)
        where T : MonoBehaviour
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(isActive);
        }
    }


  


#if UNITY_EDITOR
    public static string PlatformPath = Application.streamingAssetsPath;
#elif UNITY_STANDALONE_WIN
      public static string PlatformPath = Application.streamingAssetsPath;
#elif UNITY_ANDROID
    public static string PlatformPath =   jar:file://"+Application.dataPath+"!/assets/";
#elif UNITY_IOS
       public static string PlatformPath =  Application.dataPath+"/Ray";
#endif
}

