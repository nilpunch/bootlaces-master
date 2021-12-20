using System;
using UnityEngine;

namespace Code
{
    public class EventSource : MonoBehaviour
    {
        public event Action Event;

        public void FireEvent()
        {
            Event?.Invoke();
        }
    }
    
    public class EventTarget : MonoBehaviour
    {
        public void OnEventFired()
        {
        }
    }

    public class EventSourceMediator : MonoBehaviour
    {
        private EventSource _eventSource;
        private EventTarget _eventTarget;

        public void Subscribe(EventSource eventSource, EventTarget eventTarget)
        {
            _eventSource = eventSource;
            _eventTarget = eventTarget;
            _eventSource.Event += _eventTarget.OnEventFired;
        }
    }

    public class SelfSubscribingTarget : EventTarget
    {
        private EventSource _eventSource;
        
        public void Subscribe(EventSource eventSource)
        {
            _eventSource = eventSource;
            _eventSource.Event += OnEventFired;
        }
    }
}