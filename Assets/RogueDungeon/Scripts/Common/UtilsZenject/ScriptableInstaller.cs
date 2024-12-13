using UnityEngine;
using Zenject;

namespace Common.ZenjectUtils
{
    public class ScriptableInstaller : ScriptableObject
    {
        [SerializeField] private ScriptableInstaller[] _otherInstallers;

        public virtual void Install(DiContainer container)
        {
            foreach (var installer in _otherInstallers) 
                installer.Install(container);
        }
    }
}