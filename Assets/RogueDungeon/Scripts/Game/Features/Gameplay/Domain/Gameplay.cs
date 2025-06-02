using System;
using IInitializable = Libs.Lifecycle.IInitializable;
using ITickable = Libs.Lifecycle.ITickable;

namespace Game.Features.Gameplay.Domain
{
    public class Gameplay : IInitializable, ITickable
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Tick(float timeDelta)
        {
            throw new NotImplementedException();
        }
    }
}