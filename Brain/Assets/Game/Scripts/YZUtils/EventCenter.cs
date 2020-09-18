using System;
using System.Collections.Generic;

public class EventCenter
{
    private static Dictionary<UtilsEventType, Delegate> m_EventTable = new Dictionary<UtilsEventType, Delegate>();

    private static void OnListenerAdding(UtilsEventType utilsEventType, Delegate callBack)
    {
        if (!m_EventTable.ContainsKey(utilsEventType))
        {
            m_EventTable.Add(utilsEventType, null);
        }
        Delegate d = m_EventTable[utilsEventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception($"尝试为事件{utilsEventType}添加不同类型的委托，当前事件所对应的委托是{d.GetType()}，要添加的委托类型为{callBack.GetType()}");
        }
    }
    private static void OnListenerRemoving(UtilsEventType utilsEventType, Delegate callBack)
    {
        if (m_EventTable.ContainsKey(utilsEventType))
        {
            Delegate d = m_EventTable[utilsEventType];
            if (d == null)
            {
                throw new Exception($"移除监听错误：事件{utilsEventType}没有对应的委托");
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(
                    $"移除监听错误：尝试为事件{utilsEventType}移除不同类型的委托，当前委托类型为{d.GetType()}，要移除的委托类型为{callBack.GetType()}");
            }
        }
        else
        {
            throw new Exception($"移除监听错误：没有事件码{utilsEventType}");
        }
    }
    private static void OnListenerRemoved(UtilsEventType utilsEventType)
    {
        if (m_EventTable[utilsEventType] == null)
        {
            m_EventTable.Remove(utilsEventType);
        }
    }
    //no parameters
    public static void AddListener(UtilsEventType utilsEventType, Action callBack)
    {
        OnListenerAdding(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action)m_EventTable[utilsEventType] + callBack;
    }
    //Single parameters
    public static void AddListener<T>(UtilsEventType utilsEventType, Action<T> callBack)
    {
        OnListenerAdding(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T>)m_EventTable[utilsEventType] + callBack;
    }
    //two parameters
    public static void AddListener<T, TX>(UtilsEventType utilsEventType, Action<T, TX> callBack)
    {
        OnListenerAdding(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T, TX>)m_EventTable[utilsEventType] + callBack;
    }
    //three parameters
    public static void AddListener<T, TX, TY>(UtilsEventType utilsEventType, Action<T, TX, TY> callBack)
    {
        OnListenerAdding(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T, TX, TY>)m_EventTable[utilsEventType] + callBack;
    }
    //four parameters
    public static void AddListener<T, TX, TY, TZ>(UtilsEventType utilsEventType, Action<T, TX, TY, TZ> callBack)
    {
        OnListenerAdding(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T, TX, TY, TZ>)m_EventTable[utilsEventType] + callBack;
    }
    //five parameters
    public static void AddListener<T, TX, TY, TZ, TW>(UtilsEventType utilsEventType, Action<T, TX, TY, TZ, TW> callBack)
    {
        OnListenerAdding(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T, TX, TY, TZ, TW>)m_EventTable[utilsEventType] + callBack;
    }

    //no parameters
    public static void RemoveListener(UtilsEventType utilsEventType, Action callBack)
    {
        OnListenerRemoving(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action)m_EventTable[utilsEventType] - callBack;
        OnListenerRemoved(utilsEventType);
    }
    //single parameters
    public static void RemoveListener<T>(UtilsEventType utilsEventType, Action<T> callBack)
    {
        OnListenerRemoving(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T>)m_EventTable[utilsEventType] - callBack;
        OnListenerRemoved(utilsEventType);
    }
    //two parameters
    public static void RemoveListener<T, TX>(UtilsEventType utilsEventType, Action<T, TX> callBack)
    {
        OnListenerRemoving(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T, TX>)m_EventTable[utilsEventType] - callBack;
        OnListenerRemoved(utilsEventType);
    }
    //three parameters
    public static void RemoveListener<T, TX, TY>(UtilsEventType utilsEventType, Action<T, TX, TY> callBack)
    {
        OnListenerRemoving(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T, TX, TY>)m_EventTable[utilsEventType] - callBack;
        OnListenerRemoved(utilsEventType);
    }
    //four parameters
    public static void RemoveListener<T, TX, TY, TZ>(UtilsEventType utilsEventType, Action<T, TX, TY, TZ> callBack)
    {
        OnListenerRemoving(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T, TX, TY, TZ>)m_EventTable[utilsEventType] - callBack;
        OnListenerRemoved(utilsEventType);
    }
    //five parameters
    public static void RemoveListener<T, TX, TY, TZ, TW>(UtilsEventType utilsEventType, Action<T, TX, TY, TZ, TW> callBack)
    {
        OnListenerRemoving(utilsEventType, callBack);
        m_EventTable[utilsEventType] = (Action<T, TX, TY, TZ, TW>)m_EventTable[utilsEventType] - callBack;
        OnListenerRemoved(utilsEventType);
    }


    //no parameters
    public static void Broadcast(UtilsEventType utilsEventType)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(utilsEventType, out d))
        {
            Action callBack = d as Action;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception($"广播事件错误：事件{utilsEventType}对应委托具有不同的类型");
            }
        }
    }
    //single parameters
    public static void Broadcast<T>(UtilsEventType utilsEventType, T arg)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(utilsEventType, out d))
        {
            Action<T> callBack = d as Action<T>;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception($"广播事件错误：事件{utilsEventType}对应委托具有不同的类型");
            }
        }
    }
    //two parameters
    public static void Broadcast<T, TX>(UtilsEventType utilsEventType, T arg1, TX arg2)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(utilsEventType, out d))
        {
            Action<T, TX> callBack = d as Action<T, TX>;
            if (callBack != null)
            {
                callBack(arg1, arg2);
            }
            else
            {
                throw new Exception($"广播事件错误：事件{utilsEventType}对应委托具有不同的类型");
            }
        }
    }
    //three parameters
    public static void Broadcast<T, TX, TY>(UtilsEventType utilsEventType, T arg1, TX arg2, TY arg3)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(utilsEventType, out d))
        {
            Action<T, TX, TY> callBack = d as Action<T, TX, TY>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3);
            }
            else
            {
                throw new Exception($"广播事件错误：事件{utilsEventType}对应委托具有不同的类型");
            }
        }
    }
    //four parameters
    public static void Broadcast<T, TX, TY, TZ>(UtilsEventType utilsEventType, T arg1, TX arg2, TY arg3, TZ arg4)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(utilsEventType, out d))
        {
            Action<T, TX, TY, TZ> callBack = d as Action<T, TX, TY, TZ>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4);
            }
            else
            {
                throw new Exception($"广播事件错误：事件{utilsEventType}对应委托具有不同的类型");
            }
        }
    }
    //five parameters
    public static void Broadcast<T, TX, TY, TZ, TW>(UtilsEventType utilsEventType, T arg1, TX arg2, TY arg3, TZ arg4, TW arg5)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(utilsEventType, out d))
        {
            Action<T, TX, TY, TZ, TW> callBack = d as Action<T, TX, TY, TZ, TW>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4, arg5);
            }
            else
            {
                throw new Exception($"广播事件错误：事件{utilsEventType}对应委托具有不同的类型");
            }
        }
    }
    }


