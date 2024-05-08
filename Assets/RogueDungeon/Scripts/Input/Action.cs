namespace RogueDungeon.Input
{
    public enum Action
    {
        // Walking
        MoveForward = 100,
        TurnAround = 101,
        
        // Crossroad
        ChooseLeft = 200,
        ChooseRight = 201,
        ChooseForward = 202,
        ChooseBack = 203,
        
        // Combat
        Attack = 300,
        DodgeLeft = 301,
        DodgeRight = 302,
        Block = 303,
        
        // close
        // confirm
        // cancel
        // inventory
    }
}