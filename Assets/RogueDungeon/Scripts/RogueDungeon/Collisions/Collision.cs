namespace RogueDungeon.Collisions
{
    public struct Collision
    {
        public readonly Positions Positions;
        public readonly ICollidable Collidable;

        public Collision(ICollidable collidable, Positions positions)
        {
            Positions = positions;
            Collidable = collidable;
        }
    }
}