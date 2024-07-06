using RogueDungeon.Assets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private const string ANIMATIONS_FOLDER = "Animations/Player/";
        
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameObject _animationRoot;
        
        [SerializeField,BoxGroup("Debug"), ReadOnly] private AnimationClip _animation;
        [SerializeField,BoxGroup("Debug"), ReadOnly] private string _animationPath = ANIMATIONS_FOLDER;

        private void Awake() => 
            _playerController.OnStateChanged += HandleStateChanged;

        private void OnDestroy() => 
            _playerController.OnStateChanged -= HandleStateChanged;

        private void HandleStateChanged()
        {
            _animationPath = ANIMATIONS_FOLDER + _playerController.CurrentState.Name;
            _animation = AssetProvider.Get<AnimationClip>(_animationPath);
        }

        private void LateUpdate()  
        {
            if (_animation != null)
                _animation.SampleAnimation(_animationRoot, (float)_playerController.CurrFrame / _playerController.CurrentState.Frames);
        }
    }
}