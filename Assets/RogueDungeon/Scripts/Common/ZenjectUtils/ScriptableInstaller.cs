using UnityEngine;
using Zenject;

namespace Common.InstallerGenerator
{
    public class ScriptableInstaller : ScriptableObject
    {
        [SerializeField] private ScriptableInstaller[] _otherInstallers;
        protected DiContainer Container { get; private set; }

        public virtual void Install(DiContainer container)
        {
            Container = container;
            foreach (var installer in _otherInstallers) 
                installer.Install(container);
        }
    }
}