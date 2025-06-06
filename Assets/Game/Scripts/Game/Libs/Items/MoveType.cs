namespace Game.Libs.Items
{
    public enum MoveType
    {
        None,
        Idle = 10,
        
        Sheath = 20,
        Unsheath = 21,
        
        PrepareAttack = 100,
        ExecuteAttack = 101,
        RecoverAttack = 102,
        
        RaiseBlock = 200,
        HoldBlock = 201,
        LowerBlock = 202,
        AbsorbBlockImpact = 203,
        
        RaiseMap = 300,
        HoldMap = 301,
        LowerMap = 302,
    }
}