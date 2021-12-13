using System;
using UnityEngine;

public class TouchPointerInput : MonoBehaviour, IPointerInput
{
    public Vector2 Position { get; private set; }
    
    public Vector2 Delta { get; private set; }
    
    public event Action Pressed;
    
    public event Action Moved;
    
    public event Action Released;

    private void Update()
    {
        if (UnityEngine.Input.touchCount == 0)
            return;

        Touch touch = UnityEngine.Input.GetTouch(0);

        Delta = touch.position - Position;
        Position = touch.position;
        
        switch (touch.phase)
        {
            case TouchPhase.Began:
                Pressed?.Invoke();
                break;
            case TouchPhase.Moved:
                Moved?.Invoke();
                break;
            case TouchPhase.Ended: case TouchPhase.Canceled:
                Released?.Invoke();
                break;
        }
    }
}