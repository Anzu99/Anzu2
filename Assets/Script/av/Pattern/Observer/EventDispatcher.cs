using System;
using System.Collections.Generic;
using UnityEngine;

public enum EventID
{
    None = 0,
    Win,
    Lose
}
public class EventDispatcher : MonoBehaviour
{
    #region Singleton
    static EventDispatcher s_instance;
    public static EventDispatcher Instance
    {
        get
        {
            if (s_instance == null)
            {
                GameObject singletonObject = new GameObject();
                s_instance = singletonObject.AddComponent<EventDispatcher>();
                singletonObject.name = "Singleton - EventDispatcher";
                Common.Log("Create singleton : {0}", singletonObject.name);
            }
            return s_instance;
        }
        private set { }
    }

    public static bool HasInstance()
    {
        return s_instance != null;
    }

    void Awake()
    {
        if (s_instance != null && s_instance.GetInstanceID() != this.GetInstanceID())
        {
            Common.Log("An instance of EventDispatcher already exist : <{1}>, So destroy this instance : <{2}>!!", s_instance.name, name);
            Destroy(gameObject);
        }
        else
        {
            s_instance = this as EventDispatcher;
        }
    }


    void OnDestroy()
    {
        if (s_instance == this)
        {
            ClearAllListener();
            s_instance = null;
        }
    }
    #endregion


    #region Fields
    Dictionary<EventID, Action<object>> _listeners = new Dictionary<EventID, Action<object>>();
    #endregion


    #region Add Listeners, Post events, Remove listener


    public void RegisterListener(EventID eventID, Action<object> callback)
    {
        Common.Assert(callback != null, "AddListener, event {0}, callback = null !!", eventID.ToString());
        Common.Assert(eventID != EventID.None, "RegisterListener, event = None !!");

        if (_listeners.ContainsKey(eventID))
        {
            _listeners[eventID] += callback;
        }
        else
        {
            _listeners.Add(eventID, null);
            _listeners[eventID] += callback;
        }
    }
    public void PostEvent(EventID eventID, object param = null)
    {
        if (!_listeners.ContainsKey(eventID))
        {
            Common.Log("No listeners for this event : {0}", eventID);
            return;
        }

        var callbacks = _listeners[eventID];
        if (callbacks != null)
        {
            callbacks(param);
        }
        else
        {
            Common.Log("PostEvent {0}, but no listener remain, Remove this key", eventID);
            _listeners.Remove(eventID);
        }
    }


    public void RemoveListener(EventID eventID, Action<object> callback)
    {
        Common.Assert(callback != null, "RemoveListener, event {0}, callback = null !!", eventID.ToString());
        Common.Assert(eventID != EventID.None, "AddListener, event = None !!");

        if (_listeners.ContainsKey(eventID))
        {
            _listeners[eventID] -= callback;
        }
        else
        {
            Common.Warning(false, "RemoveListener, not found key : " + eventID);
        }
    }


    public void ClearAllListener()
    {
        _listeners.Clear();
    }
    #endregion
}

#region Extension class

public static class EventDispatcherExtension
{
    public static void RegisterListener(this MonoBehaviour sender, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance.RegisterListener(eventID, callback);
    }

    public static void PostEvent(this MonoBehaviour sender, EventID eventID, object param)
    {
        EventDispatcher.Instance.PostEvent(eventID, param);
    }

    public static void PostEvent(this MonoBehaviour sender, EventID eventID)
    {
        EventDispatcher.Instance.PostEvent(eventID, null);
    }
    //public static void ReMoveListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    //{
    //	EventDispatcher.Instance.RemoveListener(eventID, callback);
    //}
}
#endregion

