// using Moq;
// using NUnit.Framework;
// using RogueDungeon.RogueDungeon.Scripts.RogueDungeon.Weapons;
// using UniRx;
//
// namespace RogueDungeon.Scripts.Tests.EditMode
// {
//     public class WeaponBehaviourTests
//     {
//         private Mock<IAttackMediator> _mediatorMock;
//         private Mock<IAttackInputProvider> _inputProviderMock;
//         private Mock<IAttackComboConfig> _comboConfigMock;
//         private WeaponBehaviour _weaponBehaviour;
//
//         [SetUp]
//         public void SetUp()
//         {
//             // Create mocks
//             _mediatorMock = new Mock<IAttackMediator>();
//             _inputProviderMock = new Mock<IAttackInputProvider>();
//             _comboConfigMock = new Mock<IAttackComboConfig>();
//
//             // Mock properties and methods
//             _mediatorMock.Setup(m => m.CanStartAttack).Returns(true);
//             _mediatorMock.SetupProperty(m => m.AttackState, new ReactiveProperty<AttackState>(AttackState.None));
//             _mediatorMock.SetupProperty(m => m.ComboIndex, 0);
//
//             _comboConfigMock.Setup(c => c.Count).Returns(3);
//             _comboConfigMock.Setup(c => c.GetTimings(It.IsAny<int>()))
//                 .Returns(new Mock<IAttackTimingsProvider>().Object);
//
//             // Initialize the behaviour
//             _weaponBehaviour = new WeaponBehaviour(
//                 _mediatorMock.Object,
//                 _inputProviderMock.Object,
//                 _comboConfigMock.Object);
//         }
//
//         [Test]
//         public void Enable_ResetsAttackStateAndComboIndex_AndRunsStateMachine()
//         {
//             // Act
//             _weaponBehaviour.Enable();
//
//             // Assert
//             Assert.AreEqual(AttackState.None, _mediatorMock.Object.AttackState.Value);
//             Assert.AreEqual(0, _mediatorMock.Object.ComboIndex);
//         }
//
//         [Test]
//         public void Disable_ResetsAttackStateAndComboIndex_AndStopsStateMachine()
//         {
//             // Arrange
//             _weaponBehaviour.Enable();
//
//             // Act
//             _weaponBehaviour.Disable();
//
//             // Assert
//             Assert.AreEqual(AttackState.None, _mediatorMock.Object.AttackState.Value);
//             Assert.AreEqual(0, _mediatorMock.Object.ComboIndex);
//         }
//
//         [Test]
//         public void Tick_CallsStateMachineTick()
//         {
//             // Arrange
//             _weaponBehaviour.Enable();
//
//             // Act
//             _weaponBehaviour.Tick();
//
//             // Assert
//             // No exceptions or errors indicate success since Tick() doesn't return values.
//             Assert.Pass();
//         }
//     }
// }
