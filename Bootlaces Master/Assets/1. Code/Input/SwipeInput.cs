using System;
using JetBrains.Annotations;
using UnityEngine;

public enum SwipeDirection
{
    Left, 
    Right,
    Up,
    Down,
}

public class SwipeInput : MonoBehaviour
{
    [SerializeField] private float _distanceForSwipe = 20f;
    [SerializeField] private float _velocityForSwipe = 10f;
    [SerializeField] private bool _allowPingPongSwipe = false;

    [Header("Dependencies")]
    [SerializeField, UsedImplicitly] private MouseInputRouter _mouseInputRouter = null; 
    [SerializeField, UsedImplicitly] private TouchInputRouter _touchInputRouter = null; 
    
    private Vector2 _fingerLastPosition;
    private Vector2 _delta;
    private Vector2 _fingerVelocity;

    private SwipeDirection _lastSwipe;
    private bool _swipedOnce;
    
    private IPointerInputRouter _pointerInputRouter;
    
    public event Action<SwipeDirection> Swiped;

    private void Awake()
    {
        _swipedOnce = false;
        
#if UNITY_EDITOR
        _pointerInputRouter = _mouseInputRouter;
        _touchInputRouter.enabled = false;
#else
        _pointerInputRouter = _touchInputRouter;
        _mouseInputRouter.enabled = false;
#endif
    }

    private void OnEnable()
    {
        _pointerInputRouter.Pressed += OnPressed;
        _pointerInputRouter.Moved += OnMoved;
        _pointerInputRouter.Released += OnReleased;
    }
    
    private void OnDisable()
    {
        _pointerInputRouter.Pressed += OnPressed;
        _pointerInputRouter.Moved += OnMoved;
        _pointerInputRouter.Released += OnReleased;
    }

    private void OnPressed()
    {
        _swipedOnce = false;

        Vector2 normalized = ScreenToNormalized(_pointerInputRouter.Position);
        _fingerLastPosition = normalized;
    }
    
    private void OnMoved()
    {
        Vector2 normalized = ScreenToNormalized(_pointerInputRouter.Position);
        
        _fingerVelocity = normalized - _fingerLastPosition;
        _delta += _fingerVelocity;
        _delta = Vector2.ClampMagnitude(_delta, _distanceForSwipe + 0.01f);
        _fingerLastPosition = normalized;
        
        DetectSwipe();
    }
    
    private void OnReleased()
    {
        _fingerVelocity = Vector2.zero;
        _delta = Vector2.zero;
        _fingerLastPosition = ScreenToNormalized(_pointerInputRouter.Position);
        _swipedOnce = false;
    }

    private void DetectSwipe()
    {
        if (_swipedOnce && _allowPingPongSwipe == false)
            return;

        if (SwipeDistanceSatisfied())
        {
            SwipeDirection direction;

            if (IsVerticalSwipe())
                direction = _delta.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            else
                direction = _delta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;

            if (_swipedOnce && _allowPingPongSwipe && IsOppositeDirections(direction, _lastSwipe) == false)
                return;
            
            _lastSwipe = direction;
            Swiped?.Invoke(direction);
            _swipedOnce = true;

            _fingerVelocity = Vector3.zero;
            Vector2 normalized = ScreenToNormalized(_pointerInputRouter.Position);
            _delta = Vector2.zero;
            _fingerLastPosition = normalized;
            
            return;
        }

        if (SwipeVelocitySatisfied())
        {
            SwipeDirection direction;

            if (IsVerticalVelocity())
                direction = _fingerVelocity.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            else
                direction = _fingerVelocity.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
            
            if (_swipedOnce && _allowPingPongSwipe && IsOppositeDirections(direction, _lastSwipe) == false)
                return;
            
            _lastSwipe = direction;
            Swiped?.Invoke(direction);
            _swipedOnce = true;

            _fingerVelocity = Vector3.zero;
            Vector2 normalized = ScreenToNormalized(_pointerInputRouter.Position);
            _delta = Vector2.zero;
            _fingerLastPosition = normalized;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceSatisfied()
    {
        return VerticalMovementDistance() > _distanceForSwipe || HorizontalMovementDistance() > _distanceForSwipe;
    }
    
    private bool SwipeVelocitySatisfied()
    {
        return _fingerVelocity.magnitude >= _velocityForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(_delta.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(_delta.x);
    }
    
    private bool IsVerticalVelocity()
    {
        return Mathf.Abs(_fingerVelocity.y) > Mathf.Abs(_fingerVelocity.x);
    }

    private Vector2 ScreenToNormalized(Vector2 point)
    {
        Vector2 viewportPoint = new Vector2(point.x / Screen.width, point.y / Screen.height);

        viewportPoint.x *= (float)Screen.width / Screen.height;
        
        return viewportPoint;
    }

    private bool IsOppositeDirections(SwipeDirection first, SwipeDirection second)
    {
        return first == SwipeDirection.Down && second == SwipeDirection.Up ||
               first == SwipeDirection.Up && second == SwipeDirection.Down ||
               first == SwipeDirection.Left && second == SwipeDirection.Right ||
               first == SwipeDirection.Right && second == SwipeDirection.Left;
    }
}