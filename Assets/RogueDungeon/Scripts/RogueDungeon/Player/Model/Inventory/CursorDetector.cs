using RogueDungeon.Camera;
using RogueDungeon.Input;
using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class CursorDetector
    {
        private readonly IGameCamera _camera;
        private readonly IPlayerInput _input;

        public CursorDetector(IGameCamera camera, IPlayerInput input)
        {
            _camera = camera;
            _input = input;
        }

        public bool RectContainsCursor(RectTransform rectTransform) => 
            RectTransformUtility.RectangleContainsScreenPoint(rectTransform, _input.CursorPos, _camera.Camera);
    }
}