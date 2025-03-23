using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.UI.Bars
{
    public class ImageFillBar : Bar
    {
        [SerializeField] private Image _image;
        
        protected override void SetValue(float value) => 
            _image.fillAmount = value;
    }
}