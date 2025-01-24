#nullable enable


namespace BubbleApi
{
    public class BuildingSystem : System
    {
        public ActionUpdater actionUpdater
        {
            get { return GlobalStorage.actionUpdater; }
        }

        public BuildingUpdater buildingUpdater
        {
            get { return GlobalStorage.buildingUpdater; }
        }

        public virtual void OnUpdate(Building building, Bubble bubble) { }

        public virtual Building Build(int id, Bubble bubble) 
        {
            return new Building(id);
        }

        public Building Build(int id, Bubble bubble, BuildingType type)
        {
            bubble.buildings.SetBuildingType(id, type);
            Building building = bubble.buildings.GetBuilding(id);

            building.data.interval = this.storage.timer.CreateInterval(
                this.buildingUpdater.buildingsInfo[type].updateInterval, 
                () => this.OnUpdate(building, bubble)
            );

            return building;
        }

        public virtual void Destroy(Building building, Bubble bubble)
        {
            bubble.buildings.SetBuildingType(building.id, BuildingType.Empty);

            this.storage.timer.DeleteInterval(building.data.interval);
            this.buildingUpdater.DestroyBuilding(building, bubble);
        }

        public void BreakBuilding(Building building)
        {
            building.data.requireRepair = true;
        }

        public virtual void RepairBuilding(Building building, Bubble bubble)
        {
            this.RepairBuilding(building);
            this.buildingUpdater.RepairBuilding(building, bubble);
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
            string name = building.GetBuildingType() == BuildingType.Empty ? "Пусто" : "Будується";
            
            return $"{name}[{building.id}]";
        }
    }
}
