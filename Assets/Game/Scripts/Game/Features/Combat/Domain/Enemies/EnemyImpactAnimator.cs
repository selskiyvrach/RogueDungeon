using Libs.Animations;
using Libs.Lifecycle;

namespace Game.Features.Combat.Domain.Enemies
{
    public class EnemyImpactAnimator : ITickable
    {
        private readonly IAnimation _animation;
        private bool _isPlaying;

        public EnemyImpactAnimator(IAnimation enemy) => 
            _animation = enemy;

        public void Tick(float timeDelta)
        {
            if(!_isPlaying)
                return;
            
            _animation.TickNormalizedTime(timeDelta * 4);
            if(_animation.IsFinished)
                _isPlaying = false;
        }

        public void OnHit()
        {
            if(_isPlaying)
                return;
            
            _animation.Play();
            _isPlaying = true;
        }
    }
}