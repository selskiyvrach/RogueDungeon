using System;
using Camera;
using UnityEngine;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class WorldCanvas : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Canvas _canvas;

        private void OnValidate() => 
            _canvas = GetComponent<Canvas>() ?? throw new Exception("Canvas is null");

        [Inject]
        private void Construct(IGameCamera gameCamera) => 
            _canvas.worldCamera = gameCamera.Camera;
    }
}