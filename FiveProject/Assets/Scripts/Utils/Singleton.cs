/** 
 *Copyright(C) 2018 by 蓝智纵横科技有限公司 
 *All rights reserved. 
 *FileName:     Singleton.cs 
 *Author:       刘成 
 *Version:      1.5 
 *UnityVersion：2017.2.0f3 
 *Date:         2018-04-19 
 *Description:    单例系统
 *History: 
*/

using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _Instance;

    public static T Instance
    {
        get
        {

            if (_Instance == null)
            {


                _Instance = FindObjectOfType(typeof(T)) as T;
                if (_Instance == null)
                {
                    // 如果没有找到， 则新建一个
                    GameObject obj = new GameObject(typeof(T).Name);
                    // 对象不可见，不会被保存
                //    obj.hideFlags = HideFlags.HideAndDontSave;
       
                    // 强制转换为 T 
                    _Instance = obj.AddComponent(typeof(T)) as T;
                }
            }

            return _Instance;


        }
    }

    protected virtual void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this as T;
        }
        else
        {
            GameObject.Destroy(_Instance);
            _Instance = this as T;
        }
    }
    /// <summary>
    /// 切换场景的时候自动剔除
    /// </summary>
    void OnApplicationQuit()
    {
        if (_Instance != null)
        {
            GameObject.Destroy(_Instance);
            _Instance = null;
        }
    }

}

public class SingeltonDontDestory<T> : Singleton<T> where T : MonoBehaviour
{



    protected override void Awake()
    {

        // 如果单例为空，将当前对象赋值给它
        if (_Instance == null)
        {
            _Instance = this as T;
            // 指明切换场景时不会被删除
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject.GetComponent<T>()); // 如果已经有实例了，则直接删除自己 (单例不允许重复存在)
        }
    }
}

public class SingeltonCommon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    protected virtual void Awake()
    {

        instance = this as T;

    }
}








