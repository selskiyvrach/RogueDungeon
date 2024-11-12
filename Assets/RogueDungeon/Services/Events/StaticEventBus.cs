namespace RogueDungeon.Services.Events
{
    public static class StaticEventBus
    {
        public static EventBus Instance { get; }

        static StaticEventBus() => 
            Instance = new EventBus();
    }
}