using System;
using UnityEngine;

public interface IPointerInputRouter
{
    Vector2 Position { get; }
    Vector2 Delta { get; }
    
    event Action Pressed;
    event Action Moved;
    event Action Released;
}