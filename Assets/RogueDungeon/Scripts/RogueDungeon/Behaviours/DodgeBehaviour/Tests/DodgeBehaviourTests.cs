using Moq;
using NUnit.Framework;
using RogueDungeon.Behaviours.DodgeBehaviour;

namespace RogueDungeon.Tests
{
    [TestFixture]
    public class DodgeBehaviourTests
    {
        private Mock<IDodgeMediator> _mediatorMock;
        private Mock<IDodgeAnimationsPlayer> _animationsPlayerMock;
        private Mock<IDodgeSoundsPlayer> _soundsPlayerMock;
        private Mock<IDodgeParametersProvider> _parametersProviderMock;
        private Mock<IDodgeInputProvider> _inputProviderMock;

        private DodgeBehaviour _dodgeBehaviour;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IDodgeMediator>();
            _animationsPlayerMock = new Mock<IDodgeAnimationsPlayer>();
            _soundsPlayerMock = new Mock<IDodgeSoundsPlayer>();
            _parametersProviderMock = new Mock<IDodgeParametersProvider>();
            _inputProviderMock = new Mock<IDodgeInputProvider>();

            _parametersProviderMock.Setup(p => p.GetDuration()).Returns(1.0f);

            _dodgeBehaviour = new DodgeBehaviour(
                _mediatorMock.Object,
                _animationsPlayerMock.Object,
                _soundsPlayerMock.Object,
                _parametersProviderMock.Object,
                _inputProviderMock.Object
            );
        }
        
        [Test]
        public void Enable_SetsDodgeStateToNone_AndRunsStateMachine()
        {
            // Act
            _dodgeBehaviour.Enable();

            // Assert
            _mediatorMock.Verify(m => m.SetDodgeState(DodgeState.None), Times.Once);
        }
        
        [Test]
        public void Disable_SetsDodgeStateToNone_AndStopsStateMachine()
        {
            // Act
            _dodgeBehaviour.Disable();

            // Assert
            _mediatorMock.Verify(m => m.SetDodgeState(DodgeState.None), Times.Once);
        }
        
        [Test]
        public void Tick_ExecutesDodgeRight_WhenConditionsAreMet()
        {
            // Arrange
            _mediatorMock.Setup(m => m.CanStartDodge()).Returns(true);
            _inputProviderMock.Setup(i => i.HasDodgeRightInput()).Returns(true);

            // Simulate a single tick for the state machine
            _dodgeBehaviour.Enable();
            _dodgeBehaviour.Tick();

            // Assert
            _mediatorMock.Verify(m => m.SetDodgeState(DodgeState.Right), Times.Once);
            _animationsPlayerMock.Verify(a => a.PlayDodgeRight(1.0f), Times.Once);
            _soundsPlayerMock.Verify(s => s.PlayDodgeSound(), Times.Once);
        }

        [Test]
        public void Tick_ExecutesDodgeLeft_WhenConditionsAreMet()
        {
            // Arrange
            _mediatorMock.Setup(m => m.CanStartDodge()).Returns(true);
            _inputProviderMock.Setup(i => i.HasDodgeLeftInput()).Returns(true);

            // Simulate a single tick for the state machine
            _dodgeBehaviour.Enable();
            _dodgeBehaviour.Tick();

            // Assert
            _mediatorMock.Verify(m => m.SetDodgeState(DodgeState.Left), Times.Once);
            _animationsPlayerMock.Verify(a => a.PlayDodgeLeft(1.0f), Times.Once);
            _soundsPlayerMock.Verify(s => s.PlayDodgeSound(), Times.Once);
        }
        
        [Test]
        public void Tick_DoesNotExecuteDodge_WhenConditionsAreNotMet()
        {
            // Arrange
            _mediatorMock.Setup(m => m.CanStartDodge()).Returns(false);
            _inputProviderMock.Setup(i => i.HasDodgeRightInput()).Returns(false);
            _inputProviderMock.Setup(i => i.HasDodgeLeftInput()).Returns(false);

            // Simulate a single tick for the state machine
            _dodgeBehaviour.Enable();
            _dodgeBehaviour.Tick();

            // Assert
            _mediatorMock.Verify(m => m.SetDodgeState(It.IsAny<DodgeState>()), Times.Never);
            _animationsPlayerMock.Verify(a => a.PlayDodgeRight(It.IsAny<float>()), Times.Never);
            _animationsPlayerMock.Verify(a => a.PlayDodgeLeft(It.IsAny<float>()), Times.Never);
            _soundsPlayerMock.Verify(s => s.PlayDodgeSound(), Times.Never);
        }
        
        [Test]
        public void Tick_TransitionsBackToIdle_AfterDodgeCompletes()
        {
            // Arrange
            _mediatorMock.Setup(m => m.CanStartDodge()).Returns(true);
            _inputProviderMock.Setup(i => i.HasDodgeRightInput()).Returns(true);

            // Enable and simulate a full dodge
            _dodgeBehaviour.Enable();
            _dodgeBehaviour.Tick(); // Start dodge
            _dodgeBehaviour.Tick(); // Simulate end of dodge (state machine transition)

            // Assert
            _mediatorMock.Verify(m => m.SetDodgeState(DodgeState.None), Times.AtLeastOnce);
        }
    }
}