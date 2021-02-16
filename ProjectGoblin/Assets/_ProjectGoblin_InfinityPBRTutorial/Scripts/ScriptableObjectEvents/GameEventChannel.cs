using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/GameEventChannel", 
    fileName = "New GameEventChannel")]

public class GameEventChannel : ScriptableObject
{
    public List<GameEventListener> listeners = 
        new List<GameEventListener>();

    public void RaiseEvent()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null)
            {
                listeners[i].OnEventRaised();
            }
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void DeregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}

