namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandsBehaviour 
    {
        public PlayerHandBehaviour RightHand {get; }
        // public PlayerHandBehaviour LeftHand {get; }

        public PlayerHandsBehaviour(PlayerHandBehaviour rightHandBehaviour, PlayerHandBehaviour leftHandBehaviour)
        {
            RightHand = rightHandBehaviour;
            // LeftHand = leftHandBehaviour;
        }

        public void Initialize()
        {
            RightHand.Initialize();
            // LeftHand.Initialize();
        }

        public void Tick(float deltaTime)
        {
            RightHand.Tick(deltaTime);
            // LeftHand.Tick(deltaTime);
        }
    }
}