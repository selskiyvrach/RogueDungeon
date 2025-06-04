using Game.Features.Player.App.ViewInterfaces;
using UnityEngine;

namespace Game.Features.Player.Infrastructure.View
{
    public class PlayerGameObjectPositionView : MonoBehaviour, IPlayerGameObjectPositionView
    {
        public void SetPosition(Vector2 position) => 
            transform.position = new Vector3(position.x, 0, position.y);

        public void SetRotation(Vector2 rotation) => 
            transform.forward = new Vector3(rotation.x, 0, rotation.y);
    }
}