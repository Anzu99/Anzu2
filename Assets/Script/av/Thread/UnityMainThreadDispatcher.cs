using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : AV_SingletonMono<UnityMainThreadDispatcher>
{
    private static Queue<Action> _actionQueue = new Queue<Action>();

    public void Update()
    {
        lock (_actionQueue)
        {
            while (_actionQueue.Count > 0)
            {
                Action action = _actionQueue.Dequeue();
                action.Invoke();
            }
        }
    }

    public void Enqueue(Action action)
    {
        lock (_actionQueue)
        {
            _actionQueue.Enqueue(action);
        }
    }
}