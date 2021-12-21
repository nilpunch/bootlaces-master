using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    public class InputQuery : MonoBehaviour
    {
        [SerializeField] private CrossPlatformPointerInput _input = null;
        [SerializeField] private List<PointerInputHandler> _inputHandlersQuery = null;

        private bool _pressed = false;
        
        private void OnEnable()
        {
            _input.Pressed += OnPressed;
            _input.Moved += OnMoved;
            _input.Released += OnReleased;
        }

        private void OnDisable()
        {
            OnReleased();
            
            _input.Pressed -= OnPressed;
            _input.Moved -= OnMoved;
            _input.Released -= OnReleased;
        }

        private void OnPressed()
        {
            foreach (var inputHandler in _inputHandlersQuery)
                if (inputHandler.OnPressed(_input.Position))
                    break;

            _pressed = true;
        }

        private void OnMoved()
        {
            if (_pressed == false)
                return;
            
            foreach (var inputHandler in _inputHandlersQuery)
                inputHandler.OnMoved(_input.Position);
        }

        private void OnReleased()
        {
            if (_pressed == false)
                return;
            
            _pressed = false;
            
            foreach (var inputHandler in _inputHandlersQuery)
                inputHandler.OnReleased(_input.Position);
        }
    }
}