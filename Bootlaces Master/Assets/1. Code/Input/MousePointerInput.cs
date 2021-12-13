using System;
using UnityEngine;

public class MousePointerInput : MonoBehaviour, IPointerInput
{
    public Vector2 Position { get; private set; }

    public Vector2 Delta { get; private set; }

    public event Action Pressed;

    public event Action Moved;

    public event Action Released;

    private void Update()
    {
        Delta = (Vector2)UnityEngine.Input.mousePosition - Position;
        
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            Position = UnityEngine.Input.mousePosition;
            Pressed?.Invoke();
        }

        if (UnityEngine.Input.GetMouseButton(0))
        {
            if (Position != (Vector2)UnityEngine.Input.mousePosition)
            {
                Position = UnityEngine.Input.mousePosition;
                Moved?.Invoke();
            }
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            Position = UnityEngine.Input.mousePosition;
            Released?.Invoke();
        }
    }
}