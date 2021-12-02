using System;
using UnityEngine;

public class InputRouter : MonoBehaviour, IPointerInputRouter
{
    [Header("Dependencies")] 
    [SerializeField] private MouseInputRouter _mouseInputRouter = null;
    [SerializeField] private TouchInputRouter _touchInputRouter = null;

#if UNITY_EDITOR
    private IPointerInputRouter PointerInputRouter => _mouseInputRouter;
    
#else
    private IPointerInputRouter PointerInputRouter => _touchInputRouter;
#endif


    public Vector2 Position => PointerInputRouter.Position;

    public Vector2 Delta => PointerInputRouter.Position;

    public event Action Pressed
    {
        add => PointerInputRouter.Pressed += value;
        remove => PointerInputRouter.Pressed -= value;
    }

    public event Action Moved
    {
        add => PointerInputRouter.Moved += value;
        remove => PointerInputRouter.Moved -= value;
    }

    public event Action Released
    {
        add => PointerInputRouter.Released += value;
        remove => PointerInputRouter.Released -= value;
    }
    
    private void Awake()
    {
#if UNITY_EDITOR
        _touchInputRouter.enabled = false;
#else
        _mouseInputRouter.enabled = false;
#endif
    }
}