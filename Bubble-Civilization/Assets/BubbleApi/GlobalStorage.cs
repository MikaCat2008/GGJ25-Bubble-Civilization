namespace BubbleApi
{
    public static class GlobalStorage
    {
        public static Storage storage;
        public static SystemsContainer systems;
        public static BuildingUpdater buildingUpdater;
        public static SceneType currentSceneType;

        private static bool initialized = false;

        public static bool Initialize()
        {
            if (GlobalStorage.initialized)
                return false;

            GlobalStorage.storage = new Storage();
            GlobalStorage.systems = new SystemsContainer(GlobalStorage.storage);
            GlobalStorage.buildingUpdater = new BuildingUpdater(GlobalStorage.systems);
            GlobalStorage.currentSceneType = SceneType.MainMenu;

            GlobalStorage.initialized = true;

            return true;
        }
    }
}
