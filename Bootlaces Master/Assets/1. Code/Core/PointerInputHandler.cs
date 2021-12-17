using System;
using UnityEngine;

namespace BootlacesMaster
{
    public abstract class PointerInputHandler : MonoBehaviour
    {
        public abstract bool OnPressed(Vector2 screenPosition);
        public abstract void OnMoved(Vector2 screenPosition);
        public abstract void OnReleased(Vector2 screenPosition);
    }
}