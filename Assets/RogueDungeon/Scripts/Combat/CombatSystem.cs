namespace RogueDungeon.Combat
{
    public interface ISpawner
    {
        IFighter Spawn(string id);
    }

    public interface IFighter
    {
        
    }

    public class CombatSystem
    {
        private readonly IFighter[] _fightersInPositions = new IFighter[4];
        
        // glossary
            // fighter
            // attack pattern
            // chill pattern
            // hivemind
            // front-line
            // back-line
                // left
                // right
        
        // use cases
            // player moves to a tile, where enemies should be
            // spawn enemies
            // pass player character as a parameter
            // pass enemies as parameters
            // call start combat
            
        // loop
            // initialize enemy behaviours
            // track deaths
                // edit ongoing pattern
                // finish fight on last enemy death
            
            
        // spawn new enemies mid-fight
            // put at position
            // or swap with another position
    }
}