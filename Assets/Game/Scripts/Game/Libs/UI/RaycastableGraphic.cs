using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Libs.UI
{
    public class RaycastableGraphic : MonoBehaviour, IRaycastable
    {
        [SerializeField, ReadOnly] private Graphic _raycastGraphic;

        public bool Raycastable
        {
            get => _raycastGraphic.raycastTarget;
            set => _raycastGraphic.raycastTarget = value;
        }

        protected virtual void OnValidate() => 
            _raycastGraphic ??= GetComponent<Graphic>() ?? throw new Exception(name);
    }
}