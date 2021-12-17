using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    public class PointerInputSplitter : MonoBehaviour
    {
        [SerializeField] private CrossPlatformPointerInput _input = null;
        [SerializeField] private List<PointerInputHandler> _inputHandlersQuery = null;
        
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
        }

        private void OnMoved()
        {
            foreach (var inputHandler in _inputHandlersQuery)
                inputHandler.OnMoved(_input.Position);
        }

        private void OnReleased()
        {
            foreach (var inputHandler in _inputHandlersQuery)
                inputHandler.OnReleased(_input.Position);
        }
    }
}