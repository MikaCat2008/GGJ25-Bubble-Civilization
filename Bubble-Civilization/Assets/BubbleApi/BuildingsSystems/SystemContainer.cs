namespace BubbleApi
{
    public class SystemsContainer
    {
        public BuildingSystem building;
        public House_BuildingSystem house;
        public Mine_BuildingSystem mine;
        public PowerStation_BuildingSystem powerStation;
        public GreenHouse_BuildingSystem greenHouse;
        public ShipDock_BuildingSystem shipDock;
        public AirPurificationStation_BuildingSystem airPurificationStation;

        public SystemsContainer(Storage storage)
        {
            this.building = new BuildingSystem();
            this.building.storage = storage;
            this.house = new House_BuildingSystem();
            this.house.storage = storage;
            this.mine = new Mine_BuildingSystem();
            this.mine.storage = storage;
            this.powerStation = new PowerStation_BuildingSystem();
            this.powerStation.storage = storage;
            this.greenHouse = new GreenHouse_BuildingSystem();
            this.greenHouse.storage = storage;
            this.shipDock = new ShipDock_BuildingSystem();
            this.shipDock.storage = storage;
            this.airPurificationStation = new AirPurificationStation_BuildingSystem();
            this.airPurificationStation.storage = storage;
        }

        public BuildingSystem GetBuildingSystem(BuildingType type)
        {
            if (type == BuildingType.House)
                return this.house;
            else if (type == BuildingType.Mine)
                return this.mine;
            else if (type == BuildingType.PowerStation)
                return this.powerStation;
            else if (type == BuildingType.GreenHouse)
                return this.greenHouse ;
            else if (type == BuildingType.ShipDock)
                return this.shipDock;
            else if (type == BuildingType.AirPurificationStation)
                return this.airPurificationStation;
            return this.building;
        }
    }
}
