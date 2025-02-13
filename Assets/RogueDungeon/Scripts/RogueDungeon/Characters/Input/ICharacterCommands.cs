namespace RogueDungeon.Characters.Input
{
    public interface ICharacterInput
    {
        virtual bool TryConsume(Input input)
        {
            if (!HasInput(input))
                return false;
            ConsumeInput(input);
            return true;
        }

        bool HasInput(Input input);
        void ConsumeInput(Input input);
    }

    public enum Input
    {
        None,
        LightAttack = 100,
        HeavyAttack = 200,
        Block = 300,
        DodgeRight = 400,
        DodgeLeft = 450,
    }
}