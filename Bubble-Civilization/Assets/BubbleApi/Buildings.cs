using System.Collections.Generic;


namespace BubbleApi
{
    public enum BuildingType
    {
        Empty,
        Building,
        House,
        Mine,
        PowerStation,
        GreenHouse,
        ShipDock,
        AirPurificationStation
    }

    public class Building
    {
        public int id;
        public BuildingData data;
    
        private byte type;
        
        public Building(int id)
        {
            this.id = id;
            this.type = (byte)BuildingType.Empty;
            this.data = new BuildingData();
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

        public Building GetBuilding(int id)
        {
            if (this.container.TryGetValue(id, out Building building))
                return building;

            return new Building(id);
        }

        public void SetBuildingType(int id, BuildingType type)
        {
            if (this.container.TryGetValue(id, out Building building))
            {
                building.SetBuildingType(type);
                building.data = new BuildingData();
            }
            else 
            {
                building = new Building(id);
                building.SetBuildingType(type);

                this.container.Add(id, building);
            }
        }
    }
}
