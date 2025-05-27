namespace RogueDungeon.Items.Model
{
    // if two items equipped -> pick the one with the highest tier or the left one if both are the same
    public enum BlockingTier
    {
        None,
        Second, 
        First, 
    }
}