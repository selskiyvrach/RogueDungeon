using UnityEngine;
using UnityEngine.UI;

namespace Common.UI.Bars
{
    public class ImageFillBar : Bar
    {
        [SerializeField] private Image _image;
        
        public override float Value
        {
            get => _image.fillAmount;
            set => _image.fillAmount = value;
        }
    }
}