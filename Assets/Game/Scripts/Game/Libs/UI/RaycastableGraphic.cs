using UnityEngine;
using UnityEngine.UI;

namespace Game.Libs.UI
{
    public class RaycastableGraphic : MonoBehaviour, IRaycastable
    {
        private Graphic _graphic;

        public bool Raycastable
        {
            get => _graphic.raycastTarget;
            set => _graphic.raycastTarget = value;
        }

        protected virtual void OnValidate() => 
            _graphic ??= GetComponent<Graphic>();
    }
}