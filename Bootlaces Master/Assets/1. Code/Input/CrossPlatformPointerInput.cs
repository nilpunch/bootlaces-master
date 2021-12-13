using System;
using UnityEngine;

public class CrossPlatformPointerInput : MonoBehaviour, IPointerInput
{
    [Header("Dependencies")] 
    [SerializeField] private MousePointerInput _mouseInputRouter = null;
    [SerializeField] private TouchPointerInput _touchInputRouter = null;

#if UNITY_EDITOR
    private IPointerInput PointerInput => _mouseInputRouter;
    
#else
    private IPointerInput PointerInput => _touchInputRouter;
#endif


    public Vector2 Position => PointerInput.Position;

    public Vector2 Delta => PointerInput.Position;

    public event Action Pressed
    {
        add => PointerInput.Pressed += value;
        remove => PointerInput.Pressed -= value;
    }

    public event Action Moved
    {
        add => PointerInput.Moved += value;
        remove => PointerInput.Moved -= value;
    }

    public event Action Released
    {
        add => PointerInput.Released += value;
        remove => PointerInput.Released -= value;
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