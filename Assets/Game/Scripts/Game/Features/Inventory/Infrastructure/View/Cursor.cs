using System;
using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public interface ICursor
    {
        Vector2 ScreenPos { get; }
        event Action OnPosChanged;
    }

    public interface ICursorController
    {
        Vector2 ScreenPos { set; }
        void Enable();
        void Disable();
    }

    public class Cursor : MonoBehaviour, ICursor, ICursorController
    {
        private Vector2 _lastScreenPos;
        private Vector2 _screenPos;

        public Vector2 ScreenPos
        {
            get => _screenPos;
            set
            {
                _screenPos = value;
                OnPosChanged?.Invoke();
            }
        }

        public event Action OnPosChanged;

        public void Enable() => 
            gameObject.SetActive(true);

        public void Disable() => 
            gameObject.SetActive(false);
    }
}