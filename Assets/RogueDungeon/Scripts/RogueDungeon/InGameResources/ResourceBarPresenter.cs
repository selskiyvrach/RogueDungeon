using System;
using Common.UI.Bars;
using UnityEngine;

namespace InGameResources
{
    public class ResourceBarPresenter : IDisposable
    {
        private readonly IReadOnlyResource _resource;
        private readonly IBar _bar;

        public ResourceBarPresenter(IReadOnlyResource resource, IBar bar)
        {
            _resource = resource;
            _bar = bar;
            _resource.OnChanged += UpdateBar;
            UpdateBar();
        }

        private void UpdateBar() => 
            _bar.Value = Mathf.Clamp01(_resource.Current / _resource.Max);

        public void Dispose() => 
            _resource.OnChanged -= UpdateBar;
    }
}