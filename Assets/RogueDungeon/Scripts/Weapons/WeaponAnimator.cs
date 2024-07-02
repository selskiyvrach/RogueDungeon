using RogueDungeon.Assets;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace RogueDungeon.Weapons
{
    public class WeaponAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("_weapon")] [SerializeField] private PlayerController _playerController;
        private AnimationClip _animation;

        private void Awake() => 
            _playerController.OnStateChanged += HandleStateChanged;

        private void OnDestroy() => 
            _playerController.OnStateChanged -= HandleStateChanged;

        private void HandleStateChanged()
        {
            var path = "Animations/Weapons/" + _playerController.CurrentState.Name;
            _animation = AssetProvider.Get<AnimationClip>(path);
            Assert.IsNotNull(_animation, $"Animation not found by \"{path}\" on {name}");
        }

        private void LateUpdate()
        {
            if(_animation != null)
                _animation.SampleAnimation(gameObject, (float)_playerController.CurrFrame / _playerController.CurrentState.Frames);
        }
    }
}