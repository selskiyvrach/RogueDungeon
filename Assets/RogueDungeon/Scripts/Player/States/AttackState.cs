using System;
using RogueDungeon.Player.Commands;
using RogueDungeon.StateMachine;
using UnityEngine.Assertions;

namespace RogueDungeon.Player.States
{
    public class AttackState : FinishableByAnimationState<IAttackAnimation>
    {
        public AttackState(IAttackAnimation animation) : base(animation)
        {
        }
    }
    
    public class SwingState : FinishableByAnimationState<ISwingAnimation>
    {
        public SwingState(ISwingAnimation animation) : base(animation)
        {
        }
    }

    public class SwingAnimation : AnimationPlayer, ISwingAnimation
    {
        
    }

    public class AttackAnimation : AnimationPlayer, IAttackAnimation
    {
        public event Action OnHitKeyframe;

        protected override void OnEvent(int eventIndex)
        {
            Assert.AreEqual(eventIndex, 0);
            
        }
    }

    public class ConsumeCommandStateEnterHandler : IStateEnterHandler
    {
        private readonly ICommandsConsumer _commandsConsumer;
        private readonly Command _command;

        public ConsumeCommandStateEnterHandler(ICommandsConsumer commandsConsumer, Command command)
        {
            _commandsConsumer = commandsConsumer;
            _command = command;
        }

        public void OnEnter() => 
            _commandsConsumer.ConsumeCommandIfCurrent(_command);
    }
}