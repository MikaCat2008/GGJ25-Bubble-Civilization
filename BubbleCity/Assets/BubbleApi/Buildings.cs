using System.Collections.Generic;


namespace BubbleApi
{
    public enum BuildingType
    {
        Empty = 0,
        House = 1,
        Mine_Fuel = 2,
        Mine_Materials = 3,
        GreenHouse_Food = 4,
        GreenHouse_Oxygen = 5,
        GreenHouse_Materials = 6,
        ShipDock_Discovery = 7,
        ShipDock_MaterialCollecting = 8,
        AirPurification = 9
    }

    public class BuildingsContainer
    {
        public List<BuildingType> container;

        public BuildingsContainer()
        {
            this.container = new List<BuildingType>();
        }

        public BuildingType GetBuildingType(byte id)
        {
            return this.container[id];
        }

        public void SetBuildingType(byte id, BuildingType type)
        {
            this.container[id] = type;
        }
    }
}
