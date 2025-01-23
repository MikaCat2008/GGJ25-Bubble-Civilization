#nullable enable


namespace BubbleApi
{
    public class BuildingSystem : System
    {
        public BuildingUpdater buildingUpdater
        {
            get { return GlobalStorage.buildingUpdater; }
        }

        public virtual void Update(Building building, Bubble bubble) { }

        public virtual Building Build(int id, Bubble bubble) 
        {
            return new Building(id);
        }

        public Building Build(int id, Bubble bubble, BuildingType type)
        {
            bubble.buildings.SetBuildingType(id, type);

            return bubble.buildings.GetBuilding(id);
        }

        public virtual void Destroy(Building building, Bubble bubble)
        {
            bubble.buildings.SetBuildingType(building.id, BuildingType.Empty);
        }

        public void BreakBuilding(Building building)
        {
            building.data.requireRepair = true;
        }
        public void RepairBuilding(Building building)
        {
            building.data.requireRepair = false;
        }

        public T GetData<T>(Building building, BuildingType type) where T : BuildingData
        {
            this.CheckType(building, type);

            return (T)building.data;
        }

        public void CheckType(Building building, BuildingType requiredType)
        {
            BuildingType type = building.GetBuildingType();

            if (type != requiredType)
                throw new BubbleApiException(
                    BubbleApiExceptionType.AnotherBuildingType
                );
        }

        public string BrokenText(Building building)
        {
            return building.data.requireRepair ? "(Зламано) " : "";
        }

        public virtual string BuildingToString(Building building)
        {
            return "Пусто";
        }
    }
}
