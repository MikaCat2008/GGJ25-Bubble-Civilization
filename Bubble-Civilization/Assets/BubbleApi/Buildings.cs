using System.Collections.Generic;


namespace BubbleApi
{
    public enum BuildingType
    {
        Empty,
        Unavaliable,
        
        House,
        Mine, // Fuel, Materials
        GreenHouse, // Food, Oxygen, Materials
        ShipDock, // Discovery, MaterialCollecting
        AirPurification
    }

    public class Building
    {
        public BuildingData data;
    
        private byte type;
        
        public Building()
        {
            this.type = (byte)BuildingType.Unavaliable;
            this.data = new BuildingData();
        }

        public Building(BuildingType type, BuildingData data)
        {
            this.type = (byte)type;
            this.data = data;
        }

        public BuildingType GetBuildingType()
        {
            return (BuildingType)this.type;
        }
        
        public void SetBuildingType(BuildingType type)
        {
            this.type = (byte)type;
        }
    }

    public class BuildingsContainer
    {
        public Dictionary<int, Building> container;

        public BuildingsContainer()
        {
            this.container = new Dictionary<int, Building>();
        }

        public Building GetBuilding(byte id)
        {
            if (this.container.TryGetValue(id, out Building building))
                return building;

            return new Building();
        }

        public void SetBuildingType(byte id, BuildingType type)
        {
            if (this.container.TryGetValue(id, out Building building))
            {
                building.SetBuildingType(type);
                building.data = new BuildingData();
            }
            else 
            {
                building = new Building(type, this.CreateBuildingData(type));

                this.container.Add(id, building);
            }
        }

        private BuildingData CreateBuildingData(BuildingType type)
        {
            if (type == BuildingType.House)
                return new House_BuildingData();
            if (type == BuildingType.Mine)
                return new Mine_BuildingData();

            return new BuildingData();
        }
    }
}
