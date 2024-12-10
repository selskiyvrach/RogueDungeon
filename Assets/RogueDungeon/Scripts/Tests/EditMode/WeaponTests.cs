using Moq;
using NUnit.Framework;
using RogueDungeon.Weapons;
using UniRx;

[TestFixture]
public class WeaponBehaviourTests
{
    private Mock<IAttackMediator> _mockMediator;
    private Mock<IWeaponInputProvider> _mockInputProvider;
    private Mock<IAttackComboCountAndTimingsConfig> _mockComboConfig;
    private Mock<IAttackTimingsProvider> _mockTimingsProvider;

    private WeaponBehaviour _weaponBehaviour;
    private ReactiveProperty<AttackState> _mockAttackState;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IAttackMediator>();
        _mockInputProvider = new Mock<IWeaponInputProvider>();
        _mockComboConfig = new Mock<IAttackComboCountAndTimingsConfig>();
        _mockTimingsProvider = new Mock<IAttackTimingsProvider>();

        // Setup default timings
        _mockTimingsProvider.Setup(t => t.GetPrepareDuration()).Returns(1f);
        _mockTimingsProvider.Setup(t => t.GetExecuteDuration()).Returns(1f);
        _mockTimingsProvider.Setup(t => t.GetFinishDuration()).Returns(1f);

        // Return the mocked timings provider
        _mockComboConfig.Setup(c => c.GetTimings(It.IsAny<int>())).Returns(_mockTimingsProvider.Object);
        _mockComboConfig.Setup(c => c.Count).Returns(3);

        _mockMediator.SetupProperty(m => m.AttackIndex, 0);
        _mockAttackState = new ReactiveProperty<AttackState>(AttackState.None);

        // Setup the mock to return the real ReactiveProperty
        _mockMediator.Setup(m => m.AttackState).Returns(_mockAttackState);
        _mockMediator.Setup(m => m.CanStartAttack()).Returns(true);

        _weaponBehaviour = new WeaponBehaviour(
            _mockMediator.Object,
            _mockInputProvider.Object,
            _mockComboConfig.Object);
    }

    [Test]
    public void Enable_ShouldInitializeState()
    {
        _weaponBehaviour.Enable();

        Assert.AreEqual(AttackState.None, _mockMediator.Object.AttackState.Value);
        Assert.AreEqual(0, _mockMediator.Object.AttackIndex);
    }

    [Test]
    public void Disable_ShouldResetState()
    {
        _weaponBehaviour.Enable();
        _weaponBehaviour.Disable();

        Assert.AreEqual(AttackState.None, _mockMediator.Object.AttackState.Value);
        Assert.AreEqual(0, _mockMediator.Object.AttackIndex);
    }

    [Test]
    public void Tick_ShouldAdvanceStateMachine()
    {
        _mockInputProvider.Setup(i => i.HasAttackInput()).Returns(true);

        _weaponBehaviour.Enable();
        _weaponBehaviour.Tick();

        _mockMediator.VerifySet(m => m.AttackState.Value = AttackState.Preparing, Times.Once);
    }

    [Test]
    public void TryStartNextComboAttack_ShouldIncrementComboIndex()
    {
        _mockInputProvider.Setup(i => i.HasAttackInput()).Returns(true);

        _weaponBehaviour.Enable();
        _weaponBehaviour.Tick(); // Preparing -> Executing
        _weaponBehaviour.Tick(); // Executing -> Finishing
        _weaponBehaviour.Tick(); // Finishing -> Idle (Ready for next combo)

        Assert.AreEqual(1, _mockMediator.Object.AttackIndex);
    }

    [Test]
    public void ComboAttack_ShouldFailIfComboLimitReached()
    {
        _weaponBehaviour.Enable();
        _mockInputProvider.Setup(i => i.HasAttackInput()).Returns(true);
        _mockMediator.Setup(m => m.AttackIndex).Returns(2); // Maximum combo index

        _weaponBehaviour.Tick(); // Preparing -> Executing
        _weaponBehaviour.Tick(); // Executing -> Finishing
        _weaponBehaviour.Tick(); // Finishing -> Idle (No further combo)

        Assert.AreEqual(2, _mockMediator.Object.AttackIndex); // Remains at max index
    }

    [Test]
    public void AttackCancel_ShouldReturnToIdle()
    {
        _mockMediator.Setup(m => m.CanStartAttack()).Returns(false);

        _weaponBehaviour.Enable();
        _weaponBehaviour.Tick(); // Should attempt Preparing but cancel to Idle

        Assert.AreEqual(AttackState.None, _mockMediator.Object.AttackState.Value);
    }

    [TearDown]
    public void Teardown()
    {
        _weaponBehaviour.Disable();
    }
}
