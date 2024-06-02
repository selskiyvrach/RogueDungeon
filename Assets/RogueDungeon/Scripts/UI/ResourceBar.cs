using RogueDungeon.CharacterResource;
using UnityEngine;
using UnityEngine.UI;

namespace RogueDungeon.UI
{
    public class ResourceBar : MonoBehaviour, IResourceDisplay
    {
        [SerializeField] private Image _fill;
        [SerializeField] private Image _diff;
        [SerializeField] private int _diffCatchingUpFrames = 40;

        private float _diffInitialValue;
        private int _catchingUpFramesLeft;
        
        public void HandleChanged(Resource resource, ResourceChangeReason _)
        {
            _fill.fillAmount = resource.Current / resource.Max;
            if (_fill.fillAmount > _diff.fillAmount)
                _diff.fillAmount = _fill.fillAmount;
            else
            {
                _diffInitialValue = _diff.fillAmount;
                _catchingUpFramesLeft = _diffCatchingUpFrames;
            }
        }

        public void Tick()
        {
            if(_catchingUpFramesLeft == 0)
                return;
            
            if(_catchingUpFramesLeft-- > _diffCatchingUpFrames / 2)
                return;

            var normValue = 1 - _catchingUpFramesLeft / ((float)_diffCatchingUpFrames / 2);
            _diff.fillAmount = Mathf.Lerp(_diffInitialValue, _fill.fillAmount, normValue);
        }
    }
}