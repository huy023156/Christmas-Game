using System;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher: MonoBehaviour {
    private static EventDispatcher instance;

    public static EventDispatcher Instance {
        get {
            if(instance == null) {
                instance = FindFirstObjectByType<EventDispatcher>();
                if(instance == null) {
                    GameObject go = new GameObject("EventDispatcher");
                    instance = go.AddComponent<EventDispatcher>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private Dictionary<Type,Action<IEventParam>> eventDictionary = new Dictionary<Type,Action<IEventParam>>();

    private void OnDestroy() {
    }

    public void AddEventListener<T>(Action<IEventParam> listener) where T : IEventParam {
        Type eventType = typeof(T);
        if(eventDictionary.TryGetValue(eventType,out Action<IEventParam> thisEvent)) {
            thisEvent += listener;
            eventDictionary[eventType] = thisEvent;
        } else {
            thisEvent += listener;
            eventDictionary.Add(eventType,thisEvent);
        }
    //    Debug.Log($"<color=yellow>Listener added for event type: {eventType}</color>");
    }

    public static void Add<T>(Action<IEventParam> listener) where T : IEventParam {
        Instance.AddEventListener<T>(listener);
    }

    public void RemoveEventListener<T>(Action<IEventParam> listener) where T : IEventParam {
        Type eventType = typeof(T);
        if(instance == null)
            return;
        if(eventDictionary.TryGetValue(eventType,out Action<IEventParam> thisEvent)) {
            thisEvent -= listener;
            eventDictionary[eventType] = thisEvent;
          //  Debug.Log($"<color=yellow>Listener removed for event type: {eventType}</color>");
        }
    }

    public static void Remove<T>(Action<IEventParam> listener) where T : IEventParam {
        instance?.RemoveEventListener<T>(listener);
    }

    public void DispatchEvent<T>(T eventParam) where T : IEventParam {
        Type eventType = typeof(T);
        if(eventDictionary.TryGetValue(eventType,out Action<IEventParam> thisEvent)) {
            thisEvent.Invoke(eventParam);
        }
    }

    public static void Dispatch<T>(T eventParam) where T : IEventParam {
        Instance.DispatchEvent(eventParam);
    }
}
