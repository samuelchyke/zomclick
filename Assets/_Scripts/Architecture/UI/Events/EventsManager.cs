using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Zenject;

// [CreateAssetMenu(fileName = "NewEvent", menuName = "Events")]
public class EventsManager : IInitializable
{
    private Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    public void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void TriggerEvent(string eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public void Initialize()
    {
        Debug.Log($"Events Manager Initialized");
    }
}

