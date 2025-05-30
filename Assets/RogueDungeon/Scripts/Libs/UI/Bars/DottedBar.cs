using System.Linq;
using UnityEngine;

namespace Libs.UI.Bars
{
    public class DottedBar : Bar
    {
        [SerializeField] private ShowHideHandler[] _elements;

        public override float Value
        {
            get => (float)_elements.Count(n => n.IsShown) / _elements.Length;
            set
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
}