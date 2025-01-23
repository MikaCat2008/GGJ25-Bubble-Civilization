namespace BubbleApi
{
    public static class GlobalStorage
    {
        public static Storage storage;
        public static SystemsContainer systems;
        public static BuildingUpdater buildingUpdater;


        private static bool initialized = false;

        public static bool Initialize()
        {
            if (GlobalStorage.initialized)
                return false;

            GlobalStorage.storage = new Storage();
            GlobalStorage.systems = new SystemsContainer(GlobalStorage.storage);
            GlobalStorage.buildingUpdater = new BuildingUpdater(GlobalStorage.systems);
            GlobalStorage.systems.bubble.CreateBubble(0);
            GlobalStorage.initialized = true;

            return true;
        }
    }
}
