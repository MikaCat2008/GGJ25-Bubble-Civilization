using System.Collections.Generic;


namespace BubbleApi
{
    public static class GlobalStorage
    {
        public static Storage storage;
        public static SystemsContainer systems;
        public static ActionUpdater actionUpdater;
        public static BuildingUpdater buildingUpdater;
        public static ResourcesUpdater resourcesUpdater;

        public static bool initialized = false;

        public static bool Initialize()
        {
            if (GlobalStorage.initialized)
                return false;

            GlobalStorage.storage = new Storage();
            GlobalStorage.systems = new SystemsContainer(GlobalStorage.storage);
            
            GlobalStorage.actionUpdater = new ActionUpdater();
            GlobalStorage.actionUpdater.actions = new Dictionary<ActionType, ResourcesContainer>()
            {
                { ActionType.House_Build, new ResourcesContainer(
                    food: -20,
                    materials: -30
                ) },
                { ActionType.House_Update, new ResourcesContainer(
                    food: -1
                ) },
                { ActionType.House_Settle, new ResourcesContainer(
                    food: -5,
                    population: 1,
                    freePopulation: 1
                ) },
                { ActionType.House_Repair, new ResourcesContainer(
                    food: -10,
                    materials: -15
                ) },


                { ActionType.Mine_Build, new ResourcesContainer(
                    food: -25,
                    materials: -40
                ) },
                { ActionType.Mine_Update, new ResourcesContainer(
                    food: -1,
                    pollution: 1
                ) },
                { ActionType.Mine_Hire, new ResourcesContainer(
                    freePopulation: -1
                ) },
                { ActionType.Mine_Repair, new ResourcesContainer(
                    food: -12,
                    materials: -20
                ) },


                { ActionType.PowerStation_Build, new ResourcesContainer(
                    food: -20,
                    materials: -50
                ) },
                { ActionType.PowerStation_Update, new ResourcesContainer(
                    fuel: -1,
                    pollution: 1
                ) },
                { ActionType.PowerStation_Hire, new ResourcesContainer(
                    freePopulation: -1
                ) },
                { ActionType.PowerStation_Repair, new ResourcesContainer(
                    food: -10,
                    materials: -25
                ) },


                { ActionType.GreenHouse_Build, new ResourcesContainer(
                    food: -5,
                    materials: -5
                ) },
                { ActionType.GreenHouse_Update, new ResourcesContainer(
                    energy: -1
                ) },
                { ActionType.GreenHouse_Hire, new ResourcesContainer(
                    freePopulation: -1
                ) },
                { ActionType.GreenHouse_Repair, new ResourcesContainer(
                    food: -2,
                    materials: -2
                ) },


                { ActionType.ShipDock_Build, new ResourcesContainer(
                    food: -20,
                    materials: -50
                ) },
                { ActionType.ShipDock_Update, new ResourcesContainer(
                    energy: -1
                ) },
                { ActionType.ShipDock_BuildShip, new ResourcesContainer(
                    food: -20,
                    materials: -50
                ) },
                { ActionType.ShipDock_Hire, new ResourcesContainer(
                    freePopulation: -1
                ) },
                { ActionType.ShipDock_Repair, new ResourcesContainer(
                    food: -15,
                    materials: -50
                ) },


                { ActionType.AirPurificationStation_Build, new ResourcesContainer(
                    food: -10,
                    materials: -15
                ) },
                { ActionType.AirPurificationStation_Update, new ResourcesContainer(
                    energy: -1
                ) },
                { ActionType.AirPurificationStation_Hire, new ResourcesContainer(
                    freePopulation: -1
                ) },
                { ActionType.AirPurificationStation_Repair, new ResourcesContainer(
                    food: -5,
                    materials: -7
                ) }
            };

            GlobalStorage.buildingUpdater = new BuildingUpdater(GlobalStorage.systems);
            GlobalStorage.buildingUpdater.buildingsInfo = new Dictionary<BuildingType, BuildingInfo>()
            {
                { BuildingType.House, new BuildingInfo(
                    buildingTime: 5 * 60,
                    updateInterval: 10 * 60
                ) },
                { BuildingType.Mine, new BuildingInfo(
                    buildingTime: 10 * 60,
                    updateInterval: 10 * 60
                ) },
                { BuildingType.PowerStation, new BuildingInfo(
                    buildingTime: 15 * 60,
                    updateInterval: 5 * 60
                ) },
                { BuildingType.GreenHouse, new BuildingInfo(
                    buildingTime: 5 * 60,
                    updateInterval: 5 * 60
                ) },
                { BuildingType.ShipDock, new BuildingInfo(
                    buildingTime: 30 * 60,
                    updateInterval: 30 * 60
                ) },
                { BuildingType.AirPurificationStation, new BuildingInfo(
                    buildingTime: 10 * 60,
                    updateInterval: 10 * 60
                ) }
            };

            GlobalStorage.systems.bubble.CreateBubble(0);
            GlobalStorage.storage.currentBubble = GlobalStorage.storage.bubbles[0];

            GlobalStorage.resourcesUpdater = new ResourcesUpdater();

            GlobalStorage.initialized = true;

            return true;
        }

        public static void Reinitialize()
        {
            GlobalStorage.initialized = false;
            GlobalStorage.Initialize();
        }
    }
}
