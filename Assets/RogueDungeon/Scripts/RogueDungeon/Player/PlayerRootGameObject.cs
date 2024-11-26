using Common.UnityUtils;
using RogueDungeon.Animations;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerRootGameObject : MonoBehaviour, IRootObject<Player>, IRootObject<AnimationPlayer>, IRootObject<UnityEngine.Camera>
    {
        [SerializeField] private GameObject _animationRoot;
        [SerializeField] private GameObject _cameraRoot;
        GameObject IRootObject<UnityEngine.Camera>.GameObject => _cameraRoot;
        GameObject IRootObject<AnimationPlayer>.GameObject => _animationRoot;
        GameObject IRootObject<Player>.GameObject => gameObject;
    }
}