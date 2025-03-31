using UnityEngine;

namespace Common.UI.Bars
{
    public class DottedBar : Bar
    {
        [SerializeField] private ShowHideHandler[] _elements;
        
        protected override void SetValue(float value)
        {
            for (var i = 0; i < _elements.Length; i++)
            {
                var element = _elements[i];
                var enable = i / (float)_elements.Length < value;
                
                if (element.IsShown == enable) 
                    continue;
                
                if(enable)
                    element.Show();
                else
                    element.Hide();
            }
        }
    }
}