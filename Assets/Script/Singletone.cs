using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 싱글톤 클래스
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : class
{
    protected static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = System.Activator.CreateInstance(typeof(T)) as T;
            }
            return instance;
        }
    }
}