using Common.UnityUtils;
using RogueDungeon.Animations;
using RogueDungeon.Camera;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerRootGameObject : MonoBehaviour, IRootObject<Player>, IRootObject<AnimationPlayer>, IRootObject<GameCamera>
    {
        [SerializeField] private GameObject _animationRoot;
        [SerializeField] private GameObject _cameraRoot;
        GameObject IRootObject<AnimationPlayer>.GameObject => _animationRoot;
        GameObject IRootObject<Player>.GameObject => gameObject;
        GameObject IRootObject<GameCamera>.GameObject => _cameraRoot;
    }
}