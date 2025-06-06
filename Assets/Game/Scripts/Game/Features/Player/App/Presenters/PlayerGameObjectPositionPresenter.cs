using Game.Features.Player.App.ViewInterfaces;
using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Movesets.Movement;

namespace Game.Features.Player.App.Presenters
{
    public class PlayerGameObjectPositionPresenter
    {
        private readonly IPlayerGameObjectPositionView _positionView;
        private readonly LevelTraverserContext _levelTraverserContext;

        public PlayerGameObjectPositionPresenter(LevelTraverserContext levelTraverserContext, IPlayerGameObjectPositionView positionView)
        {
            _positionView = positionView;
            _levelTraverserContext = levelTraverserContext;
            _levelTraverserContext.OnRealPositionChanged += UpdatePosition;
            _levelTraverserContext.OnRealRotationChanged += UpdateRotation;
            UpdatePosition();
            UpdateRotation();
        }

        private void UpdatePosition() => 
            _positionView.SetPosition(_levelTraverserContext.RealPosition);

        private void UpdateRotation() => 
            _positionView.SetRotation(_levelTraverserContext.RealRotation);
    }
}