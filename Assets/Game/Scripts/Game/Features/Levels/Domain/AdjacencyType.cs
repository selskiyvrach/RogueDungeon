namespace Game.Features.Levels.Domain
{
    public enum AdjacencyType
    {
        None = 0,
        Corridor = 10,
        TJoint = 20,
        LJoint = 30,
        XJoint = 40,
        DeadEnd = 50,
    }
}