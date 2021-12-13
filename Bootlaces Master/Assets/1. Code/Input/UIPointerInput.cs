using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointerInput : MonoBehaviour, IPointerInput, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector2 Position { get; private set; }
    
    public Vector2 Delta { get; private set; }
    
    public event Action Pressed;
    public event Action Moved;
    public event Action Released;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Position = eventData.position;
        Delta = Vector2.zero;
        Pressed?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Position = eventData.position;
        Delta = eventData.delta;
        Moved?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Position = eventData.position;
        Delta = Vector2.zero;
        Released?.Invoke();
    }
}